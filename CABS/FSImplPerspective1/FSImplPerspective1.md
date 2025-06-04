# Unit 08 File Systems Implementer's Perspective - Exercises Part1

## 1. FAT Main Memory Requirements

### 1a. How many blocks?

Disk Size: 250 GB (250 * 1024 * 1024 * 1024 Bytes)
Block Size: 1 kB (1024 Bytes)

Block Count: 250 GB / 1kB => 250 * 1024 * 1024.

The block count is about **250 million**.

### 2a. How many FAT entries?

The FAT has one entry per block, meaning it has **250 million entries**.

### 3a. How big must an entry be?

An entry in the FAT must be big enough to allocate all blocks, 250 million in this case.

To allocate **250 million blocks**, we use the **binary logarithm**, which yields a result of **27.897**.
Rounded, this equals **28 bits** or **3.5 bytes**.

For convenience we will use **4 bytes**.

### 4a. Final FAT size.

In the previous questions, we have concluded that the FAT has **250 million entries** and that one entry must have a size of **4 bytes**.

Meaning, that the final size of the **FAT** is 250,000,000 * 4 = **1,000,000,000 bytes** or **1 GB**.


## 2. Random Access in MS-DOS

MS-DOS implements random access using file pointers with the FAT file system.
Files are stored as a linked list of clusters, and each file has a logical offset managed by DOS.
You can move the file pointer byte-wise using the standard C function fseek() (or lseek() in POSIX).

When accessing a byte at a certain offset, MS-DOS translates this into the corresponding cluster by walking the FAT chain. If the file spans multiple clusters, DOS resolves the chain on demand, during read/write operations.

### Example Scenario
We have a file of 3 kB size, stored across 3 clusters.
We want to read from the middle of the file.

#### File Setup
- Cluster size: 1 kB
- File size: 3 kB
- Access position: 1.5 kB (middle of the file)
- FAT Chain:
| Cluster | Next Cluster |
| ------- | ------------ |
| 2 | 3 |
| 3 | 5 |
| 5 | -1 (EOF) |

This means: the file is stored in clusters 2 -> 3 -> 5.


#### Code Example (C)

We will read from the middle of the file using some C-Code.
```c
#include <stdio.h>

int main(void) {
    // open the file in binary mode (our file is a video, so binary works better)
    FILE *file = fopen("documentary.mp4", "rb");

    if (!file) {
        perror("Error opening file");
        return 1;
    }

    // jump to 1.5 kB offset (middle of the file)
    fseek(file, 1536, SEEK_SET);

    // read the byte in the middle
    unsigned char byte;
    fread(&byte, 1, 1, file);

    printf("Byte at middle: %02X\n", byte);

    fclose(file);
    return 0;
}
```

### What Actually Happens

When the C code runs, MS-DOS performs several steps internally to support random access on a FAT file system. Here’s what happens under the hood:

#### 1. `fopen("documentary.mp4", "rb")`

- DOS opens the file and reads the **directory entry**, which includes:
  - File size
  - Starting **cluster number**
- It assigns a **file handle** and creates an internal **File Control Block (FCB)** or **System File Table (SFT)** entry.
- The file pointer is initialized to offset **0**.

#### 2. `fseek(file, 1536, SEEK_SET)`

- DOS calculates:
  - Target byte offset = 1536
  - Cluster size = 1024 bytes → Target is in the **second half of the second cluster**
  - So: 1536 bytes = **Cluster 2 (0–1023)** → **Cluster 3 (1024–2047)** → Byte offset **512** within Cluster 3.

#### 3. On-Demand FAT Chain Resolution

- DOS does not store the full cluster chain in memory.
- Instead, it:
  1. Starts from the file’s **first cluster** (Cluster 2).
  2. Looks up Cluster 2 in the FAT table: it points to **Cluster 3**.
  3. Looks up Cluster 3: it points to **Cluster 5**, but that is not needed yet.
  4. Stops — because the second cluster (Cluster 3) contains the byte at offset 1536.

> FAT is resolved **on demand**, only as far as needed to reach the desired cluster.

---

### Summary: MS-DOS Random Access Flow

| Step             | Action                                                                 |
|------------------|------------------------------------------------------------------------|
| `fopen()`        | Loads directory info, initializes file handle                          |
| `fseek(offset)`  | Translates logical offset into cluster index                           |
| FAT resolution   | Follows FAT entries from start cluster to target (on demand)           |

## 3. Random Access of Files

### 3a. UFS

Target position: 1024
Block size: 512

**UFS** (Unix file system) uses i-nodes. For this example we assume the following configuration:
- 10 direct
- 1 single indirect
- 1 double indirect
- 1 triple indirect

---

#### 1. Find the correct block

**Direct 1**: 0-511
**Direct 2**: 512-1023
**Direct 3**: 1024-1635

Block index = Target / Block size = 1024 / 512 = 2
Meaning that the correct block is **Direct 3** (block index is 0-based).

#### 2. Find offset inside block

Offset = Target % Block size = 1024 % 512 = 0.
The position 1024 is **byte 0** in **block 2** (both 0-based).

#### 3. Read content

- Access the block by the pointer for **Direct 3**.
- Read the byte at **offset 0** from the block.

### 3b. Contiguous allocation

Target position: 1024
File start block: 512

We will also assume a block size of **512 bytes**.

---

#### 1. Find relative block number

Block number = 1024 / 512 = 2

#### 2. Find absolute block number

Absolute block number = start block + relative block number = 512 + 2 = 514

#### 3. Find offset inside block

Offset = 1024 % 512 = 0 (again the first byte)

#### 4. Access

- Read block **514**
- Read byte with **offset 0**

## 4. Random Access of Files

In both cases we know
- Target position: 107.834.590
- Block size: 1 kB

In both cases we need to know
- The block index
- The offset inside the block

We can calculate it like this
- Block index = `floor(107.834.590 / 1024) = **105.269**
- Offset = 107.834.590 % 1024 = **222**

### 4a. UFS

We will assume the same configuration of the i-node as in the previous question.

We can calculate the number of pointers for an indirect block like so (if we assume a pointer size of **4 bytes**):
- Block size / Pointer size = 1024 / 4 = **256**.

Now we can find the indirect level we need for the block 105.269
- 105.269 > 10 ==> **direct not enough**
- 105.269 > 256 + 10 ==> **single indirect not enough**
- 105.269 > 256^2 + 256 + 10 ==> **double indirect not enough**
- 105.269 < 256^3 + 256^2 + 256 + 10 ==> **we need a triple indirect**.

Now we need to find the block position inside the triple indirect space
- Block index (triple-indirect) = 105.269 - 65.804 = **39.465**
- Layer 1: `floor(39.465 / 256^2)` = **0**
- Layer 2: `floor(39.465 / 256) % 256` = **154**
- Layer 3: `39.465 % 256` = **121**

This means that we need to
1. Read the **triple indirect** block from the i-node.
2. Read **pointer 0** from the **double indirect** inside that.
3. Read **pointer 154** from the **single indrecit** inside that.
4. Read **pointer 121** to get to the required block.
5. Read **byte 222** to get to the requred byte.

### 4b. FAT32

To read block 105.269 in **FAT32** we need to start at the file's starting block and walk through the table until we arrive at the **105.269th block**.
From there we just need to read the **222nd byte** and we're done.
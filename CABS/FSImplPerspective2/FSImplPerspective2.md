# Unit 08 File Systems Implementer's Perspective - Exercises Part2

## 1. UFS (i-node) File Size

We will assume that our i-node looks like this
- 10 direct
- 1 single indirect
- 1 double indirect
- 1 triple indirect

### 4 kB block size

With a block size of **4 kB** and a pointer size of **4 bytes**, we can fit **1024 pointers** in one indirect block.

From that we can calculate:
- 10x Direct: **10 Blocks**
- Single indirect: **1024 Blocks**
- Double indirect: 1024 * 1024 = **1 048 576 Blocks**
- Triple indirect: 1024 * 1024 * 1024 = **1 073 741 824 Blocks**

Summing that up we get **1 074 791 434 Blocks**, which equate to **4 402 345 713 664 bytes** or about **4 TB**.

### 1 kB block size

With a block size of 1 kB and the same pointer size, we can only fit **256 pointers** in one indirect block.

This means that we only get
- 10x Direct: **10 Blocks**
- Single indirect: **256 Blocks**
- Double indirect: 256 * 256 = **65 536 Blocks**
- Triple indirect: 256 * 256 * 256 = **16 777 216 Blocks**

for a total of **16 843 018 Blocks** or **16 GB**.

## 2. UFS File Size

### 2a.

We assume a maximum file size of **5 GB** and want to create a UFS system with a **512 byte** block size.
To check if that works, we need to calculate the maximum file size that a 512 byte block size allows.

If we assume a **pointer size** of **4 bytes** again, we can fit **128 pointers** into one indirect block.
Assuming the same i-node structure as before, we get the following result:
- 10x Direct: **10 Blocks**
- Single indirect: **128 Blocks**
- Double indirect: 128 * 128 = **16 384 Blocks**
- Triple indirect: 128 * 128 * 128 = **2 097 152 Blocks**

for a total of **2 113 674** Blocks, which equal **1 082 201 088 bytes** or **1 GB**.

Meaning that a block size of **512 bytes** is **not enough** for a maximum file size of **5 GB**.
You would need a block size of **1024 bytes**.

### 2b.

From the previous question, we already know that a block size of **1024 bytes** can handle file sizes up to **16 GB**, meaning that the required block size would not change, if you estimated the maximum file size to **12 GB**.

## 3. File Systems

### 3a. What happens, when a new file is created?

| FAT | i-node-based |
| --- | ------------ |
| 1. New directory entry | 1. Reserve free i-node |
| 2. Look for a free block to put in the directoy entry | 2. Write metadata and pointer to the first data block in the i-node |
| 3. Update **FAT** (e.g. Block 5 -> Block 6 -> EOF) | 3. Directory only saves file name + i-node-number |

### 3b. What happens, when the file size gets bigger?

| FAT | i-node-based |
| --- | ------------ |
| 1. Look for a free block | 1. Reserve new block |
| 2. Update the **FAT** with new block link | 2. Write block address in the i-node (direct or indirect) |
| 3. Mark the new block as **EOF** | |

### 3c. What happens, when a file gets deleted?

| FAT | i-node-based |
| --- | ------------ |
| 1. Delete directory entry | 1. Delete directory entry |
| 2. Mark block in the **FAT** as *free* | 2. Mark i-node as *free* |
| | 3. Mark all referenced data-blocks as *free* |

### 3d. Advantages and Disadvantages of FAT and i-node-based File Systems

#### FAT

**Advantages:**
- Simple to implement
- Widely supported and cross-platform compatible (e.g., Windows, cameras, USB drives)
- Low overhead – ideal for small, portable media

**Disadvantages:**
- Poor performance with fragmented files
- No journaling -> risk of data loss on crashes or power failure
- Central FAT table is a single point of failure
- File size limitations (e.g., 4 GB max with FAT32)

**Best suited for:**
- Small, removable storage (e.g., USB sticks, SD cards)
- Systems with limited resources or simple needs

---

#### i-node-based File Systems

**Advantages:**
- Better performance for large files
- More robust against fragmentation and power loss
- Supports very large files and file systems
- Efficient metadata storage and access

**Disadvantages:**
- More complex structure
- Fixed number of i-nodes can limit number of files
- Not natively compatible with Windows

**Best suited for:**
- Server and desktop systems with large data sets
- Multimedia storage
- Applications requiring stability, scalability, and performance

---

#### Summary & Recommendation

| Use Case | Recommended System | Reason |
| -------- | ------------------ | ------ |
| USB stick / SD card | **FAT** | High compatibility, minimal overhead |
| Linux server / Multimedia HDD | **i-node-based (e.g., ext4)** | Supports large files, journaling, stable & high-performance |
| Embedded systems | **FAT or FAT32** | Simplicity and low resource consumption |
| Backup systems | **i-node-based** | Efficient handling of large files and metadata |

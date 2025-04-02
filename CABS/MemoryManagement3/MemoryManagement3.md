# Unit 07 - Memory Management: Exercises 03

## 1. Which kind of memory management will you use in case that the programs are too big to fit into memeory? Explain why.

I would use virtual memory, because it allows me to act like I have more memory than I actually have.

## 2. Explain in own words, how "virtual memory" works.

You split the physical memory into multiple parts, called **Page Frames**.
Then, you have your virtual memory. It is also split into parts (of the same size), called **Pages**.

Each page is mapped to a page frame in physical memory (not actually every page because there are more pages than page frames).
Processes only get to know the virtual address space.

When a process tries to use some memory, and this memory is mapped to a page frame, the **MMU** converts the addresses and gives the process acess to the memory.
But when a process tries to use memory that is not mapped to physical memory, there will be a **page fault** (more on that later).

## 3. Why does virtual memeory work perfectly in a multiprogramming system?

Because the "problem" with page faults disappears completely.
When a page fault is done, the OS needs to swap in the required page frame.
This takes time.
But when a process is waiting for the data, another one can get the CPU.
Meaning, that there is essentially no delay when a page fault happens.

## 4. Explain with your own words: What is the difference between virtual memory address and physical memory address?

The **physical memory** is actually present on the system (in the form of RAM sticks), while the **virtual memory** is just a trick to *fake* more memory.

Processes only know the virtual address space; everything else happens behind the scenes.

Virtual addresses are mapped to physical addresses by the **MMU**.

## 5. Explain with your own words: What is an MMU and for what is it used for?

The **M**emory **M**anagement **U**nit is used to map **virtual** addresses to **physical** space.

## 6. Explain with your own words: What is the difference between pages and page frames?

**Pages** are chunks of virtual memory, while **Page frames** are chunks of physical memory.
They have the same size!

## 7. Explain with your own words: What is a page fault?

A **page fault** happens when a process tries to reference a part of the address space that is **not in mapped to physical memory**.

## 8. Explain with your own words: What happens next if there is an trap due to an page fault.

1. The **MMU** notices, that a page is unmapped.
2. Causes CPU to trap to OS (trap called page fault).
3. OS picks a little used page frame and writes the content to disk.
4. OS fetches referenced page frame into the now free page frame.
5. OS updates map and restarts trapped instruction
# Unit 07 - Memory Management: Exercises 01

## 1. Explain the three main problems with RAM.

- The first problem is, that **there is never enough RAM**.
    Example: A 32 bit system gives each program its own 4GB address space. A program can access any byte of the address space (except those, reserved for the OS).

    **Why do we that?** Because it's easiest for the user.

    **But what if the computer only has 2GB RAM?** That's a **Problem**.


 - The second problem is, that there will be **holes in the address space**, also called [memory fragmentation](#2-explain-the-term-memory-fragmentation-in-association-with-ram).
     Example: We have a 4GB address space (again). We have 3 Programs: A (1GB), B (2GB) and C (2GB).
      We can fit program A and B simultaneously, because we have enough memory.
      Now we quit Program A. Program B is now right in the middle of the memory.

      Meaning, **altough we have 2GB free, we can't run Program C** ==> **Memory Fragmentation**


 - The third problem is **keeping programs secure**
      Example: Image, we have two programs, a **shooter** and an **accounting program**.
     The programmers of both programs really like the **address 1234**, so they save their most important data there (the highscore and the total profit).

     **What happens when we run both programs at once?** Both programs try to save their data to the same address ==> They can **corrupt** or **crash** each other

## 2. Explain the term "Memory Fragmentation" in association with RAM

**Memory Fragmentation** is, when there are gaps in the address space. This can happen, if we run multiple programs, quit one, which leaves the second program in the middle of memory.
Refer to the example in the [previous answer](#1-explain-the-three-main-problems-with-ram)

## 3. In case of no memory abstraction: Why is it not possible to run two processes in parallel? Explain with
your own words.

When we have **no Memory Management** and **no memory abstraction**, and we try to run 2 processes, they will certainly interfere with each other.

If Program A is loaded at **address 0** and Program B at **address 10000** and B has a `jmp`-command to a static address, it will probably jump right into A.
Also, saving to static addresses in memory will interfere with Program A.

In short, **exposing physical addresses to processes is not a good idea**

## 4. In case of no memory abstraction; How can you achieve at least multiprocessing in such a system?
Explain with your own words.

In Theory, you can at least achieve parallelism using **Threads**.

**But what if we wan't multiple programs?** We can do it, but it won't be a fun experience for the user.

Having multiple programs without any memory management/abstraction is actually surprisingly simple, we just swap them out to the disk, so we only ever have 1 program running at a time.

**The problem**: It's **crazy slow!!** Writing entire programs to the disk multiple times a second is a very expensive process and likely won't be fun for the user. 
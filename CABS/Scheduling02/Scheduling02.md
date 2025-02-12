# Unit 06 - Scheduling Exercises 02

## 1. Describe the *first-come-first-serve* scheduling algorithm with your own words

The *first-come-first-serve* scheduling algorithm is typically used in batch-systems. The process that was registered first (that *came* first), will always be executed first. This scheduling algorithm behaves like a Queue, where the FIFO Principe applies.

## 2. Explain why it is necessary for the *shortest-job-next* algorithm to know how long the expected execution time is?

How else would it know which one is the shortest job (the one to execute next).

## 3. Shortest Job first scheduling

### a) is it a preemptive or non-preemptive procedure

The shortest-job-first algorithm is **non-preemptive**.

### b) is it possible that a process looses the CPU during execution (another process gets the CPU)?

No, it is not possible, because the current job will always be executed completely before a new proccess is even considered.

## 4. Minimizing *turn-around-time*

Given is the following list of processes in status *ready*:

| Process: | A | B | C | D | E | F | G | H |
| -------- | - | - | - | - | - | - | - | - |
| Expected Runtime: | 8 | 5 | 16 | 12 | 55 | 21 | 2 | 34 |

### a) What is the average turn-around-time if a *first-come-first-serve* scheduling algorithm is used

When the processes are queued in this exact order, a *first-come-first-serve* algorithm would have a turn-around-time of: *8 + (8 + 5) + (8 + 5 + 16) + (8 + 5 + 16 + 12) + (8 + 5 + 16 + 12 + 55) + (8 + 5 + 16 + 12 + 55 + 21) + (8 + 5 + 16 + 12 + 55 + 21 + 2) + (8 + 5 + 16 + 12 + 55 + 21 + 2 + 34)* for a total of **576** quanta.

### b) What is the average turn-around-time if the scheduler uses the *shortest-job-first* algrorithm?

For the *shortest-job-first* algorithm, the order of processes would look like this:

| Process: | G | B | A | D | C | F | H | E |
| -------- | - | - | - | - | - | - | - | - |
| Expected Runtime: | 2 | 5 | 8 | 12 | 16 | 21 | 34 | 55 |

Here the turn-around-time would be *2 + (2 + 5) + (2 + 5 + 8) + (2 + 5 + 8 + 12) + (2 + 5 + 8 + 12 + 16) + (2 + 5 + 8 + 12 + 16 + 21) + (2 + 5 + 8 + 12 + 16 + 21 + 34) + (2 + 5 + 8 + 12 + 16 + 21 + 34 + 55)* for a total of **409** quanta.

## 5. Shortest Job remaining Time next scheduling: Explain in your own words why this method must be a preemptive.

The core feature of this scheduling algorithm is, that if a new job, whose remaining time is less than the remaining time of the currently running process, arrives, this new process will get executed. 
This requires the original process to be interruptable, meaning the whole system needs to be preemptive.
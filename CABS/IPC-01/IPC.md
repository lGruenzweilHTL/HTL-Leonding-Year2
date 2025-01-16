# Process IPC: Exercise I - Some Simple Questions

## Race Condition

A Race Condition occurs, when two or more processes try to access the same resource at the same time.
This can lead to a multitude of problems including mutual exclusion and overwriting data.

## Lock Variables

Task: Analyze the following code snipped when which instruction occurs and how a race condition can occur. Provide two time lines: One when no race condition occurs and one with a race condition.

```c
int sem = 0; // shared variable
while (sem == 1); // take care, empty statement!
sem = 1;
// critical region starts
LongRunner r = new LongRunner();
r.start(); // some time consuming method
// critical region ends
sem = 0;
```

**Timeline 1** (No race condition):
Process A gains computing time and manages to set `sem` to 1 (line 3).
Process B stays in the loop (line 2) while Process A completes the code and resets `sem`.
Process B gains computing time and completes the code.

---

**Timeline 2** (Race condition):
Process A gains computing time and comletes the loop (line 2).
Before Process A can set `sem` to 1, Process B gains computing time and sets `sem` to 1.
Both Processes are past the loop and execute the code almost simultaneously.

## Peterson's Solution
**a) Using the Peterson's solution - is it possible that both processes enter the critical region?**

No, it is not possible that Peterson's solution lets both processes access the critical region at the same time.

**b) What is still the problem with the Peterson's Solution?**

One of the main issues with the Peterson's Solution is the use of busy waiting and wasting clock cycles, that could be used for other things.

## Busy Waiting
**a) What does the term "Busy Waiting" mean?**

Busy Waiting describes the act of actively waiting until a given condition is met, for example in a `while`-loop.

**b) Give a coding example with "Busy Waiting", for example using a lock variable**

This code uses the shared lock variable `cr_is_blocked` to lock the critical region for one of the processes.
	It uses a `while`-loop to wait until the region is unlocked. This is called busy waiting as the process keeps the cpu busy with checking a single condition a lot of times. 

```c
extern cr_is_blocked = false;
void enter_region() {
	while (cr_is_blocked) {
		// busy waiting
	}
	cr_is_blocked = true;
}
void leave_region() {
	cr_is_blocked = false;
}
```

**c) Can Busy Waiting be avoided? If so, how?**

Busy waiting can easily be avoided by surrendering your cpu time while we need to wait
This can be done using `sleep()` and `wakeup()`

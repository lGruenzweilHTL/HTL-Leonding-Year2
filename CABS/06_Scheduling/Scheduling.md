1. Explain in own words the difference between "I/O bound proceses" and "CPU bound processes".

    I/O bound processes use the majority of their time for I/O operations, whereas CPU bound processes spend more time with computations interrupted by a little bit of I/O.

    **The speed of execution is primarily limited by different factors (CPU for CPU-bound and I/O for I/O-bound)**
2. List all single steps of a context switch.
    - Trap from User to Kernel Mode
    - Save state of current process
    - Select process to run
    - Restore state of process to run
    - Start process
3. List all situations which require the scheduler to schedule a new process.
    - When a process exits
    - When a process blocks on I/O, semaphore, etc.
    - When an I/O interrupt occurs
4. Describe in own words the difference of *non-preemptve* and *preemptive* scheduling.

    Preemptive Scheduling makes a scheduling decision based on a hardware clock. Every k'th cycle a decision is made.

    Non-Preemptive Scheduling executes a process until it is either finished or blocked.
5. List three categories of schedulng algorithms.

    - Batch
        - widespread use in the business world for periodic tasks (payroll, interest calculation, …)
        - Non-preemptive and preemptive Algorithms used
        - Long CPU Time for tasks, few process switches
    - Interactive
        - Preemption required
    - Real-Time
        - Preemtion (sometimes) not needed
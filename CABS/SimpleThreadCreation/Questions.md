- Task a) How can it happen, that the output is not in sequence?

    The sequence, in which the threads are executed depends on the amount of computing time each thread gets. This is decided by the scheduler.
- Task b) Why are some output lines longer than others and why isn't every line printed exactely like Thread id: &lt;id&gt; Number &lt;number&gt;?

    Because the method the threads have to execute have multiple `Console.WriteLine`'s.
    So if the Scheduler decides a thread should stop getting computing time midway through the execution of the method, another thread has time to cram in its execution before the other thread can finish.
    That means, that the outputs can sometimes be quite a mess and there can be some really jumbled up lines.
- Do you have any other ideas (except of a static variable) how to hand over data to the method executed by the thread?

    You can give the thread a method with an object as a parameter. This object can contain all of the data the thread needs and can even be used to hand over return values.
- Do you have any ideas how to hand over return values from the thread ot the parent proces?

    As I mentioned in the answer to the previous question, if you use the `ParameterizedThreadStart`, you can give the thread a single object as a parameter. 
    This object can not only be used to transfer data to the thread, but it can also be used to hand over a return value.
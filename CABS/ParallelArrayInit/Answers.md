# Questions for ParallelArrayInitialization

1. Experiment with serial and parallel solutions and check the runtime behaviour of both. Which solution is more efficient (in terms of performance)?

    For smaller arrays the serial solution is actually the faster one, but as the arrays get larget, the parallel solution becomes comparatively more efficient.
2. Use arrays of different sizes. Is there a size which makes one solution more performant compared to the other?

    On my machine, a size of 150x150 consistently yields equal results. For smaller arrays, the serial method usually outperforms the parllel method, for larger arrays it's vice-versa.
3. Can you explain why which version is more efficient?

    The Parallel method is more efficient for the majority of array sizes, because the gain in performance outweighs the creation time of the threads.
    For sufficiently small arrays, the creation time of the threads is comparatively big, meaning that the serial method will be faster.
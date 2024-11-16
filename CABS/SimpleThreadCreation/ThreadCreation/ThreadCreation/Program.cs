namespace ThreadCreation;

class Program {
    private static int number = 0; // Shared variable which is incremented when a thread starts
    
    private static void Main() {
        const int NUM_THREADS = 20;
        
        Console.WriteLine("Main: Creating threads...");
        for (int i = 0; i < NUM_THREADS; i++) {
            new Thread(PrintThreadId).Start();
            number++;
        }
        Console.WriteLine("Main: Thread creation finished.");
    }

    private static void PrintThreadId() {
        Console.Write($"Thread id: {Environment.CurrentManagedThreadId} ");
        Console.WriteLine($"Number: {number}");
    }
}
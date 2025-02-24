namespace ReadersWritersSolution;

internal static class Program
{
    private const int READERS = 3;
    private const int WRITERS = 1;

    private static Semaphore rcMutex = new(1, 1); // controls access to rc
    private static Semaphore db = new(1, 1); // controls access to the database
    private static int readCount = 0; // # of processes reading or wanting to read

    private static void Main()
    {
        for (int i = 0; i < READERS; i++) 
            new Thread(Reader).Start();

        for (int i = 0; i < WRITERS; i++) 
            new Thread(Writer).Start();
    }

    private static void Reader()
    {
        rcMutex.WaitOne(); // get exclusive access to update readCount

        readCount++; // One more reader now

        // if this is the first reader, get exclusive access to the db
        // subsequent readers are already guaranteed exclusive access
        if (readCount == 1)
        {
            StatusMessage("First reader, waiting on db...", reader: true);
            db.WaitOne();
        }
        StatusMessage("Readers have exclusive access to db...", reader: true);

        rcMutex.Release(); // release lock on readCount
        // Read database
        StatusMessage("Reading from db...", reader: true);
        rcMutex.WaitOne(); // get exclusive access to update readCount

        readCount--; // We are finished, so one less reader

        if (readCount == 0) // when all readers are done, release access to the db
        {
            StatusMessage("Last reader, releasing db...", reader: true);
            db.Release();
        }

        rcMutex.Release(); // release control of rc

        // do something with data
        
        StatusMessage("Done reading from db...", reader: true);
    }

    private static void Writer()
    {
        // think up some data

        StatusMessage("Waiting on db...", reader: false);
        db.WaitOne(); // Wait until we have exclusive access to the db

        // write to db

        StatusMessage("Done writing to db...", reader: false);
        db.Release(); // When we're done, release access to the db
    }

    private static void StatusMessage(object? msg, bool reader)
    {
        Console.WriteLine($"[{DateTime.Now}] Thread {Environment.CurrentManagedThreadId} ({(reader ? "reader" : "writer")}): {msg}");
    }
}
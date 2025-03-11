using Raylib_cs;

namespace ReadersWritersSolution;

internal static class Program {
    private static int numReaders = 1000;
    private static int numWriters = 10;

    // Semaphores for concurrency
    private static SemaphoreSlim readSemaphore = new SemaphoreSlim(1, 1);
    private static SemaphoreSlim writeSemaphore = new SemaphoreSlim(1, 1);

    private static int readers = 0;
    private static int writersWaiting = 0;
    private static bool resourceInUseByWriter = false;

    // Database location
    private static int databaseX = 550;
    private static int databaseY = 250;

    // Default positions
    private static int defaultReaderX = 50;
    private static int defaultReaderY = 100;
    private static int defaultWriterX = 750;
    private static int defaultWriterY = 100;

    // Current positions
    private static float[] readerX;
    private static float[] readerY;
    private static float[] writerX;
    private static float[] writerY;

    // Target positions for animation
    private static float[] readerTargetX;
    private static float[] readerTargetY;
    private static float[] writerTargetX;
    private static float[] writerTargetY;

    private static long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

    public static void Main() {
        Raylib.InitWindow(800, 600, "Readers/Writers Animated");
        Raylib.SetTargetFPS(60);

        readerX = new float[numReaders];
        readerY = new float[numReaders];
        writerX = new float[numWriters];
        writerY = new float[numWriters];
        readerTargetX = new float[numReaders];
        readerTargetY = new float[numReaders];
        writerTargetX = new float[numWriters];
        writerTargetY = new float[numWriters];

        // Initialize positions
        for (int i = 0; i < numReaders; i++) {
            readerX[i] = defaultReaderX + (i % 25) * 15;
            readerY[i] = defaultReaderY + (i / 25) * 15;
            readerTargetX[i] = readerX[i];
            readerTargetY[i] = readerY[i];
        }
        for (int i = 0; i < numWriters; i++) {
            writerX[i] = defaultWriterX;
            writerY[i] = defaultWriterY + i * 15;
            writerTargetX[i] = writerX[i];
            writerTargetY[i] = writerY[i];
        }

        // Start reader threads
        Thread[] readerThreads = new Thread[numReaders];
        for (int i = 0; i < numReaders; i++) {
            int id = i;
            readerThreads[i] = new Thread(() => ReaderThread(id));
            readerThreads[i].Start();
        }

        // Start writer threads
        Thread[] writerThreads = new Thread[numWriters];
        for (int i = 0; i < numWriters; i++) {
            int id = i;
            writerThreads[i] = new Thread(() => WriterThread(id));
            writerThreads[i].Start();
        }

        // Render loop
        float moveSpeed = 5f; // movement speed
        float writerMoveSpeed = 5f; // faster movement speed for writers
        while (!Raylib.WindowShouldClose()) {
            // Gradually move each reader/writer toward its target
            for (int i = 0; i < numReaders; i++) {
                MoveToward(ref readerX[i], ref readerY[i], readerTargetX[i], readerTargetY[i], moveSpeed);
            }
            for (int i = 0; i < numWriters; i++) {
                MoveToward(ref writerX[i], ref writerY[i], writerTargetX[i], writerTargetY[i], writerMoveSpeed);
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            // Draw the database
            if (resourceInUseByWriter) {
                Raylib.DrawRectangle(databaseX, databaseY, 100, 100, Color.Red);
            } else if (readers > 0) {
                Raylib.DrawRectangle(databaseX, databaseY, 100, 100, Color.Blue);
            } else {
                Raylib.DrawRectangle(databaseX, databaseY, 100, 100, Color.Gray);
            }

            // Draw readers
            for (int i = 0; i < numReaders; i++) {
                Raylib.DrawCircle((int)readerX[i], (int)readerY[i], 5, Color.Blue);
            }

            // Draw writers
            for (int i = 0; i < numWriters; i++) {
                Raylib.DrawRectangle((int)writerX[i] - 5, (int)writerY[i] - 5, 10, 10, Color.Green);
            }

            // Debug info
            long elapsed = DateTimeOffset.Now.ToUnixTimeMilliseconds() - startTime;
            Raylib.DrawText($"Active Readers: {readers}", 10, 10, 20, Color.Black);
            Raylib.DrawText($"Writers Waiting: {writersWaiting}", 10, 40, 20, Color.Black);
            Raylib.DrawText($"Resource In Use: {(resourceInUseByWriter ? "YES" : "NO")}", 10, 70, 20, Color.Black);
            Raylib.DrawText($"Total Threads: {numReaders + numWriters}", 10, 100, 20, Color.Black);
            Raylib.DrawText($"Elapsed Time: {elapsed / 1000f:F2}s", 10, 130, 20, Color.Black);

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }

    private static void ReaderThread(int i) {
        while (true) {
            readSemaphore.Wait();
            if (readers == 0) writeSemaphore.Wait();
            Interlocked.Increment(ref readers);
            readSemaphore.Release();

            // Move to database
            readerTargetX[i] = databaseX - 50 + (i % 2 == 0 ? -10 : 10);
            readerTargetY[i] = databaseY;

            Thread.Sleep(2000); // reading

            // Move away
            readSemaphore.Wait();
            Interlocked.Decrement(ref readers);
            if (readers == 0) writeSemaphore.Release();
            readSemaphore.Release();

            readerTargetX[i] = defaultReaderX + (i % 25) * 15;
            readerTargetY[i] = defaultReaderY + (i / 25) * 15;
            Thread.Sleep(500); // let others have a chance
        }
    }

    private static void WriterThread(int i) {
        while (true) {
            Interlocked.Increment(ref writersWaiting);
            writeSemaphore.Wait();
            Interlocked.Decrement(ref writersWaiting);

            // Move to database
            writerTargetX[i] = databaseX + 150;
            writerTargetY[i] = databaseY + 20;
            resourceInUseByWriter = true;

            Thread.Sleep(600); // writing
            resourceInUseByWriter = false;
            writeSemaphore.Release();

            // Move away
            writerTargetX[i] = defaultWriterX;
            writerTargetY[i] = defaultWriterY + i * 15;
            Thread.Sleep(1500); // let others have a chance
        }
    }

    private static void MoveToward(ref float x, ref float y, float tx, float ty, float speed) {
        float dx = tx - x;
        float dy = ty - y;
        float dist = (float)Math.Sqrt(dx*dx + dy*dy);
        if (dist > 0.5f) {
            float step = Math.Min(speed, dist);
            x += (dx / dist) * step;
            y += (dy / dist) * step;
        } else {
            x = tx;
            y = ty;
        }
    }
}
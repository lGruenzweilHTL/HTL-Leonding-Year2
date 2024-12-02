using System.Diagnostics;

namespace ParallelArrayInit;

class Program
{
    private static void Main()
    {
        const int size = 100;
        int[,] array = new int[size, size];
        var init = new ArrayInitializer(array);
        
        Console.WriteLine($"Initializing array with a size of {size}...");
        
        // Create serial
        Stopwatch timer = Stopwatch.StartNew();
        init.InitSerial();
        timer.Stop();
        Console.WriteLine($"Serial initialization finished in {timer.ElapsedMilliseconds} ms");
        
        // Create parallel
        timer.Restart();
        init.InitParallel();
        timer.Stop();
        Console.WriteLine($"Parallel initialization finished in {timer.ElapsedMilliseconds} ms");
    }
}

class ArrayInfo
{
    public int[,] Array;
    public int StartCol;
    public int EndCol;
}

class ArrayInitializer
{
    public ArrayInitializer(int[,] _array) => array = _array;
    
    private int[,] array;
    
    public void InitSerial()
    {
        ArrayInfo arrayInfo = new ArrayInfo
        {
            Array = array,
            StartCol = 0,
            EndCol = array.GetLength(1) - 1
        };

        InitializeArray(arrayInfo);
    }

    public void InitParallel()
    {
        ArrayInfo info1 = new ArrayInfo
        {
            Array = array,
            StartCol = 0,
            EndCol = array.GetLength(1) / 2 - 1
        };
        ArrayInfo info2 = new ArrayInfo
        {
            Array = array,
            StartCol = info1.EndCol + 1,
            EndCol = array.GetLength(1) - 1
        };

        var t1 = new Thread(InitializeArray);
        t1.Start(info1);
        var t2 = new Thread(InitializeArray);
        t2.Start(info2);

        t1.Join();
        t2.Join();
    }
    
    private void InitializeArray(object info)
    {
        ArrayInfo arrInfo = (ArrayInfo)info;
        for (int i = 0; i < arrInfo.Array.GetLength(0); i++)
        {
            for (int j = arrInfo.StartCol; j <= arrInfo.EndCol; j++)
            {
                arrInfo.Array[i, j] = 1; // Put a 1 in each cell
            }
        }
    }
}
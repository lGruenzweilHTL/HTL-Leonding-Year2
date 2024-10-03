using System.Diagnostics;
using System.Text;

internal class Program
{
    private const string VALID_TIPS = "ValidLottoTips.csv";
    private const string INVALID_TIPS = "InvalidLottoTips.csv";
    private const string HEADER = "ID;Num1;Num2;Num3;Num4;Num5;Num6";
    
    public static void Main()
    {
        // When is a tip valid?
        // Exactly 6 numbers (7 columns in file)
        // Numbers between 1 and 45
        // Every number is distinct

        Stopwatch time = Stopwatch.StartNew();
        
        string[] lines = File.ReadAllLines("LottoTipps.csv")[1..];
        
        string invalidLines = HEADER;
        StreamWriter validStream = new(VALID_TIPS);
        validStream.Write(HEADER);
        
        Stopwatch processTime = Stopwatch.StartNew();
        // Validate
        foreach (var line in lines)
        {
            if (!TryParseCsvLine(line, out int[] tip) || !TipIsValid(tip))
            {
                invalidLines += $"\n{line}";
            }
            else
            {
                validStream.Write($"\n{line}");
            }
        }
        
        processTime.Stop();
        
        validStream.Close();
        File.WriteAllText(INVALID_TIPS, invalidLines);
        
        time.Stop();
        Console.WriteLine($"Processing finished in {processTime.ElapsedMilliseconds}ms");
        Console.WriteLine($"Whole process finished in {time.ElapsedMilliseconds}ms");
    }

    public static bool TryParseCsvLine(string line, out int[] tip)
    {
        tip = null;
        
        string[] parts = line.Split(';');
        int[] nums = new int[parts.Length - 1];
        for (int i = 1; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i], out nums[i-1])) return false;
        }

        tip = nums;
        return true;
    }

    public static bool TipIsValid(int[] nums)
    {
        bool distinctNumbers = nums.Distinct().Count() == 6;
        bool isValid = nums.All(n => n is >= 1 and <= 45) && distinctNumbers;
        return isValid;
    }
}
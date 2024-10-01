using System.Text;

internal class Program
{
    public record Tip(string id, int[] numbers);

    private const string VALID_TIPS = "ValidLottoTips.csv";
    private const string INVALID_TIPS = "InvalidLottoTips.csv";
    private const string HEADER = "ID;Num1;Num2;Num3;Num4;Num5;Num6";
    
    public static void Main(string[] args)
    {
        // When is a tip valid?
        // Exactly 6 numbers (7 columns in file)
        // Numbers between 1 and 45
        // Every number is distinct
        
        string[] lines = File.ReadAllLines("LottoTipps.csv")[1..];
        
        string invalidLines = HEADER;
        FileStream validStream = new FileStream(VALID_TIPS, FileMode.Open);
        // Validate
        foreach (var line in lines)
        {
            if (!TryParseCsvLine(line, out Tip? tip) || !TipIsValid(tip!.numbers))
            {
                invalidLines += $"\n{line}";
            }
            else
            {
                validStream.Write(Encoding.ASCII.GetBytes($"\n{line}"));
            }
        }
        
        validStream.Close();
        File.WriteAllText(INVALID_TIPS, invalidLines);
    }

    private static bool TryParseCsvLine(string line, out Tip? tip)
    {
        tip = null;
        
        string[] parts = line.Split(';');
        string id = parts[0];
        int[] nums = new int[parts.Length - 1];
        for (int i = 1; i < parts.Length; i++)
        {
            if (!int.TryParse(parts[i], out nums[i-1])) return false;
        }
        
        tip = new Tip(id, nums);
        return true;
    }

    private static bool TipIsValid(int[] nums)
    {
        bool distinctNumbers = nums.Distinct().Count() == 6;
        bool isValid = nums.All(n => n is >= 1 and <= 45) && distinctNumbers;
        return isValid;
    }
}
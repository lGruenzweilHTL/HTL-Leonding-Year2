namespace HorseRace;

public sealed class Horse
{
    public Horse(string name, int age, int startNo) {
        Age = age;
        Name = name;
        StartNumber = startNo;
    }

    public int Age { get; }
    public string Name { get; }
    public int Position { get; private set; }
    public int Rank { get; set; }
    public int StartNumber { get; }

    /// <summary>
    ///     Compares this <see cref="Horse" />, first by position and then by start number,
    ///     to the supplied <see cref="Horse" />
    /// </summary>
    /// <param name="other"><see cref="Horse" /> to compare to</param>
    /// <returns>The position difference between two horses</returns>
    public int CompareTo(Horse other)
    {
        return Position == other.Position
            ? StartNumber - other.StartNumber
            : Position - other.Position;
    }

    /// <summary>
    ///     Draws the current position of this <see cref="Horse" /> to the console, including
    ///     its label and the finish line.
    /// </summary>
    public void Draw() {
        (int left, int top) = Console.GetCursorPosition();
        
        // Setup labels
        Console.SetCursorPosition(0, top);
        Console.Write($"Horse {StartNumber,2}:");
        left = 10;
        
        // Draw the horse
        Console.SetCursorPosition(left + Position, top);
        Console.Write("🐎");
        
        // Draw the finish line
        Console.SetCursorPosition(left + HorseRace.MAX_STEPS, top);
        Console.Write("🏁");
        
        // Move cursor to next line
        Console.SetCursorPosition(0, top + 1);
    }

    /// <summary>
    ///     Randomly either increases or keeps the position of the <see cref="Horse" />.
    /// </summary>
    public void Move() {
        const float MOVE_CHANCE = 0.5f;
        if (RandomProvider.Random.NextDouble() < MOVE_CHANCE)
        {
            Position++;
        }
    }

    /// <summary>
    ///     Attempts to parse a CSV formatted string to a <see cref="Horse" /> instance.
    ///     Also sets the supplied <see cref="startNo" /> if parsing succeeds.
    /// </summary>
    /// <param name="csvLine">Text to parse</param>
    /// <param name="startNo">Start number to set; must not be negative</param>
    /// <param name="horse">Set to the parsed instance; null if parsing fails</param>
    /// <returns>True if parsed successfully; false otherwise</returns>
    public static bool TryParse(string csvLine, int startNo, out Horse? horse) {
        horse = null;
        
        var parts = csvLine.Split(';');
        string name = parts[0];
        if (string.IsNullOrWhiteSpace(name)) return false;
        if (!int.TryParse(parts[1], out int age)) return false;
        
        horse = new Horse(name, age, startNo);
        return true;
    }
}
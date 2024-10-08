namespace HorseRace;

public sealed class HorseRace
{
    public const int MAX_STEPS = 20;
    private const int DELAY_MILLISECONDS = 100;
    private readonly Horse[] _horses;

    public HorseRace(Horse[] horses)
    {
        _horses = horses;
    }

    private bool IsFinished { get; set; }

    /// <summary>
    ///     Prints the starting list for all horses in this race.
    /// </summary>
    public void PrintStartList()
    {
        var nl = Environment.NewLine;

        var s = "Starting List";
        s += $"{nl}{new string('=', s.Length)}{nl}"
            ;
        foreach (var horse in this._horses)
        {
            s += string.Format($"{horse.StartNumber,3} {horse.Name,-10} {horse.Age,2}{nl}");
        }

        Console.WriteLine(s);
    }

    /// <summary>
    ///     Starts and performs the race by moving and drawing the horses, until at least one
    ///     horse has reached the finish line.
    /// </summary>
    public void PerformRace()
    {
        IsFinished = false;
        while (!IsFinished)
        {
            MoveHorses();
            DrawHorses();
            Thread.Sleep(DELAY_MILLISECONDS);
        }
        AssignRanks();
    }

    /// <summary>
    ///     Prints the race results to the terminal; only if the race is finished.
    /// </summary>
    public void PrintResults() {
        Console.Clear();
        for (int i = 0; i < _horses.Length; i++) {
            string rank = _horses[i].Rank switch {
                1 => "🥇. ",
                2 => "🥈. ",
                3 => "🥉. ",
                _ => $"{_horses[i].Rank}. "
            };
            Console.WriteLine(rank + _horses[i].Name);
        }
    }

    /// <summary>
    ///     Moves all horses and checks, if any of the horses has reached the finish line.
    /// </summary>
    private void MoveHorses()
    {
        if (IsFinished) return;
        
        // Move each horse
        foreach (Horse h in _horses)
        {
            h.Move();
            if (h.Position >= MAX_STEPS)
            {
                IsFinished = true;
                break;
            }
        }
    }

    /// <summary>
    ///     Draws each horse with label, current position and finish line.
    /// </summary>
    private void DrawHorses() {
        Console.Clear();

        foreach (Horse h in _horses) {
            h.Draw();
        }
    }

    /// <summary>
    ///     Assigns ranks to the horses, according to their individual position in the race.
    /// </summary>
    private void AssignRanks() {
        SortByPosition();
        int currRank = 0;
        int lastPos = -1;
        
        foreach (var horse in _horses)
        {
            if (horse.Position != lastPos) currRank++;
            horse.Rank = currRank;
            lastPos = horse.Position;
        }
    }

    /// <summary>
    ///     Sorts the array of horses by position and then by starting number.
    /// </summary>
    private void SortByPosition() {
        // Sort ascending using Selection Sort
        for (var i = 0; i < _horses.Length; i++) {
            int max = i;
            for (var j = i + 1; j < _horses.Length; j++) {
                if (CompareHorses(_horses[j], _horses[max]) > 0) {
                    max = j;
                }
            }
            (_horses[i], _horses[max]) = (_horses[max], _horses[i]);
        }
    }
    
    /// <summary>
    /// Compares two horses by their position and start number. 
    /// </summary>
    /// <param name="h1">The first horse</param>
    /// <param name="h2">The second horse</param>
    /// <returns>The difference in either position or start number (>0 when h1 is bigger, smaller otherwise</returns>
    private int CompareHorses(Horse h1, Horse h2)
    {
        int positionComparison = h1.Position.CompareTo(h2.Position);
        return positionComparison != 0
            ? positionComparison
            : h1.StartNumber.CompareTo(h2.StartNumber);
    }
}
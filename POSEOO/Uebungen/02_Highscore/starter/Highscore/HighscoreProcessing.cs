namespace Highscore;

/// <summary>
///     Processing of gamer scores to determine an overall highscore
/// </summary>
public static class HighscoreProcessing
{
    private const char SEPARATOR = ';';

    /// <summary>
    ///     Executes the <see cref="HighscoreProcessing"/> program.
    /// </summary>
    public static void Run()
    {
        var scores = LoadScoresFromFile("Data/scores.csv");
        var bestScores = FindBestScorePerUser(scores);
        SortGameScores(bestScores);
        WriteOutputFileAndPrint(bestScores, "Data/highscore.csv");
    }

    /// <summary>
    ///     Writes the provided <see cref="GameScore"/> instances to a file at the supplied path.
    ///     The CSV format is Score;Date;Player.
    /// </summary>
    /// <param name="highscore">Game scores to persist</param>
    /// <param name="filePath">Path to write to</param>
    public static void WriteOutputFileAndPrint(GameScore[] highscore, string filePath)
    {
        File.WriteAllText(filePath, "Score;Date;Player\n");
        var scores = highscore.Select(s => $"{s.Score}{SEPARATOR}{s.Date:dd.MM.yyyy}{SEPARATOR}{s.Player.NickName} (#{s.Player.Id})");
        File.AppendAllLines(filePath, scores);
    }

    /// <summary>
    ///     Loads the CSV file at the supplied path and attempts to parse its content to
    ///     create an array of <see cref="GameScore"/> instances utilizing <see cref="TryParseGameScore"/>.
    /// </summary>
    /// <param name="filePath">Path to read from</param>
    /// <returns>Array of parsed items; empty array if file does not exist or is empty</returns>
    public static GameScore[] LoadScoresFromFile(string filePath) {
        if (!File.Exists(filePath)) return Array.Empty<GameScore>();
        
        var lines = File.ReadAllLines(filePath);
        if (lines.Length == 0) return Array.Empty<GameScore>();
        
        
        var scores = new GameScore?[lines.Length];
        
        // The header can be ignored because it will be deleted by the TrimArray method. imo this makes a code a little bit more readable.
        for (var i = 0; i < scores.Length; i++) {
            if (TryParseGameScore(lines[i], out var score)) {
                scores[i] = score!;
            }
            else scores[i] = null;
        }

        return TrimArray(scores);
    }

    /// <summary>
    ///     Attempts to parse a CSV formatted string to a <see cref="GameScore"/> object.
    /// </summary>
    /// <param name="line">String to parse</param>
    /// <param name="gameScore">Successfully parsed object; null if parsing fails</param>
    /// <returns>True if parsed successfully; false otherwise</returns>
    public static bool TryParseGameScore(string line, out GameScore? gameScore)
    {
        gameScore = null;
        
        string[] elements = line.Split(SEPARATOR);
        
        if (elements.Length != 4) return false;
        if (!int.TryParse(elements[0], out int id)) return false;
        if (string.IsNullOrEmpty(elements[1])) return false;
        if (!DateTime.TryParse(elements[2], out DateTime date)) return false;
        if (!int.TryParse(elements[3], out int score)) return false;
        if (id < 0) return false;
        if (score < 0) return false;

        gameScore = new GameScore(new Player(id, elements[1]), score, date);
        return true;
    }

    /// <summary>
    ///     Based on the provided full set of game scores for each user the best score is determined.
    ///     If a user has two or more entries with the same score the oldest (earliest) entry is considered to
    ///     be the highest.
    /// </summary>
    /// <param name="allScores">Collection of all scores; may contain multiple entries for one user</param>
    /// <returns>An array containing the highest score of each user, based on passed scores</returns>
    public static GameScore[] FindBestScorePerUser(GameScore[] allScores)
    {
        return (from score in allScores
                group score by score.Player.Id
                into g
                select g.OrderByDescending(x => x.Score).ThenBy(x => x.Date).First()).ToArray();
    }

    /// <summary>
    ///     Sorts the provided <see cref="GameScore"/> instances so that those with the
    ///     highest score (older date is preferred if score is equal) are ranked first.
    ///     Sorting happens in-place.
    /// </summary>
    /// <param name="scores">Scores to sort</param>
    public static void SortGameScores(GameScore[] scores) {
        for (var i = 0; i < scores.Length; i++) {
            int min = i;
            for (var j = i + 1; j < scores.Length; j++) {
                if (scores[j].Score > scores[min].Score ||
                    scores[j].Score == scores[min].Score && scores[j].Date < scores[min].Date) {
                    min = j;
                }
            }
            (scores[i], scores[min]) = (scores[min], scores[i]);
        }
    }

    /// <summary>
    ///     Trims empty (null) entries from the provided array.
    /// </summary>
    /// <param name="arrayToTrim">The array to process</param>
    /// <returns>A new array containing only the non-null entries from the passed array</returns>
    public static GameScore[] TrimArray(GameScore?[] arrayToTrim) {
        return arrayToTrim.Where(x => x != null).ToArray()!;
    }
}
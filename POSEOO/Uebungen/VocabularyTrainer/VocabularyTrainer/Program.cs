using Figgle;
using System.Text;
using VocabularyTrainer;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine($"*** Vocabulary Trainer ***{Environment.NewLine}");

var trainer = new Trainer(ReadTranslationsFromFile());
var option = 1;
do
{
    switch (option)
    {
        case 1:
            PerformTrainingCycle(trainer);
            break;
        case 2:
            PrintStatistics(trainer);
            break;
        case 3:
            ResetTrainer(trainer);
            break;
        default:
            Console.WriteLine("Invalid option");
            break;
    }

    option = ReadOption();
} while (option != 4);

PrintGoodbye();

static int ReadOption()
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine("(1) Another try?");
    Console.WriteLine("(2) Print Statistics");
    Console.WriteLine("(3) Reset all statistics in Trainer");
    Console.WriteLine("(4) Quit");
    Console.Write("Please select an option: ");
    Console.ResetColor();

    int option;
    string? input;
    do
    {
        input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Invalid input, try again");
        }
    } while (!int.TryParse(input, out option) || option is < 1 or > 4);

    return option;
}

static void PrintGoodbye()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine(
        FiggleFonts.Standard.Render("Bye!"));
    Console.Write("Press any key to finish ... ");
    Console.ResetColor();
    Console.ReadKey();
}

static string[][] ReadTranslationsFromFile()
{
    // we assume the file exists and is valid - this time
    var lines = File.ReadAllLines("Data/translations.csv");
    var words = new string[lines.Length - 1][];
    for (var i = 0; i < words.Length; i++)
    {
        var line = lines[i + 1];
        var parts = line.Split(';');
        words[i] = parts;
    }

    return words;
}

/*Bits and pieces of the UI:
 Console.ForegroundColor = ConsoleColor.DarkYellow;
   Console.Write($"{trainer.CurrentVocabularyItem!.NativeWord,-10} = ");
   Console.ResetColor();

   Console.ForegroundColor = ConsoleColor.Green;
   Console.WriteLine("OK!");

   Console.ForegroundColor = ConsoleColor.Red;
   Console.WriteLine($"No, {trainer.CurrentVocabularyItem!.NativeWord} = {correctTranslation}");
 */
static void PerformTrainingCycle(Trainer trainer)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine(FiggleFonts.Standard.Render("Vocabulary Trainer"));
    Console.WriteLine($"Starting a new training cycle with {Trainer.CYCLE_COUNT} tries ...");

    trainer.ResetCycle();
    Console.ResetColor();
    bool hasMoreWords = true;
    int cycleCount = 1;
    while (hasMoreWords)
    {
        //TODO: Implement the training cycle
        hasMoreWords = trainer.GetNextWord();
        Console.Write($"{cycleCount++}. {trainer.CurrentVocabularyItem!.NativeWord} {"=",5} ");
        //Use function RequestTranslation for user input
        var userInput = RequestTranslation();


        bool correct = trainer.TestTranslation(userInput, out string correctTranslation);
        if (correct)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("OK!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No. {trainer.CurrentVocabularyItem!.NativeWord} = {correctTranslation}");
        }

        Console.ResetColor();
    }
}

/*
 Bits and pieces of the UI:
  Console.Clear();
          Console.ForegroundColor = ConsoleColor.DarkCyan;
          Console.WriteLine(FiggleFonts.Standard.Render("Training Statistics"));
          Console.WriteLine($"{"English",-10} {"German",-10} {"Asked",-5} {"Correct",-7}");
          Console.WriteLine(new string('-', 35));
          Console.ForegroundColor = ConsoleColor.DarkYellow;
 */
static void PrintStatistics(Trainer trainer)
{
    Console.WriteLine($"{"Native Word",-10} {"Translation",-10} {"Asked",5} {"Correct",7}");
    Console.WriteLine(string.Join("\n", trainer.GetSortedItems().Select(i => i.ToString())));

    Console.WriteLine();
    Console.ResetColor();
}

static void ResetTrainer(Trainer trainer)
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine(FiggleFonts.Standard.Render("Reset Trainer?"));
    Console.WriteLine("Are you sure you want to reset the trainer? (Y/N)");

    var input = Console.ReadLine();
    if (input?.ToUpper() == "Y")
    {
        trainer.Reset();
        Console.WriteLine("Trainer reset.");
    }
    else
    {
        Console.WriteLine("Reset cancelled.");
    }
}

static string RequestTranslation()
{
    string? userInput;
    do
    {
        userInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(userInput))
        {
            Console.WriteLine("Invalid input, try again");
            userInput = null;
        }
    } while (userInput == null);

    return userInput;
}
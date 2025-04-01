using Logic;
using SoundDataParser;
using Tools;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World of Sound!");
        
        CommandLineArgs commandLineArgs = new();
        commandLineArgs.FromArray(args);

        if(!commandLineArgs.IsValid())
        {
            Console.WriteLine("Please provide parameter -d pathToSoundFile.");
            return;
        }
        var pathToSoundFile = args[1];
        if(!File.Exists(pathToSoundFile))
        {
            Console.WriteLine($"File {pathToSoundFile} not found.");
            return;
        }
        
        var soundCategories = CsvReader.ReadCsv(pathToSoundFile);
        Process(commandLineArgs, soundCategories);
    }

    private static void Process(CommandLineArgs args, SoundCategory[] soundCategories)
    {
        SoundMixer soundMixer = new();
        
        if (args.listInstruments)
        {
            Console.WriteLine("List of sound categories:");
            foreach (var soundCategory in soundCategories)
            {
                Console.WriteLine(soundCategory.Name);  
            }
            return;
        }
        
        foreach (var argsSourceInfo in args.sourceInfos)
        {
            var soundCategory = soundCategories.FirstOrDefault(x => x.Name == argsSourceInfo);
            if (soundCategory == null)
            {
                Console.WriteLine($"Sound category {argsSourceInfo} not found.");
            }

            Console.WriteLine(SoundMixer.AnalyzeProducer(soundCategory as ISoundProducer));
        }
        
        foreach (var argsMixture in args.mixture)
        {
            var soundCategory = soundCategories.FirstOrDefault(x => x.Name == argsMixture);
            soundMixer += soundCategory as ISoundProducer;
        }
        
        if (args.analyzeMixture) 
        {
            Console.WriteLine(soundMixer.Analyze());
        }
    }
}

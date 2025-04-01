namespace Tools;

public class CommandLineArgs
{
    public string pathToData; // Mandatory
    public bool listInstruments; // list names of instruments and quit
    public List<string> sourceInfos = new(); // list information about sound source, multiple sources allowed
    public List<string> mixture = new(); // add sound from csv to mixture, multiple sounds allowed
    public bool analyzeMixture; // analyze mixture and quit
    
    public bool IsValid()
    {
        return !string.IsNullOrEmpty(pathToData);
    }
    
    public void FromArray(string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-d":
                    pathToData = args[++i];
                    break;
                case "-l":
                    listInstruments = true;
                    break;
                case "-i":
                    sourceInfos.Add(args[++i]);
                    break;
                case "-a":
                    mixture.Add(args[++i]);
                    break;
                case "-m":
                    analyzeMixture = true;
                    break;
            }
        }
    }
}
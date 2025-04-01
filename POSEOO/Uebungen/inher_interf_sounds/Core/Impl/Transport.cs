namespace SoundDataParser;

public class Transport: SoundCategory, ISoundEmitter
{
    public Transport(string name, string low, string high, string highest, string loudness, string spectrum) : base(name)
    {
        Low = low;
        High = high;
        Highest = highest;
        Loudness = loudness;
        Spectrum = spectrum;
    }

    public string Low { get; }
    public string High { get; }
    public string Highest { get; }
    public string Loudness { get; }
    public string Spectrum { get; }
    public Sound MakeSound()
    {
        throw new NotImplementedException();
    }

    public Sound MakeSound(MusicalNote n)
    {
        throw new NotImplementedException();
    }
}
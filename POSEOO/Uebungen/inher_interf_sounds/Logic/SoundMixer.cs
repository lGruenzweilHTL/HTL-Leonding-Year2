using SoundDataParser;

namespace Logic;

public class SoundMixer
{
    private List<ISoundProducer> _soundProducers = new();
    public static SoundMixer operator +(SoundMixer sm, ISoundProducer sc)
    {
        sm._soundProducers.Add(sc);
        return sm;
    }

    public override string ToString()
    {
        return Analyze();
    }
    
    public string Analyze()
    {
        string result = "Sounds:\n";
        foreach (var soundProducer in _soundProducers)
        {
            result += AnalyzeProducer(soundProducer);
        }
        
        return result;
    }
    
    public static string AnalyzeProducer(ISoundProducer soundProducer)
    {
        string result = "";
        if (soundProducer is ISoundEmitter soundEmitter)
        {
            result += $"Sound: {soundEmitter.Name}\n";
        }
        if (soundProducer is ITonalQuality tonalQuality)
        {
            result += $"Low: {tonalQuality.Low}\nHigh: {tonalQuality.High}\nHighest: {tonalQuality.Highest}\nLoudness: {tonalQuality.Loudness}\n";
        }
        if (soundProducer is ISpectralQuality spectralQuality)
        {
            result += $"Spectrum: {spectralQuality.Spectrum}";
        }

        result += "\n\n";
        
        return result;
    }
}
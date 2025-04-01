namespace SoundDataParser;

public abstract class SoundCategory
{
    public string Name { get; init; }
    
    
    protected SoundCategory(string name)
    {
        Name = name;
    }
    
    //Create from Name;Category;Low;High;Highest;Spectrum;Loudness
    public static SoundCategory CreateFromCsv(string[] data)
    {
        if (data.Length != 7)
        {
            throw new ArgumentException("Invalid data length");
        }
        
        var name = data[0];
        var category = data[1];
        var low = data[2];
        var high = data[3];
        var highest = data[4];
        var spectrum = data[5];
        var loudness = data[6];

        return category switch
        {
            "Electronic" => new Electronic(name, low, high, highest, loudness, spectrum),
            "Nature" => new Nature(name, low, high, highest, loudness, spectrum),
            "Percussion" => new Percussion(name, low, high, highest, loudness, spectrum),
            "Stringed Instrument" => new StringedInstrument(name, low, high, highest, loudness, spectrum),
            "Tool" => new Tool(name, low, high, highest, loudness, spectrum),
            "Transport" => new Transport(name, low, high, highest, loudness, spectrum),
            "Woodwind" => new Woodwind(name, low, high, highest, loudness, spectrum),
            _ => new DummySoundCategory()
        };
    }
}

public class DummySoundCategory : SoundCategory
{
    public DummySoundCategory() : base("Dummy")
    {
    }
}
namespace SoundDataParser;

public interface ITonalQuality : ISoundQuality
{
    string Low { get; }
    string High { get; }
    string Highest { get; }
    string Loudness { get; }
}
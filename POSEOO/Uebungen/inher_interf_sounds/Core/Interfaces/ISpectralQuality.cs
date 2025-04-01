namespace SoundDataParser;

public interface ISpectralQuality : ISoundQuality
{
    string Spectrum { get; }
}
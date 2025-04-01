namespace SoundDataParser;

public interface ISoundEmitter : ITonalQuality, ISpectralQuality, ISoundProducer
{
    string Name { get; }
}
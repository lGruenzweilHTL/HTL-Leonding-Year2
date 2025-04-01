namespace SoundDataParser;

public interface ISoundProducer
{
    Sound MakeSound();
    Sound MakeSound(MusicalNote n);
}
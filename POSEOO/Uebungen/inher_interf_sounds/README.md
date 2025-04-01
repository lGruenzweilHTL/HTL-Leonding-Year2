# **Object-Oriented Model with Interfaces: Sounds of the World **

## **Introduction**

We are surrounded by sound: Everything that moves emits a sound, since sound is a pressure wave through a medium. In addition to the natural sounds, humans have invented a myriad of things. Some of these even deliberately make sounds that are pleasant to our ears, we call these *musical instruments*. Some other inventions, e.g. our cars and machines, emit rather unpleasant *noise*.

Now imagine a street musician playing on a busy inner city crossing - what kind of sound would that be? What are the qualities of the total sound of the instrument, the city noise and the bus driving by?

## Noise Mixer: Application to mix sounds

### Purpose of Noise Mixer

The application *Noise Mixer* shall be able to

1) compose a mixture of sound emitting devices
1) calculate the properties of the total sound
1) describe the sound in a human understandable way

The basis for these tasks is a data file of sound properties of various instruments and objects. This data is available as CSV with the following columns:

- *Name*: Unique name of the sound emitting thing, e.g. **Violin** or **Drillhammer**
- *Category*: Unique name of the category:  **Stringed Instrument** or **Tool** or **Transport** or **Nature**
- *Low*: Lowest sound emitted, in musical notation (e.g. C0)
- *High*: Highest sound emitted using normal means, in musical notation (e.g. A4)
- *Highest*: Highest sound emitted using special techniques, e.g. overblowing.
- *Spectrum*: Can be odd, even or all. Denotes which overtones are present.
- *Loudness*: Can be low, middle, high, very high, deafening.

### User Interface

The application *Noise Mixer* shall presents a command line driven user interface.

It accepts the following command line options:

- *-d path* Path to data file for sounds. Mandatory
- *-l* List all Names of Instruments in data file and quit.
- *-i name* Print information about Sound source. Can be given multiple times.
- *-a name* Add sound source to mixture. Can be given multiple times.
- *-m identifier* Analyze the Sound of the mixture of the given *-a* sound sources. Mixture is called *identifier*

## Development tasks

### Solution layout

The Noise Mixer consists of the following projects in the solution *noiseMixer.sln*:

- *Core*: Contains the OO-model of the sound sources.
- *Tools*: Contains the CSV-Reader and other tools.
- *Logic*: Contains the sound analyzing components.
- *NoiseMixer*: Contains the command line application.
- *NoiseMixerTests*: Unit Tests for the OO-Model, Tools, and Logic.

### OO Design Guidelines

The Noise Mixer application shall be developed along the following guidelines:

- Each *Category* of sound sources forms an object oriented hierarchy of those things. The application knows a fixed number of elements in each hierarchy.
- The qualities of a sound is expressed using interfaces:
 - *ISoundQuality*: Base interface, empty.
 - *ITonalQuality*: Range Low, High and Loudness. Derives from *ISoundQuality*.
  - *ISpectralQuality*: Properties of spectrum. Derives from *ISoundQuality*.
- The production of sound is defined in *ISoundProducer*: It defines 2 methods:
    - `Sound MakeSound()`
    - `Sound MakeSound(MusicalNote n)`

Each class in the hierarchy of *Category* implements these interfaces.

For composing a sound scenario, a class *SoundComposition* is defined:

- It holds *ISoundProducer* objects.
- It overrides `operator +=(ISoundProducer p)` and `operator -=(SoundProducer p)` to add and remove sound producing objects from its collection.
- It implements *ISoundProducer*, *ITonalQuality* and *ISpectralQuality* as well, returning the combined sound of its collection.

To get human readable information about a sound producing object, a class *SoundDescriptionGenerator* is defined. It has a method `public string Describe(ISoundQuality s)` that returns a human readable translation of the information accessible through *ITonalQuality* and *ISpectralQuality*.


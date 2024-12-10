namespace Marathons;

public class Participant
{
    public int StartNo { get; }
    public string Name { get; }
    public TimeSpan CompletionTime { get; }

    public Participant(int startNo, string name, TimeSpan completionTime)
    {
        StartNo = startNo;
        Name = name;
        CompletionTime = completionTime;
    }

    public int CompareTo(Participant other)
    {
        int diff = CompletionTime.CompareTo(other.CompletionTime);
        if (diff != 0) return diff;
        
        diff = StartNo.CompareTo(other.StartNo);
        return diff;
    }

    public override string ToString()
    {
        return $@"{Name} (Start# {StartNo:D3}) finished in {CompletionTime:hh\:mm\:ss}";
    }
}
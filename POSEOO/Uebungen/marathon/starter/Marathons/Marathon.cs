using System.Drawing;

namespace Marathons;

public class Node(Participant p)
{
    public Participant Data = p;
    public Node? Next;
}
public class Marathon
{
    private Node? _head;
    
    public string City { get; }
    public DateOnly Date { get; }
    public int ParticipantCount { get; private set; }

    public Marathon(string city, DateOnly date)
    {
        City = city;
        Date = date;
    }

    public void AddParticipant(Participant p)
    {
        Node n = new Node(p);
        if (_head == null)
        {
            _head = n;
            ParticipantCount = 1;
            return;
        }
        
        // Walk through list until in order
        // New addition will be between previous and next
        Node? next = _head;
        Node? previous = null;
        while (next != null && p.CompareTo(next.Data) > 0)
        {
            previous = next;
            next = next.Next;
        }
        
        // Relocate pointers
        if (previous == null) _head = n;
        else previous.Next = n;
        n.Next = next;

        ParticipantCount++;
    }

    public bool RemoveParticipant(int startNo)
    {
        // Retrieve Participant at idx
        Node? previous = null;
        Node? target = _head;
        bool found = false;
        while (target != null)
        {
            // Check if target was found
            if (target.Data.StartNo == startNo)
            {
                found = true;
                break;
            }
            
            // Keep looking
            previous = target;
            target = target.Next;
        }
        if (!found) return false;
        
        // Relocate pointers
        if (previous == null) _head = previous; // Target node is head
        else previous.Next = target?.Next;
        GC.Collect(); // be a good citizen
        
        ParticipantCount--;
        return true;
    }

    public string[] GetResultList()
    {
        if (ParticipantCount == 0) return Array.Empty<string>();
        
        string[] result = new string[ParticipantCount];

        Node curr = _head!;
        for (int i = 1; i <= ParticipantCount; i++) 
        {
            result[i - 1] = $"#{i:D2} {curr.Data}";
            curr = curr.Next!;
        }

        return result;
    }

    public override string ToString()
    {
        return $"{City} marathon on {Date:dd.MM.yyyy}";
    }
}

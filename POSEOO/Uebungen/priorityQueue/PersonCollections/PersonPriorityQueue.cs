namespace PersonCollections;

//Holds Person objects in a priority queue
//The priority is determined by an additional integer parameter given on enqueue.
//Lower number means higher priority.
public class PersonPriorityQueue /*<TData, TPrio> where TPrio : IComparable<TPrio>*/
{
    //No one ever needs to see this
    //It's a private class that represents a node in the linked list
    //Therefore, we define this type inside the PersonPriorityQueue class
    private class PersonNode
    {
        public Person Obj { get; set; }
        public int Priority { get; set; }
        public PersonNode? Next { get; set; }
    }

    private PersonNode? head;
    private int count;

    public void Enqueue(Person? obj, int priority = 0)
    {
        if (obj == null || priority < 0) return;

        var newNode = new PersonNode { Obj = obj, Priority = priority };
        if (head == null || head.Priority > priority)
        {
            newNode.Next = head;
            head = newNode;
        }
        else
        {
            var current = head;
            while (current.Next != null && current.Next.Priority <= priority)
            {
                current = current.Next;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
        }

        count++;
    }

    public Person? Peek() => head?.Obj;

    public Person? Dequeue()
    {
        var obj = head?.Obj;

        if (head != null)
        {
            head = head.Next;
            count--;
        }

        return obj;
    }

    public bool TryPeek(out Person? obj,out int priority)=>(priority=head?.Priority??-1,obj=Peek())!=(-1,null);

    public bool TryDequeue(out Person? obj, out int priority)
    {
        var result = TryPeek(out obj, out priority);
        if (result) Dequeue();
        return result;
    }

    public void Clear()
    {
        head = null;
        count = 0;
        GC.Collect(); // be a good citizen and clean up after yourself
    }

    public int Count => count;
}
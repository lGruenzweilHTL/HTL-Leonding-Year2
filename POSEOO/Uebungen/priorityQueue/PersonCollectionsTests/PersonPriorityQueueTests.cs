using PersonCollections;

namespace PersonCollectionsTests;

public class PersonPriorityQueueTests
{
    [Fact]
    public void CreateEmpty_HasEmptyProperties()
    {
        var target = new PersonPriorityQueue();
        Assert.Equal(0, target.Count);
        Assert.Null(target.Peek());
        Assert.Null(target.Dequeue());
        Assert.False(target.TryPeek(out _, out _));
        Assert.False(target.TryDequeue(out _, out _));
    }

    [Fact]
    public void EnqueueOneItem_HasOneItem()
    {
        var target = new PersonPriorityQueue();
        var person = new Person { FirstName = "John", LastName = "Doe" };
        target.Enqueue(person, 1);
        var peekResult = target.Peek();
        var tryPeekResult = target.TryPeek(out var tryPeekPerson, out var tryPeekPriority);

        Assert.Equal(1, target.Count);
        Assert.Same(person, peekResult);
        Assert.True(tryPeekResult);
        Assert.Same(person, tryPeekPerson);
        Assert.Equal(1, tryPeekPriority);
    }
    
    [Fact]
    public void EnqueueThreeItems_HasThreeItemsInOrder()
    {
        var target = new PersonPriorityQueue();
        var person1 = new Person { FirstName = "John", LastName = "Doe" };
        var person2 = new Person { FirstName = "Jane", LastName = "Smith" };
        var person3 = new Person { FirstName = "Jim", LastName = "Brown" };
        target.Enqueue(person1, 1);
        target.Enqueue(person2, 2);
        target.Enqueue(person3, 3);
        
        var peekResult = target.Peek();
        var tryPeekResult = target.TryPeek(out var tryPeekPerson, out var tryPeekPriority);
        
        Assert.Equal(3, target.Count);
        Assert.Same(person1, peekResult);
        Assert.True(tryPeekResult);
        Assert.Same(person1, tryPeekPerson);
        Assert.Equal(1, tryPeekPriority);
        
        var dequeueResult1 = target.Dequeue();
        Assert.Same(person1, dequeueResult1);
        
        var dequeueResult2 = target.Dequeue();
        Assert.Same(person2, dequeueResult2);
        
        var dequeueResult3 = target.Dequeue();
        Assert.Same(person3, dequeueResult3);
    }

    [Fact]
    public void Enque4Items_ReversePriority_OrderIsReversed()
    {
        var target = new PersonPriorityQueue();
        var person1 = new Person { FirstName = "John", LastName = "Doe" };
        var person2 = new Person { FirstName = "Jane", LastName = "Smith" };
        var person3 = new Person { FirstName = "Jim", LastName = "Brown" };
        var person4 = new Person { FirstName = "Jack", LastName = "Black" };
        target.Enqueue(person1, 3);
        target.Enqueue(person2, 2);
        target.Enqueue(person3, 1);
        target.Enqueue(person4, 1);
        
        var peekResult = target.Peek();
        var tryPeekResult = target.TryPeek(out var tryPeekPerson, out var tryPeekPriority);
        
        Assert.Equal(4, target.Count);
        Assert.Same(person3, peekResult);
        Assert.True(tryPeekResult);
        Assert.Same(person3, tryPeekPerson);
        Assert.Equal(1, tryPeekPriority);
        
        var dequeueResult1 = target.Dequeue();
        Assert.Same(person3, dequeueResult1);
        
        var dequeueResult4 = target.Dequeue();
        Assert.Same(person4, dequeueResult4);
        
        var dequeueResult2 = target.Dequeue();
        Assert.Same(person2, dequeueResult2);
        
        var dequeueResult3 = target.Dequeue();
        Assert.Same(person1, dequeueResult3);
    }

    [Fact]
    public void Enqueue3Items_AddOneMoreOfSamePriority_QueueInOrder()
    {
        var target = new PersonPriorityQueue();
        var person1 = new Person { FirstName = "John", LastName = "Doe" };
        var person2 = new Person { FirstName = "Jane", LastName = "Smith" };
        var person3 = new Person { FirstName = "Jim", LastName = "Brown" };
        target.Enqueue(person1, 1);
        target.Enqueue(person2, 2);
        target.Enqueue(person3, 3);
        var person4 = new Person { FirstName = "Jack", LastName = "Black" };
        target.Enqueue(person4, 1);
        var person5 = new Person { FirstName = "Jill", LastName = "White" };
        target.Enqueue(person5, 2);
        var person6 = new Person { FirstName = "Joe", LastName = "Green" };
        target.Enqueue(person6, 3);

        {
            var dequeue = target.Dequeue();
            Assert.Same(person1, dequeue);
        }
        {
            var dequeue = target.Dequeue();
            Assert.Same(person4, dequeue);
        }
        {
            var dequeue = target.Dequeue();
            Assert.Same(person2, dequeue);
        }
        {
            var dequeue = target.Dequeue();
            Assert.Same(person5, dequeue);
        }
        {
            var dequeue = target.Dequeue();
            Assert.Same(person3, dequeue);
        }
        {
            var dequeue = target.Dequeue();
            Assert.Same(person6, dequeue);
        }
        
    }
}
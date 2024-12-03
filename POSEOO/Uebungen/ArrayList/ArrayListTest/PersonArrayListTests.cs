using ArrayListConsoleApp;

namespace ArrayListTest;

public class PersonArrayListTests
{
    [Fact]
    public void CreateDefault_ShouldBeEmpty()
    {
        var personArrayList = new PersonArrayList();
        
        Person p = new Person { FirstName = "John", LastName = "Doe" };
        int actualCount = personArrayList.Count;
        int actualIndex = personArrayList.IndexOf(p);
        bool actualContains = personArrayList.Contains(p);
        
        // Assert
        Assert.Equal(0, actualCount);
        Assert.Equal(-1, actualIndex);
        Assert.False(actualContains);
    }

    [Fact]
    public void CreateDefault_AddOnePerson_ShouldContainOnePerson()
    {
        var personArrayList = new PersonArrayList();
        
        Person p = new Person { FirstName = "John", LastName = "Doe" };
        personArrayList.Add(p);
        int actualCount = personArrayList.Count;
        int actualIndex = personArrayList.IndexOf(p);
        bool actualContains = personArrayList.Contains(p);
        
        // Assert
        Assert.Equal(1, actualCount);
        Assert.Equal(0, actualIndex);
        Assert.True(actualContains);
    }

    [Fact]
    public void CreateDefault_Add3Persons_PersonsAreInOrder()
    {
        var personArrayList = new PersonArrayList();
        
        Person p1 = new Person { FirstName = "John", LastName = "Doe" };
        Person p2 = new Person { FirstName = "Jane", LastName = "Doe" };
        Person p3 = new Person { FirstName = "Jim", LastName = "Doe" };
        
        personArrayList.Add(p1);
        personArrayList.Add(p2);
        personArrayList.Add(p3);
        
        var indexOf1 = personArrayList.IndexOf(p1);
        var indexOf2 = personArrayList.IndexOf(p2);
        var indexOf3 = personArrayList.IndexOf(p3);
        var contains1 = personArrayList.Contains(p1);
        var contains2 = personArrayList.Contains(p2);
        var contains3 = personArrayList.Contains(p3);

        Assert.Equal(3, personArrayList.Count);
        Assert.True(contains1);
        Assert.True(contains2);
        Assert.True(contains3);
        Assert.Equal(0, indexOf1);
        Assert.Equal(1, indexOf2);
        Assert.Equal(2, indexOf3);
    }

    [Fact]
    public void CreateDefault_Add3Persons_InsertAt1_Has4PersonsInOrder()
    {
        var personArrayList = new PersonArrayList();
        
        Person p1 = new Person { FirstName = "John", LastName = "Doe" };
        Person p2 = new Person { FirstName = "Jane", LastName = "Doe" };
        Person p3 = new Person { FirstName = "Jim", LastName = "Doe" };
        Person p4 = new Person { FirstName = "Jim", LastName = "Don't" };
        
        personArrayList.Add(p1);
        personArrayList.Add(p2);
        personArrayList.Add(p3);
        
        personArrayList.Insert(1, p4);
        
        var indexOf1 = personArrayList.IndexOf(p1);
        var indexOf2 = personArrayList.IndexOf(p2);
        var indexOf3 = personArrayList.IndexOf(p3);
        var indexOf4 = personArrayList.IndexOf(p4);
        var contains1 = personArrayList.Contains(p1);
        var contains2 = personArrayList.Contains(p2);
        var contains3 = personArrayList.Contains(p3);
        var contains4 = personArrayList.Contains(p4);

        Assert.Equal(4, personArrayList.Count);
        Assert.True(contains1);
        Assert.True(contains2);
        Assert.True(contains3);
        Assert.True(contains4);
        Assert.Equal(0, indexOf1);
        Assert.Equal(2, indexOf2);
        Assert.Equal(3, indexOf3);
        Assert.Equal(1, indexOf4);
    }

    [Fact]
    public void CreateDefault_Add3Persons_AccessByIndexer()
    {
        var personArrayList = new PersonArrayList();
        
        Person p1 = new Person { FirstName = "John", LastName = "Doe" };
        Person p2 = new Person { FirstName = "Jane", LastName = "Doe" };
        Person p3 = new Person { FirstName = "Jim", LastName = "Doe" };
        var p4 = new Person { FirstName = "Jim", LastName = "Don't" };
        
        personArrayList.Add(p1);
        personArrayList.Add(p2);
        personArrayList.Add(p3);
        personArrayList[1] = p4;

        Assert.Equal(3, personArrayList.Count);
        Assert.Equal(p1, personArrayList[0]);
        Assert.Equal(p4, personArrayList[1]);
        Assert.Equal(p3, personArrayList[2]);
    }

    [Fact]
    public void CreateDefault_Add3Persons_Remove2Person_ContainsOnePerson()
    {
        var personArrayList = new PersonArrayList();
        
        Person p1 = new Person { FirstName = "John", LastName = "Doe" };
        Person p2 = new Person { FirstName = "Jane", LastName = "Doe" };
        Person p3 = new Person { FirstName = "Jim", LastName = "Doe" };
        
        personArrayList.Add(p1);
        personArrayList.Add(p2);
        personArrayList.Add(p3);
        
        personArrayList.Remove(p1);
        personArrayList.Remove(p2);

        Assert.Equal(1, personArrayList.Count);
        Assert.Equal(p3, personArrayList[0]);
    }

    [Fact]
    public void CreateDefault_Add3Persons_RemoveAt1_Has2PersonsInOrder()
    {
        var personArrayList = new PersonArrayList();
        
        Person p1 = new Person { FirstName = "John", LastName = "Doe" };
        Person p2 = new Person { FirstName = "Jane", LastName = "Doe" };
        Person p3 = new Person { FirstName = "Jim", LastName = "Doe" };
        
        personArrayList.Add(p1);
        personArrayList.Add(p2);
        personArrayList.Add(p3);
        
        personArrayList.RemoveAt(1);
        
        Assert.Equal(2, personArrayList.Count);
        Assert.Equal(p1, personArrayList[0]);
        Assert.Equal(p3, personArrayList[1]);
    }
}
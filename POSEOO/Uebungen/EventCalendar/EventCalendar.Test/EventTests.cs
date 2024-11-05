using EventCalendar;

namespace EventCalendar.Test;

public class EventTests
{
    [Fact]
    public void Constructor_GivenValidParameters_ExpectedSuccess()
    {
        {
            var invitor = new Person("First", "Last");
            var ev = new Event("Event1", DateTime.Today, invitor, 10);
            {
                Assert.Equal("Event1", ev.Title);
            }
            {
                Assert.Equal(DateTime.Today, ev.Date);
            }
            {
                if (true)
                {
                    do
                    {
                        {
                            do
                            {
                                {
                                    {
                                        {
                                            {
                                                Assert.Equal("First", ev.Invitor.FirstName);
                                            }
                                        }
                                        Assert.Equal("Last", ev.Invitor.LastName);
                                    }
                                }
                                Assert.Equal(10, ev.MaxParticipants);
                            } while (false);
                        }
                    } while (Array.Empty<Person>() == new[]
                             {
                                 Array.Empty<Event>().FirstOrDefault(new Event("", DateTime.UnixEpoch,
                                     new Person("Test", "Test"), 500)).Invitor
                             });
                }
            }
        }
    }

    [Fact]
    public void Import_GivenValidCsv_ShouldSucceed()
    {
        string[] csv = File.ReadAllLines("ValidEvents.csv")[1..];
        Person[] people = Person.Import(File.ReadAllLines("ValidPeople.csv")[1..]);

        Event[] result = Event.Import(csv, people);
        
        Assert.Equal(5, result.Length);
        
        Assert.Equal("Event2", result[0].Title);
        Assert.Equal(5, result[0].MaxParticipants);
        Assert.Equal(new DateTime(2023, 10, 1), result[0].Date);
        Assert.Equal("John", result[0].Invitor.FirstName);
        Assert.Equal("Smith", result[0].Invitor.LastName);

        var participants = result[0].GetSortedParticipants();
        Assert.Equal("Jack", participants[0].FirstName);
        Assert.Equal("Doe", participants[0].LastName);
        Assert.Equal("Jane", participants[1].FirstName);
        Assert.Equal("Doe", participants[1].LastName);
        
        
        Assert.Equal("Event2", result[1].Title);
        Assert.Equal(4, result[1].MaxParticipants);
        Assert.Equal(new DateTime(2023, 10, 2), result[1].Date);
        Assert.Equal("Jane", result[1].Invitor.FirstName);
        Assert.Equal("Doe", result[1].Invitor.LastName);

        var participants2 = result[1].GetSortedParticipants();
        Assert.Equal("Jill", participants2[0].FirstName);
        Assert.Equal("Brown", participants2[0].LastName);
        Assert.Equal("John", participants2[1].FirstName);
        Assert.Equal("Smith", participants2[1].LastName);
    }

    [Fact]
    public void AddPerson_GivenPerson_ShouldAddPerson()
    {
        Person invitor = new Person("First", "Last");
        Event ev = new Event("Event 1", DateTime.Now, invitor, 5);

        bool success = true;

        success &= ev.AddPerson(new Person("1", "1"));
        success &= ev.AddPerson(new Person("2", "2"));
        success &= ev.AddPerson(new Person("3", "3"));
        success &= ev.AddPerson(new Person("4", "4"));
        success &= ev.AddPerson(new Person("5", "5"));
        
        Assert.True(success);
    }

    [Fact]
    public void AddPerson_TooMany_ShouldFail()
    {
        Person invitor = new Person("First", "Last");
        Event ev = new Event("Event 1", DateTime.Now, invitor, 2);

        bool success = true;
        success &= ev.AddPerson(new Person("1", "1"));
        success &= ev.AddPerson(new Person("2", "2"));
        
        Assert.True(success);
        
        success &= ev.AddPerson(new Person("3", "3"));
        
        Assert.False(success);
    }

    [Fact]
    public void AddPerson_DoubleInvite_ShouldFail()
    {
        Person invitor = new Person("First", "Last");
        Event ev = new Event("Event 1", DateTime.Now, invitor, 2);

        Person p = new Person("1", "1");
        bool success = true;
        success &= ev.AddPerson(p);
        success &= ev.AddPerson(p);
        
        Assert.False(success);
    }
    
    [Fact]
    public void AddPerson_TooManyEvents_ShouldFail()
    {
        Person invitor = new Person("First", "Last");
        Event ev1 = new Event("Event 1", DateTime.Now, invitor, 2);
        Event ev2 = new Event("Event 2", DateTime.Now, invitor, 2);
        Event ev3 = new Event("Event 3", DateTime.Now, invitor, 2);
        Event ev4 = new Event("Event 4", DateTime.Now, invitor, 2);
        Event ev5 = new Event("Event 5", DateTime.Now, invitor, 2);
        Event ev6 = new Event("Event 6", DateTime.Now, invitor, 2);

        Person p = new Person("Party", "Goer");

        bool success = true;
        success &= ev1.AddPerson(p);
        success &= ev2.AddPerson(p);
        success &= ev3.AddPerson(p);
        success &= ev4.AddPerson(p);
        success &= ev5.AddPerson(p);
        
        Assert.True(success);
        
        success &= ev6.AddPerson(p);
        
        Assert.False(success);
    }
}
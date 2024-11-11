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
    public void Import_PartlyValid_ReturnsValid()
    {
        string[] csv =
        [
            "Event1; 2023-10-01; 5; Smith; John; Doe Jack, Doe Jane",
            "Event2; 2023-10-02; 5; Doe; Jane; Brown Jill, Smith John",
            "Event3; 2023-10-03; 5; Doe; Jack; Brown Jill, Smith John",
            "Event4; 2023-10-04; 5; Doe; Jane; Brown Jill, Smith John",
            "Event5; 2023-10-05; 5; Doe; Jack; Brown Jill, Smith John"
        ];
        Person[] people =
        [
            new Person("John", "Smith"),
            new Person("Jane", "Doe"),
            new Person("Jack", "Doe"),
            new Person("Jill", "Brown"),
        ];
        Event[] expected =
        [
            new Event("Event1", new DateTime(2023, 10, 1), people[0], 5),
            new Event("Event2", new DateTime(2023, 10, 2), people[1], 5),
            new Event("Event3", new DateTime(2023, 10, 3), people[2], 5),
            new Event("Event4", new DateTime(2023, 10, 4), people[1], 5),
            new Event("Event5", new DateTime(2023, 10, 5), people[2], 5)
        ];
        Event[] events = Event.Import(csv, people);

        Assert.Equal(expected.Length, events.Length);
        for (var i = 0; i < events.Length; i++)
        {
            Assert.Equal(expected[i].Title, events[i].Title);
            Assert.Equal(expected[i].Date, events[i].Date);
            Assert.Equal(expected[i].Invitor, events[i].Invitor);
            Assert.Equal(expected[i].MaxParticipants, events[i].MaxParticipants);
            Assert.Equal(2, events[i].GetSortedParticipants().Length);
        }
    }

    [Fact]
    public void Import_AllInvalid_ReturnsEmpty()
    {
        string[] csv =
        [
            "Event1; 2023-10-01; 1; Smith; John; Doe Jack, Doe Jane", // Too many Participants
            "", // Everything missing
            "Event5; 2023-10-05; 5; Doe; Jack; Brown John, Smith Jill", // Invalid participants,
            "Event6; invalid; 5; Doe; Jack;", // Invalid Date
            "Event7; 2023-10-07; invalid; Doe; Jack;", // Invalid Max Participants
        ];
        Person[] people =
        [
            new Person("John", "Smith"),
            new Person("Jane", "Doe"),
            new Person("Jack", "Doe"),
            new Person("Jill", "Brown"),
        ];
        Event[] events = Event.Import(csv, people);

        Assert.Empty(events);
    }

    [Fact]
    public void Import_EdgeCasesAllValid_ReturnsValid()
    {
        string[] csv =
        [
            "Event1; 2023-10-01; 5; Smith; John; Doe Jane, Doe Jack",
            "Event2; 2023-10-02; 4; Doe; Jane; Smith John, Brown Jill",
            "Event3; 2023-10-03; 3; Doe; Jack;",
            "Event4; 2023-10-04; 2; Smith; John; Doe Jane, White Gillian",
            "Event5; 2023-10-05; 1; Doe; Jane;"
        ];
        Person[] people = new Person[]
        {
            new Person("John", "Doe"),
            new Person("Jane", "Doe", "jane@doe.com"),
            new Person("Mary", "Smith", "mary@smith.com", "123456789"),
            new Person("Jill", "Brown", "", "987654321"),
            new Person("Gillian", "White", "gw@white.com", "123456789")
        };
        Event[] expected =
        [
            new Event("Event5", new DateTime(2023, 10, 5), people[1], 1)
        ];
        
        
        Event[] events = Event.Import(csv, people);

        Assert.Equal(expected.Length, events.Length);
        for (var i = 0; i < events.Length; i++)
        {
            Assert.Equal(expected[i].Title, events[i].Title);
            Assert.Equal(expected[i].Date, events[i].Date);
            Assert.Equal(expected[i].Invitor, events[i].Invitor);
            Assert.Equal(expected[i].MaxParticipants, events[i].MaxParticipants);
            Assert.Empty(events[i].GetSortedParticipants());
        }
    }

    [Fact]
    public void Import_EmptyCsv_ReturnsEmpty()
    {
        string[] csv = [];
        Person[] people = [];
        
        Event[] events = Event.Import(csv, people);
        Assert.Empty(events);
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

    [Fact]
    public void RemovePerson_Valid_ShouldSucceed()
    {
        Person invitor = new Person("First", "Last");
        Event ev = new Event("Event 1", DateTime.Now, invitor, 2);
        Person p = new Person("First", "Last");

        bool success = true;

        success &= ev.AddPerson(p);
        success &= ev.RemovePerson(p);

        Assert.True(success);
    }

    [Fact]
    public void RemovePerson_NotAdded_ShouldFail()
    {
        Person invitor = new Person("First", "Last");
        Event ev = new Event("Event 1", DateTime.Now, invitor, 2);
        Person p = new Person("First", "Last");

        bool success = ev.RemovePerson(p);

        Assert.False(success);
    }

    [Fact]
    public void GetSortedParticipants_Valid_ShouldReturnSorted()
    {
        Person invitor = new Person("First", "Last");
        Event ev = new Event("Event 1", DateTime.Now, invitor, 10);
        Person p1 = new Person("First", "CCC");
        Person p2 = new Person("First2", "BBB");
        Person p3 = new Person("First3", "AAA");

        ev.AddPerson(p2);
        ev.AddPerson(p1);
        ev.AddPerson(p3);

        Person[] sorted = ev.GetSortedParticipants();

        Assert.Equal(p3, sorted[0]);
        Assert.Equal(p2, sorted[1]);
        Assert.Equal(p1, sorted[2]);
    }

    [Fact]
    public void ToString_Valid_ShouldReturnString()
    {
        Person invitor = new Person("First", "Last");
        Event ev = new Event("Event 1", new DateTime(1, 1, 1), invitor, 10);
        Person p1 = new Person("First", "CCC");
        Person p2 = new Person("First2", "BBB");
        Person p3 = new Person("First3", "AAA");

        ev.AddPerson(p2);
        ev.AddPerson(p1);
        ev.AddPerson(p3);

        string expected = "Event 1, 01.01.0001 12.00, Last First: AAA First3, BBB First2, CCC First";

        Assert.Equal(expected, ev.ToString());
    }
}
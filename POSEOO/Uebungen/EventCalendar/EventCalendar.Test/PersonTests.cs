namespace EventCalendar.Test;

public class PersonTests
{
    [Fact]
    public void ToString_ShouldReturnFormattedString()
    {
        Person p = new Person("Julian", "Mostbauer");
        string str = p.ToString();

        Assert.Equal("Mostbauer Julian", str);
    }

    [Fact]
    public void ToShortString_ShouldReturnFormattedString()
    {
        Person p = new Person("Matthias", "Darabos");
        string str = p.ToShortString();

        Assert.Equal("Darabos M.", str);
    }

    [Fact]
    public void Constructor_GivenValidParameters_ShouldSucceed()
    {
        Person p = new Person("First", "Last", "e@mail.com", "0677 phone");
        Assert.Equal("First", p.FirstName);
        Assert.Equal("Last", p.LastName);
        Assert.Equal("e@mail.com", p.EMail);
        Assert.Equal("0677 phone", p.PhoneNumber);
    }

    [Fact]
    public void Import_PartlyValid_ShouldReturnArray()
    {
        string[] csv =
        [
            "Erik; Reitbauer",
            "First; Last; e@mail.com; 1",
            "Foo; Bar; mail@mail.com; 2",
            "Invalid; Invalid; Invalid; Invalid; Invalid",
            "Not valid",
            ""
        ];
        Person[] expected =
        [
            new Person("Erik", "Reitbauer", Person.OPTIONAL_DEFAULT, Person.OPTIONAL_DEFAULT),
            new Person("First", "Last", "e@mail.com", "1"),
            new Person("Foo", "Bar", "mail@mail.com", "2")
        ];

        var result = Person.Import(csv);

        Assert.Equal(expected.Length, result.Length);

        for (var i = 0; i < result.Length; i++)
        {
            Assert.Equal(expected[i].FirstName, result[i].FirstName);
            Assert.Equal(expected[i].LastName, result[i].LastName);
            Assert.Equal(expected[i].EMail, result[i].EMail);
            Assert.Equal(expected[i].PhoneNumber, result[i].PhoneNumber);
        }
    }

    [Fact]
    public void Import_GivenEmpty_ShouldReturnEmpty()
    {
        string[] csv = [];

        var result = Person.Import(csv);

        Assert.Empty(result);
    }
}
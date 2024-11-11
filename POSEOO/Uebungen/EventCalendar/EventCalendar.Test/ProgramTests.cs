namespace EventCalendar.Test;

public class ProgramTests
{
    [Fact]
    public void CheckIfImportMode_WithValidArguments_ExpectedTrue()
    {
        string[] args = ["persons.txt", "events.txt"];
        bool result = Program.CheckIfImportMode(args);
        Assert.True(result);
    }
    
    [Fact]
    public void CheckIfImportMode_TooFewArguments_ExpectedFalse()
    {
        string[] args = ["person.csv"];
        bool result = Program.CheckIfImportMode(args);
        Assert.False(result);
    }

    [Fact]
    public void CheckIfImportMode_TooManyArguments_ExpectedFalse()
    {
        string[] args = ["person.csv", "event.csv", "extra.csv"];
        bool result = Program.CheckIfImportMode(args);
        Assert.False(result);
    }
    
    [Fact]
    public void CheckIfImportMode_InvalidEventsFile_ExpectedFalse()
    {
        string[] args = ["person.csv", "invalid.csv"];
        bool result = Program.CheckIfImportMode(args);
        Assert.False(result);
    }
    
    [Fact]
    public void CheckIfImportMode_InvalidPersonFile_ExpectedFalse()
    {
        string[] args = ["invalid.csv", "event.csv"];
        bool result = Program.CheckIfImportMode(args);
        Assert.False(result);
    }
}
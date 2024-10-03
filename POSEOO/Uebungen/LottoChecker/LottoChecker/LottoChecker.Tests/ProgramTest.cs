using JetBrains.Annotations;
using Xunit;

namespace LottoChecker.Tests;

[TestSubject(typeof(Program))]
public class ProgramTest {

    [Theory]
    [InlineData("id;1;2;3;4;5;6", new int[] {1,2,3,4,5,6})]
    [InlineData("id;1000;43;4567;54;1", new int[] {1000,43,4567,54,1})]
    public void TestTryParseCsvLine(string line, int[] expected) {
        // Act
        bool result = Program.TryParseCsvLine(line, out int[] tip);
        
        // Assert
        Assert.True(result);
        Assert.Equal(expected, tip);
    }
    
    [Theory]
    [InlineData(new int[] {1,2,3,4,5,6}, true)]
    [InlineData(new int[] {1,2,3,4,5,6,7}, false)]
    [InlineData(new int[] {1,2,3}, false)]
    [InlineData(new int[] {46,3,6,3,1,88}, false)]
    public void TestTipIsValid(int[] tip, bool expected) {
        // Act
        bool result = Program.TipIsValid(tip);
        
        // Assert
        Assert.Equal(expected, result);
    }
}
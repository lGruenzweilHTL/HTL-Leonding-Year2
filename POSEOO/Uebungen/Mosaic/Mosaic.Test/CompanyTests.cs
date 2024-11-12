using System.Reflection;

namespace Mosaic.Test;

public sealed class CompanyTests : TestBase
{
    [Fact]
    public void Construction()
    {
        const string Name = "A";
        const decimal M2Price = 12.34M;
        const decimal HourlyWage = 56.67M;
        const int Margin = 6;
        Worker[] workers = CreateSampleWorkers(1);

        var company = new Company(Name, M2Price, HourlyWage, Margin, workers);

        company.Name.Should().Be(Name);
        CheckFields(company, M2Price, HourlyWage, Margin, workers)
            .Should().BeTrue("ctor has to set all fields correctly");
    }

    [Theory]
    [InlineData(PatternStyle.Simple, 1, 75D)]
    [InlineData(PatternStyle.Simple, 2, 55D)]
    [InlineData(PatternStyle.Simple, 3, 45D)]
    [InlineData(PatternStyle.Simple, 4, 20D)]
    [InlineData(PatternStyle.Complex, 1, 37.5D)]
    [InlineData(PatternStyle.Complex, 2, 27.5D)]
    [InlineData(PatternStyle.Complex, 3, 22.5D)]
    [InlineData(PatternStyle.Complex, 4, 10D)]
    public void CalcPiecesPerHour(PatternStyle style, int workerSet, double expectedPpH)
    {
        var company = new Company("B", 0, 0, 
            0, CreateSampleWorkers(workerSet));

        CallCalcPiecesPerHour(company, style)
            .Should().Be(expectedPpH, "for this set of workers and pattern style");
    }

    [Theory]
    [MemberData(nameof(GetCostEstimateData))]
    public void GetCostEstimate(decimal m2Price, decimal hourlyWage, int profitMarginPercent, 
        Worker[] workers, TilePattern pattern, decimal expectedCost, string testCase)
    {
        var company = new Company("C", m2Price, hourlyWage, profitMarginPercent, workers);

        company.GetCostEstimate(pattern)
            .Should().Be(expectedCost, testCase);
    }
    
    [Fact]
    public void Import()
    {
        string[] csvData = new string[]
        {
            "CompanyName;M2Price;HourlyWage;Profit Margin;Workers",  // Header row
            "ABC Corp;50.5;25.75;20;John,Fast,Jane,Slow",               // Valid row
            "XYZ Ltd;60.0;30.5;15;Alice,Medium,Bob,Fast",               // Valid row
            "SomeCompany;75.0;35.0;10;Carol,Slow,Dan,Medium",           // Valid row
            "NoName;NaN;40.0;25;Tom,Fast,Joe,Medium",                   // Invalid: NaN for m² Price
            "CompanyB;100;NaN;30;Susan,Slow,Matt,Fast",                 // Invalid: NaN for Hourly Wage
            "CompanyC;45.5;30.0;35;",                                  // Invalid: Missing workers
            "MissingData;;50.0;40;Chris,Medium,Pat,Slow",               // Invalid: Missing m² Price
            "CompanyD;60.5;25.5;20;Eve,Fast,,Slow",                     // Invalid: Missing worker name
            "InvalidCompany;100;50;10;InvalidWorker,Fast,;Medium",      // Invalid: Missing worker name and invalid format
            "AnotherCo;90.0;20.0;15;John,Fast,Jane,NotValid",           // Invalid: Invalid WorkSpeed enum ("NotValid")
            "GoodCompany;70.0;40.0;20;Bill,Fast"                       // Invalid: Missing one worker
        };
        Company[] expectedCompanies = new Company[]
        {
            new Company("ABC Corp", 50.5m, 25.75m, 20, new Worker[]
            {
                new Worker("John", WorkSpeed.Fast),
                new Worker("Jane", WorkSpeed.Slow)
            }),
            new Company("XYZ Ltd", 60.0m, 30.5m, 15, new Worker[]
            {
                new Worker("Alice", WorkSpeed.Regular),
                new Worker("Bob", WorkSpeed.Fast)
            }),
            new Company("SomeCompany", 75.0m, 35.0m, 10, new Worker[]
            {
                new Worker("Carol", WorkSpeed.Slow),
                new Worker("Dan", WorkSpeed.Regular)
            }),
            new Company("CompanyC", 45.5m, 30.0m, 35, new Worker[] { }),
            new Company("CompanyD", 60.5m, 25.5m, 20, new Worker[] {
                new Worker("Eve", WorkSpeed.Fast)
            }),
            new Company("AnotherCo", 90.0m, 20.0m, 15, new Worker[]
            {
                new Worker("John", WorkSpeed.Fast)
            }),
            new Company("GoodCompany", 70.0m, 40.0m, 20, new Worker[]
            {
                new Worker("Bill", WorkSpeed.Fast)
            })
            // Invalid rows:
            // "NoName;NaN;40.0;25;Tom,Fast,Joe,Medium" (Invalid: NaN for m² Price)
            // "CompanyB;100;NaN;30;Susan,Slow,Matt,Fast" (Invalid: NaN for Hourly Wage)
            // "MissingData;;50.0;40;Chris,Medium,Pat,Slow" (Invalid: Missing m² Price)
            // "InvalidCompany;100;50;10;InvalidWorker,Fast,;Medium" (Invalid: Invalid format)
        };

        Company.Import(csvData).Should().BeEquivalentTo(expectedCompanies);
    }

    public static IEnumerable<object[]> GetCostEstimateData()
    {
        static TilePattern CreatePattern(PatternStyle style, int tileSet) => new(style, CreateSampleTiles(tileSet));

        return new[]
        {
            new object[]
            {
                12.34M, 56.78M, 5, CreateSampleWorkers(1),
                CreatePattern(PatternStyle.Simple, 1), 20M, "simple case 1"
            },
            new object[]
            {
                12.34M, 87.65M, 5, CreateSampleWorkers(1),
                CreatePattern(PatternStyle.Simple, 1), 25M, "simple case 2"
            },
            new object[]
            {
                12.34M, 87.65M, 5, CreateSampleWorkers(1),
                CreatePattern(PatternStyle.Complex, 1), 39M, "simple case 3"
            },
            new object[]
            {
                12.34M, 87.65M, 5, CreateSampleWorkers(1),
                CreatePattern(PatternStyle.Simple, 2), 2351M, "bigger pattern, workers 1"
            },
            new object[]
            {
                12.34M, 87.65M, 9, CreateSampleWorkers(1),
                CreatePattern(PatternStyle.Simple, 2), 2368M, "bigger pattern, workers 1, higher profit margin"
            },
            new object[]
            {
                12.34M, 87.65M, 5, CreateSampleWorkers(2),
                CreatePattern(PatternStyle.Simple, 2), 2182M, "bigger pattern, workers 2"
            },
            new object[]
            {
                60M, 87.65M, 5, CreateSampleWorkers(2),
                CreatePattern(PatternStyle.Simple, 2), 2280M, "bigger pattern, workers 2, higher m2 price"
            },
            new object[]
            {
                12.34M, 87.65M, 5, CreateSampleWorkers(3),
                CreatePattern(PatternStyle.Simple, 2), 2557M, "bigger pattern, workers 3"
            },
            new object[]
            {
                12.34M, 35.45M, 5, CreateSampleWorkers(3),
                CreatePattern(PatternStyle.Simple, 2), 1328M, "bigger pattern, workers 3, lower hourly price"
            },
            new object[]
            {
                12.34M, 87.65M, 5, CreateSampleWorkers(4),
                CreatePattern(PatternStyle.Simple, 2), 2815M, "bigger pattern, workers 4"
            },
            new object[]
            {
                12.34M, 87.65M, 5, CreateSampleWorkers(4),
                CreatePattern(PatternStyle.Complex, 2), 5138M, "bigger pattern, workers 4, complex pattern"
            }
        };
    }

    private static Worker[] CreateSampleWorkers(int set)
    {
        return set switch
        {
            1 => new Worker[]
            {
                new("Franz Felser", WorkSpeed.Regular),
                new("Marlene Marmor", WorkSpeed.Fast),
                new("Ludwig Langsam", WorkSpeed.Slow)
            },
            2 => new Worker[]
            {
                new("Franz Felser", WorkSpeed.Fast),
                new("Marlene Marmor", WorkSpeed.Regular)
            },
            3 => new Worker[]
            {
                new("Franz Felser", WorkSpeed.Slow),
                new("Marlene Marmor", WorkSpeed.Regular)
            },
            4 => new Worker[]
            {
                new("Franz Felser", WorkSpeed.Slow)
            },
            _ => Array.Empty<Worker>()
        };
    }

    #region test helper methods - ignore

    private static double CallCalcPiecesPerHour(Company instance, PatternStyle patternStyle)
    {
        var method = typeof(Company).GetMethod("CalcPiecesPerHour",
            BindingFlags.Instance | BindingFlags.NonPublic);
        if (method == null)
        {
            return -1D;
        }

        return (double) method.Invoke(instance, new object[] { patternStyle })!;
    }

    private static bool CheckFields(Company instance, decimal m2Price, decimal hourlyWage,
        int margin, Worker[] workers)
    {
        return CheckField(instance, m2Price, Prefix(nameof(m2Price)))
               && CheckField(instance, hourlyWage, Prefix(nameof(hourlyWage)))
               && CheckField(instance, margin, Prefix("profitMargin"))
               && CheckField(instance, workers, Prefix(nameof(workers)));
    }

    #endregion
}
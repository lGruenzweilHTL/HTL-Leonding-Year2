namespace HorseRace.Test;

#region test helper methods - ignore

public abstract class HorseTestBase
{
    protected static void SetPosition(Horse horse, int position)
    {
        var prop = typeof(Horse).GetProperty(nameof(Horse.Position));
        prop?.SetValue(horse, position);
    }
}

#endregion
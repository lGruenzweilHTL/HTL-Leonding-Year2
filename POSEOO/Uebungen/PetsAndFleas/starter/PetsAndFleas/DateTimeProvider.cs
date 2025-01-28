namespace PetsAndFleas;

/// <summary>
///     Provides the current time
/// </summary>
public sealed class DateTimeProvider
{
    private DateTime? _overrideDateTime;

    /// <summary>
    ///     Gets the current time.
    ///     Can be set to a specific value which will then be returned in the future.
    /// </summary>
    public DateTime Now
    {
        get => _overrideDateTime ?? DateTime.Now;
        set => _overrideDateTime = value;
    }
}

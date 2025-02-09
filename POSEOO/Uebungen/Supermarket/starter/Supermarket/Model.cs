namespace Supermarket;

public sealed record Review(DateTime Timestamp, Rating Rating, string Comment);

public enum Rating
{
    OneStar = 1,
    TwoStars = 2,
    ThreeStars = 3,
    FourStars = 4,
    FiveStars = 5
}

public enum AllergenType
{
    /// <summary>
    ///     Gluten
    /// </summary>
    A,

    /// <summary>
    ///     Crustaceans
    /// </summary>
    B,

    /// <summary>
    ///     Egg
    /// </summary>
    C,

    /// <summary>
    ///     Fish
    /// </summary>
    D,

    /// <summary>
    ///     Peanut
    /// </summary>
    E,

    /// <summary>
    ///     Soy
    /// </summary>
    F,

    /// <summary>
    ///     Milk / Lactose
    /// </summary>
    G,

    /// <summary>
    ///     Legumes
    /// </summary>
    H,

    /// <summary>
    ///     Celery
    /// </summary>
    L,

    /// <summary>
    ///     Mustard
    /// </summary>
    M,

    /// <summary>
    ///     Sesame
    /// </summary>
    N,

    /// <summary>
    ///     Sulfides
    /// </summary>
    O,

    /// <summary>
    ///     Lupines
    /// </summary>
    P,

    /// <summary>
    ///     Molluscs
    /// </summary>
    R
}

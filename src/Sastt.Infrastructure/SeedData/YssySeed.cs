namespace Sastt.Infrastructure.SeedData;

public static class YssySeed
{
    public static readonly (string TailNumber, string Model)[] Aircraft =
    {
        ("SY-001", "B738"),
        ("SY-002", "A330")
    };

    public static readonly string[] Pilots =
    {
        "Alice Brown",
        "Ben Smith"
    };

    public static readonly string[] Technicians =
    {
        "hemi.yssy@sastt.local",
        "maya.yssy@sastt.local"
    };
}


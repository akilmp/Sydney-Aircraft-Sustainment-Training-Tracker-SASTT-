namespace Sastt.Infrastructure.SeedData;

public static class YwlmSeed
{
    public static readonly (string TailNumber, string Model)[] Aircraft =
    {
        ("WL-001", "Hawk 127"),
        ("WL-002", "PC-21")
    };

    public static readonly string[] Pilots =
    {
        "Charlie Green",
        "Dana White"
    };

    public static readonly string[] Technicians =
    {
        "luke.ywlm@sastt.local",
        "zoe.ywlm@sastt.local"
    };
}


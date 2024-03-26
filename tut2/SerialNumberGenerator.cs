using System.Collections.Generic;

public static class SerialNumberGenerator
{
    private static readonly Dictionary<string, int> counters = new Dictionary<string, int>();

    public static string GenerateSerialNumber(string prefix)
    {
        if (!counters.ContainsKey(prefix))
        {
            counters[prefix] = 0;
        }
        counters[prefix]++;
        return $"KON-{prefix}-{counters[prefix]}";
    }
}
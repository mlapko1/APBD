using System.Collections.Generic;

namespace tut2
{
    public static class SerialNumberGenerator
    {
        private static readonly Dictionary<string, int> _counters = new Dictionary<string, int>();

        public static string GenerateSerialNumber(string containerType)
        {
            // Check if the container type already has a counter, if not initialize it
            if (!_counters.ContainsKey(containerType))
            {
                _counters[containerType] = 0;
            }

            // Increment the counter for this container type
            _counters[containerType]++;

            // Generate the serial number based on the container type and the new counter value
            return $"KON-{containerType}-{_counters[containerType]}";
        }
    }

}
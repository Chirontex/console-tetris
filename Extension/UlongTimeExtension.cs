namespace ConsoleTetris.Extension;

internal static class UlongTimeExtension
{
    /// <summary>
    /// Returns a time representation of ulong value as seconds.
    /// </summary>
    /// <param name="value">Seconds.</param>
    /// <returns>Normal time representation.</returns>
    public static string ConvertToTime(this ulong value)
    {
        var handleValue = (ulong value) => {
            return $"{(value < 10 ? "0" : "")}{value}";
        };

        if (value < 60)
        {
            return $"00:00:{handleValue(value)}";
        }

        ulong hours = value / 3600;
        ulong minutes = value / 60;

        if (hours > 0)
        {
            minutes = minutes % hours;
        }

        ulong seconds = value - 60 * (value / 60);

        return $"{handleValue(hours)}:{handleValue(minutes)}:{handleValue(seconds)}";
    }
}

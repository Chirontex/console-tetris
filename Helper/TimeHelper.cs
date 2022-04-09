namespace ConsoleTetris.Helper;

internal class TimeHelper
{
    /// <summary>
    /// Returns a time representation of integer value.
    /// </summary>
    /// <param name="value">Seconds.</param>
    /// <returns>Normal time representation.</returns>
    public static string ToTime(ulong value)
    {
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

    protected static string handleValue(ulong value)
    {
        return $"{(value < 10 ? "0" : "")}{value}";
    }
}

namespace ConsoleTetris.Helper;

internal class TimeHelper
{
    /// <summary>
    /// Returns a time representation of integer value.
    /// </summary>
    /// <param name="value">Seconds.</param>
    /// <returns>Normal time representation.</returns>
    public static string ToTime(int value)
    {
        if (value < 60)
        {
            return $"00:00:{(value < 10 ? "0" : "")}{value}";
        }

        int hours = value / 3600;
        int minutes = value / 60;
        int seconds = value - 60 * (value / 60);

        return $"{handleValue(hours)}:{handleValue(minutes)}:{handleValue(seconds)}";
    }

    protected static string handleValue(int value)
    {
        if (value < 10)
        {
            return $"0{value}";
        }

        return $"{value}";
    }
}

namespace ConsoleTetris.Dto;

public struct Cell
{
    private const string FILLED = "#";
    private const string EMPTY = " ";

    public bool IsFilled { get; set; }

    public Cell(bool isFilled = false)
    {
        this.IsFilled = isFilled;
    }

    /// <summary>
    /// Returns a string representation of cell.
    /// </summary>
    /// <returns>A character that equivalent to cell state.</returns>
    public string Get()
    {
        return this.IsFilled ? FILLED : EMPTY;
    }
}

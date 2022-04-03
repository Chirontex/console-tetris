namespace ConsoleTetris.Dto;

public struct Cell
{
    private const string FILLED = "#";
    private const string EMPTY = " ";

    private bool _isFixated = false;

    public int X { get; set; }
    public int Y { get; set; }
    public bool IsFilled { get; set; }
    public bool IsFixated
    {
        get
        {
            return this._isFixated;
        }
    }

    public Cell(int x, int y, bool isFilled = false)
    {
        this.X = x;
        this.Y = y;
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

    /// <summary>
    /// Fixate the cell in its line.
    /// </summary>
    public void Fixate()
    {
        this._isFixated = true;
    }
}

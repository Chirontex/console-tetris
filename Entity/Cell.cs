namespace ConsoleTetris.Entity;

internal class Cell
{
    private const string FILLED = "#";
    private const string EMPTY = " ";

    protected bool _isFixated = false;
    protected Row _row;
    protected byte _x;

    public Row Row
    {
        get
        {
            return this._row;
        }
    }

    public byte X
    {
        get
        {
            return this._x;
        }
    }

    public bool IsFilled { get; set; }
    public bool IsFixated
    {
        get
        {
            return this._isFixated;
        }
    }

    public Cell(Row row, byte x, bool isFilled = false)
    {
        this._row = row;
        this._x = x;
        this.IsFilled = isFilled;

        if (this._x == 0)
        {
            this._isFixated = true;
        }
    }

    /// <summary>
    /// Returns a string representation of cell.
    /// </summary>
    /// <returns>A character that equivalent to cell state.</returns>
    public string GetAsString()
    {
        return this.IsFilled ? FILLED : EMPTY;
    }

    /// <summary>
    /// Returns the cell's row object.
    /// </summary>
    /// <returns>The Row instance.</returns>
    public Row GetRow()
    {
        return this._row;
    }

    /// <summary>
    /// Fixate the cell in its line.
    /// </summary>
    public void Fixate()
    {
        this._isFixated = true;
    }
}

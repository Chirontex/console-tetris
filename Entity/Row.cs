using System.Collections.Generic;

namespace ConsoleTetris.Entity;

internal class Row
{
    public const byte CELLS_QUANTITY = 10;

    protected List<Cell> _cells;
    protected byte _y;

    public byte Y
    {
        get
        {
            return this._y;
        }
    }

    public Row(byte y)
    {
        this._y = y;
        this._cells = new();

        for (byte i = 0; i < CELLS_QUANTITY; i++)
        {
            this._cells.Add(new Cell(this, i));
        }
    }

    /// <summary>
    /// Returns the cell by its key.
    /// </summary>
    /// <param name="key">Cell coordinate in the row.</param>
    /// <returns>Cell object.</returns>
    public Cell GetCell(byte key)
    {
        return this._cells[key];
    }
}

using System.Collections.Generic;

namespace ConsoleTetris.Entity;

internal class Row
{
    public const int CELLS_QUANTITY = 10;

    protected List<Cell> _cells;
    protected int _x;

    public int X
    {
        get
        {
            return this._x;
        }
    }

    public Row(int x)
    {
        this._x = x;
        this._cells = new();

        for (int i = 0; i < CELLS_QUANTITY; i++)
        {
            this._cells.Add(new Cell(this, i));
        }
    }

    /// <summary>
    /// Returns the cell by its key.
    /// </summary>
    /// <param name="key">Cell coordinate in the row.</param>
    /// <returns>Cell object.</returns>
    public Cell GetCell(int key)
    {
        return this._cells[key];
    }
}

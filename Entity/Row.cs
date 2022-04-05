using System.Collections.Generic;

namespace ConsoleTetris.Entity;

internal class Row
{
    private const int CELLS_QUANTITY = 10;

    protected List<Cell> _cells;

    public Row()
    {
        this._cells = new();

        for (int i = 0; i < CELLS_QUANTITY; i++)
        {
            this._cells.Add(new Cell(this, i));
        }
    }
}

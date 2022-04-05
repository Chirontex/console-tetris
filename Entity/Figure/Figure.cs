using ConsoleTetris.Exception;
using System.Collections.Generic;

namespace ConsoleTetris.Entity.Figure;

internal abstract class Figure
{
    protected Field _field;
    protected List<Cell> _figureCells;
    protected bool _isInitialized = false;

    public Figure(Field field)
    {
        this._field = field;

        this.checkEntryPointCell();

        this._figureCells = new();
        this.initializeOnField();
        this._isInitialized = true;
    }

    /// <summary>
    /// Setting the figure on the field.
    /// </summary>
    abstract protected void initializeOnField();

    protected void checkEntryPointCell()
    {
        if (this.getEntryPointCell().IsFilled)
        {
            throw new GameOverException("An entry point cell is filled.");
        }
    }

    protected Cell getEntryPointCell()
    {
        return this._field.GetCell(
            Field.ROWS_QUANTITY - 1,
            Row.CELLS_QUANTITY/2
        );
    }

    protected void addCell(Cell cell)
    {
        if (!this._isInitialized && cell.IsFilled)
        {
            throw new GameOverException("A figure field is already filled.");
        }

        cell.IsFilled = true;
        this._figureCells.Add(cell);
    }
}

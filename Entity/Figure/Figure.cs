using ConsoleTetris.Exception;
using System.Collections.Generic;

namespace ConsoleTetris.Entity.Figure;

internal abstract class Figure
{
    protected Field _field;
    protected List<Cell> _figureCells;
    protected bool _isInitialized = false;
    protected bool _isDead = false;

    public bool IsDead
    {
        get
        {
            return this._isDead;
        }
    }

    public Figure(Field field)
    {
        this._field = field;

        this.checkEntryPointCell();

        this._figureCells = new();
        this.initializeOnField();
        this.fillCells();
        this._isInitialized = true;
    }

    /// <summary>
    /// Move figure down.
    /// </summary>
    public void Down()
    {
        this.cleanCells();

        List<Cell> newCells = new();

        foreach (Cell cell in this._figureCells)
        {
            Cell? newCell = this._field.GetCell(
                cell.GetRow().Y - 1,
                cell.X
            );

            if (newCell == null || newCell.IsFilled)
            {
                this.fillCells();
                this._isDead = true;
                return;
            }

            newCells.Add(newCell);
        }

        this._figureCells = newCells;
        this.fillCells();
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
        )!;
    }

    protected void fillCells()
    {
        foreach (Cell cell in this._figureCells)
        {
            cell.IsFilled = true;
        }
    }

    protected void cleanCells()
    {
        foreach (Cell cell in this._figureCells)
        {
            cell.IsFilled = false;
        }
    }
}

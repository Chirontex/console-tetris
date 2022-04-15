using ConsoleTetris.Exception;
using System.Collections.Generic;
using System;

namespace ConsoleTetris.Entity.Figure;

internal abstract class Figure
{
    public delegate void Movement();

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

    public Movement StartMovement { get; set; }
    public Movement EndMovement { get; set; }

    public Figure(Field field)
    {
        this._field = field;

        this.checkEntryPointCell();

        this._figureCells = new();
        this.initializeOnField();
        this.fillCells();
        this._isInitialized = true;

        this.StartMovement = () => {
            if (this._isDead) {
                throw new DeadFigureException("Attempt to move down a dead figure.");
            }
        };
        this.StartMovement += this.cleanCells;

        this.EndMovement = this.fillCells;
    }

    /// <summary>
    /// Move figure down.
    /// </summary>
    public void Down()
    {
        this.StartMovement();

        List<Cell> newCells = new();

        foreach (Cell cell in this._figureCells)
        {
            byte rowKey;

            try
            {
                rowKey = checked((byte)(cell.GetRow().Y - 1));
            }
            catch (OverflowException)
            {
                this._isDead = true;
                break;
            }

            Cell? newCell = this._field.GetCell(rowKey, cell.X);

            if (newCell == null || newCell.IsFilled)
            {
                this._isDead = true;
                break;
            }

            newCells.Add(newCell);
        }

        if (!this._isDead)
        {
            this._figureCells = newCells;
        }

        this.EndMovement();
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

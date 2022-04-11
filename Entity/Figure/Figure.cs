using ConsoleTetris.Exception;
using System.Collections.Generic;
using System;

namespace ConsoleTetris.Entity.Figure;

internal abstract class Figure
{
    public delegate void Movement();
    protected delegate void Death();

    protected Field _field;
    protected List<Cell> _figureCells;
    protected bool _isInitialized = false;
    protected bool _isDead = false;
    protected Death _death;

    public bool IsDead
    {
        get
        {
            return this._isDead;
        }
    }

    public Movement MovementDelegate { get; set; }

    public Figure(Field field)
    {
        this._field = field;

        this.checkEntryPointCell();

        this._figureCells = new();
        this.initializeOnField();
        this.fillCells();
        this._isInitialized = true;

        this._death = this.fillCells;
        this._death += this.die;

        this.MovementDelegate = this.fillCells;
    }

    /// <summary>
    /// Move figure down.
    /// </summary>
    public void Down()
    {
        if (this._isDead)
        {
            throw new DeadFigureException("Attempt to move down a dead figure.");
        }

        this.cleanCells();

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
                this._death();
                return;
            }

            Cell? newCell = this._field.GetCell(rowKey, cell.X);

            if (newCell == null || newCell.IsFilled)
            {
                this._death();
                return;
            }

            newCells.Add(newCell);
        }

        this._figureCells = newCells;
        this.MovementDelegate();
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

    protected void die()
    {
        this._isDead = true;
    }
}

using ConsoleTetris.Exception;
using System.Collections.Generic;
using System.Linq;
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

        List<Cell> cells = this.initializeOnField();

        foreach (Cell cell in cells)
        {
            if (cell.IsFilled)
            {
                throw new GameOverException("A figure field is already filled.");
            }
        }

        this._figureCells = cells;
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
        this.move(-1, 0);
    }

    /// <summary>
    /// Move figure left.
    /// </summary>
    public void Left()
    {
        this.move(0, -1, false);
    }

    /// <summary>
    /// Move figure right.
    /// </summary>
    public void Right()
    {
        this.move(0, 1, false);
    }

    /// <summary>
    /// Rotate the figure.
    /// </summary>
    public void Rotate()
    {
        try
        {
            this.StartMovement();
        }
        catch (DeadFigureException)
        {
            return;
        }

        List<Cell> newCells = new();
        Cell mediumCell = this.getMediumCell();

        newCells.Add(mediumCell);

        sbyte rowKey;
        sbyte cellKey;
        bool success = true;

        foreach (Cell cell in this._figureCells)
        {
            if (cell == mediumCell)
            {
                continue;
            }

            try
            {
                rowKey = checked((sbyte)(mediumCell.GetRow().Y + checked((sbyte)(mediumCell.X - cell.X))));
                cellKey = checked((sbyte)(mediumCell.X + checked((sbyte)(cell.GetRow().Y - mediumCell.GetRow().Y))));

                if (cellKey < 0)
                {
                    this.EndMovement();
                    this.Right();
                    this.Rotate();

                    return;
                }

                if (rowKey < 0)
                {
                    break;
                }
            }
            catch (OverflowException)
            {
                success = false;
                return;
            }

            Cell? newCell = this._field.GetCell(checked((byte)rowKey), checked((byte)cellKey));

            if (newCell == null || newCell.IsFilled)
            {
                success = false;
                break;
            }

            newCells.Add(newCell);
        }

        if (success)
        {
            this._figureCells = newCells;
        }

        this.EndMovement();
    }

    protected void move(sbyte rowMod, sbyte cellMod, bool causeToDeath = true)
    {
        try
        {
            this.StartMovement();
        }
        catch (DeadFigureException)
        {
            return;
        }

        List<Cell> newCells = new();
        bool success = true;
        byte rowKey;
        byte cellKey;

        foreach (Cell cell in this._figureCells)
        {
            try
            {
                rowKey = checked((byte)(cell.GetRow().Y + rowMod));
                cellKey = checked((byte)(cell.X + cellMod));
            }
            catch (OverflowException)
            {
                success = false;
                break;
            }

            Cell? newCell = this._field.GetCell(rowKey, cellKey);

            if (newCell == null || newCell.IsFilled)
            {
                success = false;
                break;
            }

            newCells.Add(newCell);
        }

        if (success)
        {
            this._figureCells = newCells;
        }
        else if (causeToDeath)
        {
            this._isDead = true;
        }

        this.EndMovement();
    }

    /// <summary>
    /// Setting the figure on the field.
    /// </summary>
    /// <return>List of cells.</return>
    abstract protected List<Cell> initializeOnField();

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

    protected Cell getMediumCell()
    {
        SortedDictionary<byte, List<Cell>> sortedByRows = new();
        byte rowKey;

        foreach (Cell cell in this._figureCells)
        {
            rowKey = cell.GetRow().Y;

            if (!sortedByRows.ContainsKey(rowKey))
            {
                sortedByRows.Add(rowKey, new List<Cell> {});
            }

            sortedByRows[rowKey].Add(cell);
        }

        byte mediumRowKey = 0;

        foreach (byte rk in sortedByRows.Keys)
        {
            mediumRowKey += rk;
        }

        mediumRowKey = (byte)(mediumRowKey / checked((byte)sortedByRows.Count));
        byte mediumCellKey = 0;

        foreach (Cell cell in sortedByRows[mediumRowKey])
        {
            mediumCellKey += cell.X;
        }

        mediumCellKey = (byte)(mediumCellKey / checked((byte)sortedByRows[mediumRowKey].Count));

        return this._field.GetCell(mediumRowKey, mediumCellKey)!;
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

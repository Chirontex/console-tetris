using System.Collections.Generic;
using System;

namespace ConsoleTetris.Entity;

internal class Field
{
    public const int ROWS_QUANTITY = 20;

    protected List<Row> _rows;

    public Field()
    {
        this._rows = new();

        for (int i = 0; i < ROWS_QUANTITY; i++)
        {
            this._rows.Add(new Row(i));
        }
    }

    /// <summary>
    /// Returns a requested cell by coordinates.
    /// </summary>
    /// <param name="rowKey">Row coordinate.</param>
    /// <param name="cellKey">Cell coordinate in row.</param>
    /// <returns>Cell object or null.</returns>
    public Cell? GetCell(int rowKey, int cellKey)
    {
        try
        {
            return this._rows[rowKey].GetCell(cellKey);
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }

    /// <summary>
    /// Visualize the field.
    /// </summary>
    /// <returns>A string representation of the field.</returns>
    public string Visualize()
    {
        string result = this.getBorder('_') + "\n";

        for (int k = this._rows.Count - 1; k >= 0; k--)
        {
            Row row = this._rows[k];

            result += "|";

            for (int i = 0; i < Row.CELLS_QUANTITY; i++)
            {
                result += row.GetCell(i).GetAsString();
            }

            result += "|\n";
        }

        result += this.getBorder('=') + "\n";

        return result;
    }

    protected string getBorder(char character)
    {
        string result = "";

        for (int i = 0; i < Row.CELLS_QUANTITY + 2; i++)
        {
            result += character;
        }

        return result;
    }
}

using System.Collections.Generic;

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
    /// <returns>Cell object.</returns>
    public Cell GetCell(int rowKey, int cellKey)
    {
        return this._rows[rowKey].GetCell(cellKey);
    }

    /// <summary>
    /// Visualize the field.
    /// </summary>
    /// <returns>A string representation of the field.</returns>
    public string Visualize()
    {
        string result = this.getBorder('_') + "\n";

        foreach (Row row in this._rows)
        {
            for (int i = 0; i < Row.CELLS_QUANTITY; i++)
            {
                result += row.GetCell(i).GetAsString();
            }

            result += "\n";
        }

        result += this.getBorder('=') + "\n";

        return result;
    }

    protected string getBorder(char character)
    {
        string result = "";

        for (int i = 0; i < Row.CELLS_QUANTITY; i++)
        {
            result += character;
        }

        return result;
    }
}

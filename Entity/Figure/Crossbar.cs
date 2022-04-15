using System.Collections.Generic;

namespace ConsoleTetris.Entity.Figure;

internal class Crossbar : Figure
{
    public Crossbar(Field field) : base(field)
    {
    }

    override protected List<Cell> initializeOnField()
    {
        Cell entryPointCell = this.getEntryPointCell();
        List<Cell> cells = new();

        cells.Add(entryPointCell);

        for (sbyte i = -2; i <= 1; i++)
        {
            if (i == 0)
            {
                continue;
            }

            Cell cell = this._field.GetCell(
                entryPointCell.GetRow().Y,
                checked((byte)(entryPointCell.X + i))
            )!;

            cells.Add(cell);
        }

        return cells;
    }
}

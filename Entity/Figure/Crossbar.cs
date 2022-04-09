using ConsoleTetris.Exception;

namespace ConsoleTetris.Entity.Figure;

internal class Crossbar : Figure
{
    public Crossbar(Field field) : base(field)
    {
    }

    override protected void initializeOnField()
    {
        Cell entryPointCell = this.getEntryPointCell();

        this._figureCells.Add(entryPointCell);

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

            if (cell.IsFilled)
            {
                throw new GameOverException("A figure field is already filled.");
            }

            this._figureCells.Add(cell);
        }
    }
}

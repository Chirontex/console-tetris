namespace ConsoleTetris.Entity.Figure;

internal class Crossbar : Figure
{
    public Crossbar(Field field) : base(field)
    {
    }

    override protected void initializeOnField()
    {
        Cell entryPointCell = this.getEntryPointCell();

        this.addCell(entryPointCell);

        for (int i = -2; i <= entryPointCell.X; i++)
        {
            if (i == 0)
            {
                continue;
            }

            this.addCell(
                this._field.GetCell(
                    entryPointCell.GetRow().Y,
                    entryPointCell.X + i
                )
            );
        }
    }
}

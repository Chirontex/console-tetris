namespace ConsoleTetris.Dto;

internal class CellDto
{
    protected sbyte _x;
    protected sbyte _y;

    public sbyte X
    {
        get
        {
            return this._x;
        }
    }

    public sbyte Y
    {
        get
        {
            return this._y;
        }
    }

    public CellDto(sbyte x, sbyte y)
    {
        this._x = x;
        this._y = y;
    }
}

namespace ConsoleTetris.Exception;

public class DeadFigureException: ConsoleTetrisException
{
    public DeadFigureException() : base()
    {
    }

    public DeadFigureException(string? message) : base(message)
    {
    }
}

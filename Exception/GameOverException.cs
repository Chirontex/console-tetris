namespace ConsoleTetris.Exception;

public class GameOverException : ConsoleTetrisException
{
    public GameOverException() : base()
    {
    }

    public GameOverException(string? message) : base(message)
    {
    }
}

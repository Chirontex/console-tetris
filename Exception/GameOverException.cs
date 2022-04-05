using System;

namespace ConsoleTetris.Exception;

public class GameOverException : SystemException
{
    public GameOverException() : base()
    {
    }

    public GameOverException(string? message) : base(message)
    {
    }
}

using System;

namespace ConsoleTetris.Exception;

public class ConsoleTetrisException : SystemException
{
    public ConsoleTetrisException() : base()
    {
    }

    public ConsoleTetrisException(string? message) : base(message)
    {
    }
}

using ConsoleTetris.Entity;
using ConsoleTetris.Service;

public class Program
{
    public static void Main(string[] args)
    {
        (new GameService(new Field())).Run();
    }
}

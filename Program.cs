using ConsoleTetris.Entity;
using ConsoleTetris.Helper;
using System.Threading;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        Field field = new();
        bool gameOver = false;
        int tickCounter = 0;
        int score = 0;

        while (!gameOver)
        {
            Thread.Sleep(1000);

            Console.Clear();
            Console.WriteLine($"Time: {TimeHelper.ToTime(tickCounter)}");
            Console.WriteLine($"Score: {score}");
            Console.Write(field.Visualize());

            tickCounter++;
        }
    }
}

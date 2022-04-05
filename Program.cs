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
        int time = 0;
        int score = 0;

        while (!gameOver)
        {
            Console.Clear();
            Console.WriteLine($"Time: {TimeHelper.ToTime(time)}");
            Console.WriteLine($"Score: {score}");
            Console.Write(field.Visualize());

            Thread.Sleep(1000);

            time++;
        }
    }
}

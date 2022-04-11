using ConsoleTetris.Entity;
using ConsoleTetris.Entity.Figure;
using ConsoleTetris.Helper;
using System.Threading;
using System;

namespace ConsoleTetris.Service;

internal class GameService
{
    protected Field _field;
    protected ulong _tickCounter = 0;
    protected ulong _score = 0;

    public GameService(Field field)
    {
        this._field = field;
    }

    /// <summary>
    /// Runs a game.
    /// </summary>
    public void Run()
    {
        bool gameOver = false;
        int sleep = 1000;
        Figure? figure = null;

        while (!gameOver)
        {
            if (figure == null || figure.IsDead)
            {
                figure = new Crossbar(this._field);
                figure.MovementDelegate += this.render;

                this.render();
            }

            if (sleep == 0)
            {
                this._tickCounter++;
                figure.Down();
                sleep = 1000;
            }

            Thread.Sleep(1);

            sleep--;
        }
    }

    protected void render()
    {
        Console.Clear();
        Console.WriteLine($"Time: {TimeHelper.ToTime(this._tickCounter)}");
        Console.WriteLine($"Score: {this._score}");
        Console.Write(this._field.Visualize());
    }
}

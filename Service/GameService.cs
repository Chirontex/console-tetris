using ConsoleTetris.Entity;
using ConsoleTetris.Entity.Figure;
using ConsoleTetris.Helper;
using System.Threading;
using System;

namespace ConsoleTetris.Service;

internal class GameService
{
    protected Field _field;
    protected int _tickCounter = 0;
    protected int _score = 0;

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
        int sleep = 1;
        Figure? figure = null;

        while (!gameOver)
        {
            if (sleep == 0)
            {
                if (figure == null)
                {
                    figure = new Crossbar(this._field);
                }

                this.render();
                this._tickCounter++;

                figure.Down();

                if (figure.IsDead)
                {
                    figure = null;
                }

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

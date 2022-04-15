using ConsoleTetris.Dto;
using ConsoleTetris.Entity;
using ConsoleTetris.Entity.Figure;
using ConsoleTetris.Exception;
using ConsoleTetris.Helper;
using System.Threading;
using System;

namespace ConsoleTetris.Service;

internal class GameService
{
    protected Field _field;
    protected ulong _tickCounter = 0;
    protected ulong _score = 0;
    protected Figure? _figure = null;
    protected bool _gameOver = false;
    protected bool _inRender = false;

    public GameService(Field field)
    {
        this._field = field;
    }

    /// <summary>
    /// Runs a game.
    /// </summary>
    public void Run()
    {
        this.initializeFigure();
        this.render();

        AutoResetEvent autoResetEvent = new(false);
        LoopTaskStateDto state = new();
        state.Handle = ThreadPool.RegisterWaitForSingleObject(
            autoResetEvent,
            new WaitOrTimerCallback(this.loop!),
            state,
            1000,
            false
        );

        while (!this._gameOver)
        {
            Thread.Sleep(1);
            
            var input = Console.ReadKey();

            if (input.KeyChar == 's' && !this._figure!.IsDead)
            {
                this._figure!.Down();
            }
        }
    }

    protected void loop(object state, bool timedOut = false)
    {
        this._tickCounter++;
        LoopTaskStateDto stateDto = (LoopTaskStateDto)state;

        try
        {
            if (this._figure != null && !this._figure.IsDead)
            {
                this._figure.Down();
            }

            if (this._figure == null || this._figure.IsDead)
            {
                this.initializeFigure();
                this.render();
            }
        }
        catch (GameOverException)
        {
            // TODO: Сделать рендер интерфейса завершения игры.
            stateDto.Handle!.Unregister(null);
            this._gameOver = true;
            Environment.Exit(0);
        }
    }

    protected void initializeFigure()
    {
        this._figure = new Crossbar(this._field);
        this._figure.EndMovement += this.render;
    }

    protected void render()
    {
        if (this._inRender)
        {
            return;
        }

        this._inRender = true;

        Console.Clear();
        Console.WriteLine($"Time: {TimeHelper.ToTime(this._tickCounter)}");
        Console.WriteLine($"Score: {this._score}");
        Console.Write(this._field.Visualize());

        this._inRender = false;
    }
}

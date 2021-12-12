namespace Superbird;

class Game
{
    public Game() { } // Make game callable

    ConsoleKeyInfo cki;

    /*
     * @pivotX and pivotY represent positions of bird
     * @height = height of main game container
     * @width = width of main game container
     *
     * @fallDelay = make a delay before bird fall, so it will look more natural and harder to predict
     * @upwardSpeed = jump range of bird     << maybe upward should be twice as much as fall >>
     * @fallSpeed = fall range of bird
     * @wing = while bird is going up wing should be "V", whereas fall supposed to be "^"
     * @eye = if bird died, it's eye should be "X", in contrast "O"
     */

    int score, pivotX, pivotY, height, width, fallDelay, wingDelay, upwardSpeed, fallSpeed;
    int[,] birdX = new int[5, 5];
    int[,] birdY = new int[5, 5];
    char[,] bird = new char[5, 5];
    char wing;

    /*
     * there are 2 pipes that will be summoned alternately
     * @pipeX and @pipeY represent placement of pipe
     * @pipeWidth should be 2 : 1 
     * @split should be 10 ???
     */

    int[,] pipeX = new int[30, 15];
    int[,] pipeY = new int[30, 15];
    char[,] pipe = new char[30, 15];
    int splitStart, splitLength, pipePivotX;

    int[,] pipeX2 = new int[30, 15];
    int[,] pipeY2 = new int[30, 15];
    char[,] pipe2 = new char[30, 15];
    int splitStart2, splitLength2, pipePivotX2;

    int pipeWidth, extraRender;  // extrarender prevent jitter from pipe gone away
    bool restart, isFlying, gameOver;

    Menu menu = new Menu();

    private void CountDown()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        for (int i = 3; i >= 1; i--)
        {
            Console.Write($"Game will start in {i}");
            Thread.Sleep(1000);
        }
        Console.ForegroundColor = ConsoleColor.Green;
    }

    private void Pause()
    {
        menu.Pause();
        while (true)
        {
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.Spacebar)
            {
                // render game again
                CountDown();
            }
            else if (cki.Key == ConsoleKey.R)
            {
                restart = true;
                break;
            }
            else if (cki.Key == ConsoleKey.Q)
            {
                menu.SpawnMenu();
                break;
            }
        }
    }
}

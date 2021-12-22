namespace Superbird;

class Game
{
    public Game() { } // Make game callable

    Random rand = new Random();
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

    int score, pivotX, pivotY, height, width, fallDelay, wingDelay, upwardSpeed, fallSpeed, highScore;
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
    bool restart, isFlying, gameOver, isPrinted;

    Menu menu = new Menu();

    private void Bird(char wing, char eye)
    {
        bird[0, 0] = ' '; bird[0, 1] = ','; bird[0, 2] = '-'; bird[0, 3] = '.'; bird[0, 4] = ' ';
        bird[1, 0] = '>'; bird[1, 1] = wing; bird[1, 2] = ' '; bird[1, 3] = eye; bird[1, 4] = '>';
        bird[2, 0] = ' '; bird[2, 1] = '*'; bird[2, 2] = '%'; bird[2, 3] = '*'; bird[2, 4] = ' ';

        birdX[0, 0] = pivotX - 2; birdY[0, 0] = pivotY - 1;
        birdX[0, 1] = pivotX - 1; birdY[0, 1] = pivotY - 1;
        birdX[0, 2] = pivotX; birdY[0, 2] = pivotY - 1;
        birdX[0, 3] = pivotX + 1; birdY[0, 3] = pivotY - 1;
        birdX[0, 4] = pivotX + 2; birdY[0, 4] = pivotY - 1;

        birdX[1, 0] = pivotX - 2; birdY[1, 0] = pivotY;
        birdX[1, 1] = pivotX - 1; birdY[1, 1] = pivotY;
        birdX[1, 2] = pivotX; birdY[1, 2] = pivotY;
        birdX[1, 3] = pivotX + 1; birdY[1, 3] = pivotY;
        birdX[1, 4] = pivotX + 2; birdY[1, 4] = pivotY;

        birdX[2, 0] = pivotX - 2; birdY[2, 0] = pivotY + 1;
        birdX[2, 1] = pivotX - 1; birdY[2, 1] = pivotY + 1;
        birdX[2, 2] = pivotX; birdY[2, 2] = pivotY + 1;
        birdX[2, 3] = pivotX + 1; birdY[2, 3] = pivotY + 1;
        birdX[2, 4] = pivotX + 2; birdY[2, 4] = pivotY + 1;
    }

    private void Pipe1()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < pipeWidth; j++)
            {
                if (j < extraRender)
                {
                    pipeX[i, j] = pipePivotX - (extraRender - j);
                }
                else if (j > extraRender)
                {
                    pipeX[i, j] = pipePivotX + (j - extraRender);
                }
                else if (j == extraRender)
                {
                    pipeX[i, j] = pipePivotX;
                }
                pipeY[i, j] = i;
                pipe[i, j] = '1'; // pipe1 char
            }
        }
        for (int k = splitStart; k < splitLength + splitStart; k++)
        {
            for (int l = 0; l < pipeWidth; l++)
            {
                pipe[k, l] = ' ';
            }
        }
        Thread.Sleep(30); // most important thing
    }

    private void Pipe2()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < pipeWidth; j++)
            {
                if (j < extraRender)
                {
                    pipeX2[i, j] = pipePivotX2 - (extraRender - j);
                }
                else if (j > extraRender)
                {
                    pipeX2[i, j] = pipePivotX2 + (j - extraRender);
                }
                else if (j == extraRender)
                {
                    pipeX2[i, j] = pipePivotX2;
                }
                pipeY2[i, j] = i;
                pipe2[i, j] = '2'; // pipe2 char
            }
        }
        for (int k = splitStart2; k < splitLength2 + splitStart2; k++)
        {
            for (int l = 0; l < pipeWidth; l++)
            {
                pipe2[k, l] = ' ';
            }
        }
        Thread.Sleep(30); // most important thing
    }

    private void CountDown()
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = ConsoleColor.White;
        Console.CursorVisible = false;
        for (int i = 3; i >= 1; i--)
        {
            Console.SetCursorPosition(width / 2 - 8, height / 2 + 5);
            Console.Write($"Game will start in {i}");
            Thread.Sleep(1000);
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.BackgroundColor = ConsoleColor.Black;
    }

    private void Pause()
    {
        menu.Pause();
        while (true)
        {
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.Spacebar)
            {
                Render();
                CountDown();
                break;
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

    private void Lose()
    {
        menu.Lose(score, highScore);
        while (true)
        {
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.R)
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

    private void Setup()
    {
        height = 30;
        width = 60;

        score = 0;
        fallDelay = 1;
        upwardSpeed = 3;
        fallSpeed = 1;
        wing = 'v';

        gameOver = false;
        restart = false;
        isFlying = false;

        pivotX = 25;
        pivotY = height / 2;

        splitStart = rand.Next(5, height - 10);
        splitStart2 = rand.Next(3, height - 11);
        splitLength = splitLength2 = 9;
        pipePivotX = 60;
        pipePivotX2 = pipePivotX + pipeWidth + 22;
        pipeWidth = 15;
        extraRender = pipeWidth / 2;
    }

    private void InitialRender()
    {
        Console.Clear();
        pivotX = 30;
        pivotY = 10;
        Bird('v', 'o');

        Console.WriteLine("================================");
        Console.WriteLine("supert duper bird");
        Console.WriteLine("================================");
        for (int i = 6; i < 14; i++)
        {
            for (int j = 0; j < width; j++)
            {
                isPrinted = false;
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        if (j == birdX[k, l] && i == birdY[k, l])
                        {
                            if (j == pivotX + 1 && i == pivotY)
                            {
                                Console.Write(bird[k, l]);
                            }
                            else if (j == pivotX - 1 && i == pivotY)
                            {
                                Console.Write(bird[k, l]);
                            }
                            else if (j == pivotX + 2 && i == pivotY)
                            {
                                Console.Write(bird[k, l]);
                            }
                            else
                            {
                                Console.Write(bird[k, l]);
                            }
                            isPrinted = true;
                        }
                    }
                }
                if (!isPrinted)
                {
                    Console.Write(" ");
                }
            }
        }
        Console.WriteLine();
    }

    private void Render()
    {
        if (!gameOver)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    isPrinted = false;
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 5; l++)
                        {
                            if (j == birdX[k, l] && i == birdY[k, l])
                            {
                                if (j == pivotX + 1 && i == pivotY)
                                {
                                    if (bird[k, l] == 'o')   // bird eye alive? color white : color red;
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    }
                                }
                                else if (j == pivotX - 1 && i == pivotY)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                }
                                else if (j == pivotX + 2 && i == pivotY)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                }
                                Console.Write(bird[k, l]);
                                isPrinted = true;
                            }
                        }
                    }
                    if (!isPrinted)
                    {
                        for (int x = 0; x < height; x++)
                        {
                            for (int y = 0; y < pipeWidth; y++)
                            {
                                if (j == pipeX[x, y] && i == pipeY[x, y])
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(pipe[x, y]);
                                    isPrinted = true;
                                }
                            }
                        }
                    }
                    if (!isPrinted)
                    {
                        for (int a = 0; a < height; a++)
                        {
                            for (int b = 0; b < pipeWidth; b++)
                            {
                                if (j == pipeX2[a, b] && i == pipeY2[a, b])
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(pipe2[a, b]);
                                    isPrinted = true;
                                }
                            }
                        }
                    }
                    if (!isPrinted)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(0, height);
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine($"Your score: {score}");
        }
    }

    private void GameInput()
    {
        while (Console.KeyAvailable)
        {
            if (!gameOver)
            {
                cki = Console.ReadKey(true);
            }
            if (cki.Key == ConsoleKey.Spacebar)
            {
                isFlying = true;
            }
            if (cki.Key == ConsoleKey.Escape)
            {
                Pause();
            }
        }
    }

    private void Logic()
    {
        pipePivotX--;
        pipePivotX2--;
        fallDelay++;
        wingDelay++;

        if (wingDelay == 1)
        {
            wing = '^';
        }
        if (fallDelay == 1)
        {
            pivotY += fallSpeed;
            fallDelay = 0;
        }
        if (isFlying)
        {
            pivotY -= upwardSpeed;
            wing = 'v';
            fallDelay = -1;
            wingDelay = -1;
            isFlying = false;
        }
        if (pipePivotX == pivotX - extraRender || pipePivotX2 == pivotX - extraRender)
        {
            score++;
            if (score > highScore)
            {
                highScore = score;
            }
        }
        if (pipePivotX == -extraRender)
        {
            pipePivotX = width + extraRender;
            splitStart = rand.Next(3, height - splitLength - 5);
        }
        if (pipePivotX2 == -extraRender)
        {
            pipePivotX2 = width + extraRender;
            splitStart2 = rand.Next(3, height - 13);
        }
        Pipe1();
        Pipe2();

        Bird(wing, 'o');
    }

    private void CheckDeath()
    {
        void DeathHelper()
        {
            Bird(wing, 'x');  // make eye be more realistic
            Render();
            gameOver = true;
        }

        if (pivotY + 1 <= 2 || pivotY + 1 >= height - 1)
        {
            DeathHelper();
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (birdY[i, j] <= pipeY[splitStart, 0] - 1 || birdY[i, j] >= pipeY[splitStart + splitLength, 0])
                {
                    if (birdX[i, j] >= pipePivotX - extraRender && birdX[i, j] <= pipePivotX + extraRender - 1)
                    {
                        DeathHelper();
                    }
                }
                if (birdY[i, j] <= pipeY2[splitStart2, 0] - 1 || birdY[i, j] >= pipeY2[splitStart2 + splitLength2, 0])
                {
                    if (birdX[i, j] >= pipePivotX2 - extraRender && birdX[i, j] <= pipePivotX2 + extraRender + 1)
                    {
                        DeathHelper();
                    }
                }
            }
        }
    }

    private void UpdateState()
    {
        Console.Clear();
        while (true)
        {
            GameInput();
            Logic();
            Render();
            CheckDeath();
            if (gameOver)
            {
                Lose();
            }
            if (gameOver || restart)
            {
                break;
            }
            Thread.Sleep(10);
        }
    }

    public void LoadGame()
    {
        CountDown();
        Setup();
        UpdateState();
    }

    public void InitialLoad()
    {
        Setup();
        LoadGame();
        while (true)
        {
            LoadGame();
        }
    }
}

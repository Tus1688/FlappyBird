namespace Superbird;

enum MenuOption
{
    Start,
    Quit,
}
class Menu
{
    public Menu() { } // make Menu callable from Program.cs
    public MenuOption option; // type of option is MenuOption (line 3)

    public MenuOption SpawnMenu()
    {
        Console.Clear();
        PrintFlappyBird();
        radio();

        ConsoleKeyInfo cki;
        cki = Console.ReadKey(true);

        while (true)
        {
            if (cki.Key == ConsoleKey.Spacebar)
            {
                option = MenuOption.Start;
                break;
            }
            else if (cki.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
                break;
            }
        }
        return option;
    }

    private void radio()
    {
        string start = String.Format("{0,-2} {1,-20}", " ", "Press [SpaceBar] to start game");
        string stop = String.Format("{0,-2} {1,-20}", " ", "Press [Escape] to quit the game");

        /*
         *Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (start.Length / 2)) + "}", start));
         *Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (stop.Length / 2)) + "}", stop));
         *Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (highscore.Length / 2)) + "}", highscore));
         */
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("              " + start);
        Console.WriteLine("              " + stop);
    }

    private void PrintFlappyBird()
    {
        string[] bird = new string[] {
            "                 ............          ",
            "              ...dXXXXXXXMMXk..        ",
            "          .'l000:.....,;0k .0x'.       ",
            "       ,,,dMO:'.......XW    ,:ko,.     ",
            "     ;ckkkkkkkoc'.....XW   .MO :Mo     ",
            "    .MK.......:xol....ox:; .d: :Mo     ",
            "    .MK.........xM:.....oolllllkM0l'   ",
            "     clxl.....:xol......xxxxxxxxxxxdd' ",
            "       ;ckkkkkoc'.....dkdd000000000o;. ",
            "         ,,,xMo.......;:00ooooooo0Mc   ",
            "            .'oKKK,.....,:XXXXXXXNMc   ",
            "               ...kXXXXXXK.........    ",
            "                  ........             ",
        };
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        for (int i = 0; i < bird.Length; i++)
        {
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("          " + bird[i]);
        }
        Console.WriteLine();
    }

    public void Pause()
    {
        Console.CursorTop = 10;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine("        ============================================        ");
        Console.WriteLine("                         GAME PAUSED                        ");
        Console.WriteLine("                                                            ");
        Console.WriteLine("              Resume game   - press [SpaceBar]              ");
        Console.WriteLine("              Restart game  - press [R]                     ");
        Console.WriteLine("        ============================================        ");
    }

    public void Lose(int score, int highScore)
    {
        Console.CursorTop = 10;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine();
        Console.WriteLine("        ============================================        ");
        Console.WriteLine("                          GAME OVER                         ");
        Console.WriteLine("                                                            ");
        Console.WriteLine("                       Your Score: {0}                      ", score);
        Console.WriteLine("                       High Score: {0}                      ", highScore);
        Console.WriteLine("                                                            ");
        Console.WriteLine("                 Restart game   - press [R]                 ");
        Console.WriteLine("                 Main menu      - press [Q]                 ");
        Console.WriteLine("        ============================================        ");
        Console.ForegroundColor = ConsoleColor.Green;
    }
}

namespace Superbird;

enum MenuOption
{
    Start,
    Quit,
    HighScore,
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

        switch (cki.Key)
        {
            case ConsoleKey.Spacebar:
                option = MenuOption.Start;
                break;
            case ConsoleKey.Escape:
                option = MenuOption.Quit;
                break;
            case ConsoleKey.H:
                option = MenuOption.HighScore;
                break;
            default:
                break;
        }
        return option;
    }

    private void radio()
    {
        string start = String.Format("{0,-2} {1,-20}", " ", "Press [SpaceBar] to start game");
        string stop = String.Format("{0,-2} {1,-20}", " ", "Press [Escape] to start game  ");
        string highscore = String.Format("{0,-2} {1,-20}", " ", "Press [H] to show highscore   ");

        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (start.Length / 2)) + "}", start));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (stop.Length / 2)) + "}", stop));
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (highscore.Length / 2)) + "}", highscore));
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
        Console.WriteLine();
        for (int i = 0; i < bird.Length; i++)
        {
            System.Threading.Thread.Sleep(200);
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (bird[i].Length / 2)) + "}", bird[i]));
        }
        Console.WriteLine();
    }
}

namespace Superbird;

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();
        Game game = new Game();

        while (menu.option != MenuOption.Quit)
        {
            menu.SpawnMenu();
            if (menu.option == MenuOption.Start)
            {
                game.InitialLoad();
            }
            else if (menu.option == MenuOption.Quit)
            {
                Environment.Exit(0);
            }
        }
    }
}

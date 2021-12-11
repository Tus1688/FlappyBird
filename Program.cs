namespace Superbird;

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();

        while(menu.option != MenuOption.Quit) {
            menu.SpawnMenu();
        }
    }
}

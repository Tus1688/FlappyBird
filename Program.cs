﻿namespace Superbird;

class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();
        Game game = new Game();

        game.InitialLoad();
        /*
         *while (menu.option != MenuOption.Quit)
         *{
         *    menu.SpawnMenu();
         *    if (menu.option == MenuOption.Start)
         *    {
         *        game.LoadGame();
         *    }
         *}
         */
    }
}

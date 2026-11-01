using System;

namespace LaLigaApp.UI
{
    public class Main_Menu
    {
        private readonly Team_management_UI _teamUI = new Team_management_UI();
        private readonly Player_management_UI _playerUI = new Player_management_UI();
        private readonly Match_execution_UI _matchUI = new Match_execution_UI();

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n=== LaLiga Management System ===");
                Console.WriteLine("1. Manage Teams");
                Console.WriteLine("2. Manage Players");
                Console.WriteLine("3. Match Execution & History");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _teamUI.ShowMenu();
                        break;
                    case "2":
                        _playerUI.ShowMenu();
                        break;
                    case "3":
                        _matchUI.ShowMenu();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
using System;
using LaLigaApp.Logic;

namespace LaLigaApp.UI
{
    public class Player_management_UI
    {
        private readonly player_service _playerService = new player_service();

        public void ShowMenu()
        {
            Console.WriteLine("\n--- Player Management ---");
            Console.WriteLine("1. Add New Player");
            Console.WriteLine("2. View All Players");
            Console.WriteLine("3. Delete Player");
            Console.WriteLine("4. Search By Name(exact match)");
            Console.WriteLine("5. Search By Name(partial match)");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter Player Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Position: ");
                string position = Console.ReadLine();
                Console.Write("Enter Team ID: ");
                if (int.TryParse(Console.ReadLine(), out int teamId))
                {
                    _playerService.AddPlayer(name, position, teamId);
                }
            }
            else if (choice == "2")
            {
                var players = _playerService.GetAllPlayers();
                Console.WriteLine("\n--- Registered Players ---");
                foreach (var player in players)
                {
                    Console.WriteLine($"ID: {player.Id} | Name: {player.Name} | Pos: {player.Position} | Team ID: {player.TeamId}");
                }
            }
            else if (choice == "3")
            {
                Console.Write("Enter Player ID to delete: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    _playerService.DeletePlayer(id);
                }
            }
            else if (choice == "4")
            {

                    Console.Write("Enter player name: ");
                    string name = Console.ReadLine();

                    var player = _playerService.SearchPlayer(name);

                    if (player != null)
                    {
                        Console.WriteLine(
                            $"ID: {player.Id}, Name: {player.Name}, Position: {player.Position}");
                    }
                    else
                    {
                        Console.WriteLine("Player not found.");
                    }
            }
            else if(choice == "5")
            {
                Console.Write("Enter player letters: ");
                string Letters = Console.ReadLine();

                var player = _playerService.SearchPlayerByLetters(Letters);

                foreach (var p in player)
                {
                    Console.WriteLine($"Player_id: {p.Id}, Player_Name: {p.Name}");

                }
            }
        }
    }
}
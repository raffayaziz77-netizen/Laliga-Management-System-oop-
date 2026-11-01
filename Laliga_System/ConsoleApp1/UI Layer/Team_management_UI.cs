using System;
using LaLigaApp.Logic;

namespace LaLigaApp.UI
{
    public class Team_management_UI
    {
        private readonly team_service _teamService = new team_service();

        public void ShowMenu()
        {
            Console.WriteLine("\n--- Team Management ---");
            Console.WriteLine("1. Add New Team");
            Console.WriteLine("2. View Standings");
            Console.WriteLine("3. Delete Team");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Write("Enter Team Name: ");
                string name = Console.ReadLine();
                _teamService.CreateTeam(name);
            }
            else if (choice == "2")
            {
                var teams = _teamService.GetStandings();
                foreach (var team in teams)
                {
                    Console.WriteLine($"{team.Id}. {team.Name} - {team.Points} pts");
                }
            }
            else if (choice == "3")
            {
                Console.Write("Enter Team ID to delete: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    _teamService.DeleteTeam(id);
                }
            }
        }
    }
}
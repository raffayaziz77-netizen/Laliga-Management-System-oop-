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
            Console.WriteLine("4. Search By Name (exact match)");
            Console.WriteLine("5. Search By Name (partial)");
            Console.WriteLine("6. Search by points (Numeric range)");
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
            else if (choice == "4")
            {
                Console.Write("Enter Team name: ");
                string name = Console.ReadLine();

                var team = _teamService.SearchTeam(name);

                if (team != null)
                {
                    Console.WriteLine(
                        $"ID: {team.Id}, Name: {team.Name}, Points: {team.Points}");
                }
                else
                {
                    Console.WriteLine("Team not found.");
                }
            }
            else if (choice == "5")
            {
                Console.Write("Enter player letters: ");
                string Letters = Console.ReadLine();

                var Teams = _teamService.SearchTeamByLetters(Letters);

                foreach (var t in Teams)
                {
                    Console.WriteLine($"Team_id: {t.Id}, team_Name: {t.Name}");

                }
            }
            else if (choice == "6")
            {
                Console.Write("Enter n1: ");
                int n1 = int.Parse(Console.ReadLine());

                Console.Write("Enter n2: ");
                int n2 = int.Parse(Console.ReadLine());

                var Teams = _teamService.SearchByPoints(n1,n2);

                foreach (var t in Teams)
                {
                    Console.WriteLine($"Team_id: {t.Id}, team_Name: {t.Name}");

                }

            }
        }
    }
}
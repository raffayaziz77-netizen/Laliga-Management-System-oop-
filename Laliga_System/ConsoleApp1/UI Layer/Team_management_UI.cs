using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaLigaSystem.Logic;

namespace LaLigaSystem.UI
{
    public class TeamManagementUI
    {
        private readonly TeamService _teamService;
        private readonly PlayerService _playerService;

        public TeamManagementUI(TeamService teamService, PlayerService playerService)
        {
            _teamService = teamService;
            _playerService = playerService;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Team & Player Details ===");
                Console.WriteLine("1. View League Standings");
                Console.WriteLine("2. View Team Roster");
                Console.WriteLine("3. Search Player");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ViewStandings(); break;
                    case "2": ViewTeamRoster(); break;
                    case "3": SearchPlayer(); break;
                    case "4": return;
                }
            }
        }

        private void ViewStandings()
        {
            Console.Clear();
            Console.WriteLine("POS | TEAM | P | W | D | L | GF | GA | GD | PTS");
            var standings = _teamService.GetStandings();
            int pos = 1;
            foreach (var t in standings)
            {
                Console.WriteLine($"{pos++} | {t.Name} | {t.MatchesPlayed} | {t.GoalsFor} | {t.GoalsAgainst} | {t.GoalDifference} | {t.Points}");
            }
            Console.ReadLine();
        }

        private void ViewTeamRoster()
        {
            Console.Write("Enter Team ID to view roster (1-10): ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var players = _playerService.GetPlayersByTeam(id);
                var team = _teamService.GetTeamById(id);
                if (team != null)
                {
                    Console.WriteLine($"\n--- {team.Name} Roster ---");
                    foreach (var p in players) Console.WriteLine($"{p.Position} | {p.Name} | Goals: {p.Goals}");
                }
            }
            Console.ReadLine();
        }

        private void SearchPlayer()
        {
            Console.Write("Enter Player Name: ");
            var player = _playerService.SearchPlayerByName(Console.ReadLine());
            if (player != null) Console.WriteLine($"Found: {player.Name} (Team ID: {player.TeamId}, Pos: {player.Position})");
            else Console.WriteLine("Player not found.");
            Console.ReadLine();
        }
    }
}
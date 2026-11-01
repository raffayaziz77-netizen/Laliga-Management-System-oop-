using System;
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
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ViewStandings()
        {
            Console.Clear();
            Console.WriteLine("POS | TEAM                 | P  | W | D | L | GF | GA | GD | PTS");
            Console.WriteLine("----------------------------------------------------------------");
            var standings = _teamService.GetStandings();
            int position = 1;
            foreach (var team in standings)
            {
                Console.WriteLine(
                    "{0,3} | {1,-20} | {2,2} | {3,1} | {4,1} | {5,1} | {6,2} | {7,2} | {8,2} | {9,3}",
                    position++, team.Name, team.MatchesPlayed, team.Wins, team.Draws,
                    team.Losses, team.GoalsFor, team.GoalsAgainst, team.GoalDifference, team.Points);
            }
            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void ViewTeamRoster()
        {
            Console.Clear();
            Console.Write("Enter Team ID to view roster (1-10): ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please enter a valid numeric team ID.");
                Console.ReadLine();
                return;
            }

            var team = _teamService.GetTeamById(id);
            if (team == null)
            {
                Console.WriteLine("Team not found. Please check the team ID.");
                Console.ReadLine();
                return;
            }

            var players = _playerService.GetPlayersByTeam(id);
            Console.WriteLine("\n--- " + team.Name + " Roster ---");
            foreach (var player in players)
                Console.WriteLine("{0} | {1} | Goals: {2}", player.Position, player.Name, player.Goals);
            if (players.Count == 0) Console.WriteLine("No players are registered for this team.");
            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void SearchPlayer()
        {
            Console.Clear();
            Console.Write("Enter Player Name: ");
            var player = _playerService.SearchPlayerByName(Console.ReadLine());
            if (player != null)
                Console.WriteLine("Found: {0} (Team ID: {1}, Pos: {2})", player.Name, player.TeamId, player.Position);
            else
                Console.WriteLine("Player not found. Enter a non-empty name and try again.");
            Console.ReadLine();
        }
    }
}
using System;
using LaLigaApp.Logic;

namespace LaLigaApp.UI
{
    public class Match_execution_UI
    {
        private readonly match_service _matchService = new match_service();
        private readonly team_service _teamService = new team_service();

        public void ShowMenu()
        {
            Console.WriteLine("\n--- Match Execution & History ---");
            Console.WriteLine("1. Record a Match Result");
            Console.WriteLine("2. View Match History");
            Console.WriteLine("3. Delete Match Record");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter Home Team ID: ");
                if (!int.TryParse(Console.ReadLine(), out int homeId)) return;
                Console.Write("Enter Away Team ID: ");
                if (!int.TryParse(Console.ReadLine(), out int awayId)) return;
                Console.Write("Enter Home Goals: ");
                if (!int.TryParse(Console.ReadLine(), out int homeGoals)) return;
                Console.Write("Enter Away Goals: ");
                if (!int.TryParse(Console.ReadLine(), out int awayGoals)) return;

                _matchService.RecordMatch(homeId, awayId, homeGoals, awayGoals);
            }
            else if (choice == "2")
            {
                var matches = _matchService.GetAllMatches();
                var teams = _teamService.GetAllTeams();
                Console.WriteLine("\n--- Match History ---");
                foreach (var match in matches)
                {
                    string homeTeamName = teams.Find(t => t.Id == match.HomeTeamId)?.Name ?? "Unknown Team";
                    string awayTeamName = teams.Find(t => t.Id == match.AwayTeamId)?.Name ?? "Unknown Team";
                    Console.WriteLine($"Match ID: {match.Id} | {homeTeamName} [{match.HomeGoals}] vs [{match.AwayGoals}] {awayTeamName}");
                }
            }
            else if (choice == "3")
            {
                Console.Write("Enter Match ID to delete: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    _matchService.DeleteMatch(id);
                }
            }
        }
    }
}
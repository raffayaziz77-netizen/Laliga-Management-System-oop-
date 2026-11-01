using System;
using LaLigaSystem.Logic;
using LaLigaSystem.Common;

namespace LaLigaSystem.UI
{
    public class MatchExecutionUI
    {
        private readonly MatchService _matchService;

        public MatchExecutionUI(MatchService matchService) { _matchService = matchService; }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Match Details Menu ===");
                Console.WriteLine("1. Play Next Match");
                Console.WriteLine("2. Back to Main Menu");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                if (choice == "1") PlayMatch();
                else if (choice == "2") return;
                else
                {
                    Console.WriteLine("Invalid option. Press Enter to continue.");
                    Console.ReadLine();
                }
            }
        }

        private void PlayMatch()
        {
            try
            {
                _matchService.PlayNextMatch();
                Console.WriteLine("Match simulated successfully. Standings updated.");
            }
            catch (LeagueException ex)
            {
                Console.WriteLine("Unable to play match: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaLigaSystem.Logic;

namespace LaLigaSystem.UI
{
    public class MatchExecutionUI
    {
        private readonly MatchService _matchService;

        public MatchExecutionUI(MatchService matchService)
        {
            _matchService = matchService;
        }

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
            }
        }

        private void PlayMatch()
        {
            try
            {
                _matchService.PlayNextMatch();
                Console.WriteLine("Match simulated successfully. Standings updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
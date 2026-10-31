using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LaLigaSystem.UI
{
    public class MainMenuUI
    {
        private readonly TeamManagementUI _teamUI;
        private readonly MatchExecutionUI _matchUI;

        public MainMenuUI(TeamManagementUI teamUI, MatchExecutionUI matchUI)
        {
            _teamUI = teamUI;
            _matchUI = matchUI;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== LaLiga Management System ===");
                Console.WriteLine("1. Team & Player Management");
                Console.WriteLine("2. Match Execution");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                if (choice == "1") _teamUI.ShowMenu();
                else if (choice == "2") _matchUI.ShowMenu();
                else if (choice == "3") return;
            }
        }
    }
}
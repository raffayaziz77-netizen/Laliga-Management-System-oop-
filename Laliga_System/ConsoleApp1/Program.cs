using System.Collections.Generic;
using System.IO;
using LaLigaSystem.Models;
using LaLigaSystem.DAL;
using LaLigaSystem.Logic;
using LaLigaSystem.UI;
using LaLigaSystem.Common;

namespace LaLigaSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Initialize Data Access Layer (Repositories)
            var teamRepo = new TeamRepository();
            var playerRepo = new PlayerRepository();
            var matchRepo = new MatchRepository();

            // 2. Seed database if files do not exist
            if (!File.Exists(Constants.TeamsFile))
            {
                SeedDatabase(teamRepo, playerRepo, matchRepo);
            }

            // 3. Initialize Business Logic Layer (Services)
            var teamService = new TeamService(teamRepo);
            var playerService = new PlayerService(playerRepo);
            var matchService = new MatchService(matchRepo, teamService);

            // 4. Initialize Sub-Menus (UI Layer)
            var teamUI = new TeamManagementUI(teamService, playerService);
            var matchUI = new MatchExecutionUI(matchService);

            // 5. Pass Sub-Menus to the Main Router and start
            var mainUI = new MainMenuUI(teamUI, matchUI);
            mainUI.ShowMenu();
        }

        static void SeedDatabase(TeamRepository tr, PlayerRepository pr, MatchRepository mr)
        {
            var teams = new List<Team>
            {
                new Team(1, "FC Barcelona"), new Team(2, "Real Madrid"), new Team(3, "Atletico Madrid"),
                new Team(4, "Sevilla"), new Team(5, "Real Sociedad"), new Team(6, "Villarreal"),
                new Team(7, "Real Betis"), new Team(8, "Athletic Club"), new Team(9, "Valencia"),
                new Team(10, "Osasuna")
            };
            tr.SaveAll(teams);

            var players = new List<Player>();
            int playerId = 1;

            for (int t = 1; t <= 10; t++)
            {
                players.Add(new Player(playerId++, $"Goalkeeper {t}", t, "Goalkeeper"));
                for (int d = 1; d <= 5; d++) players.Add(new Player(playerId++, $"Defender {d} (T{t})", t, "Defender"));
                for (int m = 1; m <= 6; m++) players.Add(new Player(playerId++, $"Midfielder {m} (T{t})", t, "Midfielder"));
                for (int f = 1; f <= 3; f++) players.Add(new Player(playerId++, $"Forward {f} (T{t})", t, "Forward"));
            }

            players[0].Name = "Marc-Andre ter Stegen";
            players[14].Name = "Robert Lewandowski";
            players[15].Name = "Thibaut Courtois";
            players[29].Name = "Vinicius Junior";
            pr.SaveAll(players);

            var matches = new List<Match>();
            int matchId = 1;
            for (int i = 0; i < teams.Count - 1; i++)
            {
                for (int j = i + 1; j < teams.Count; j++)
                {
                    matches.Add(new Match(matchId++, teams[i].Id, teams[j].Id));
                }
            }
            mr.SaveAll(matches);
        }
    }
}
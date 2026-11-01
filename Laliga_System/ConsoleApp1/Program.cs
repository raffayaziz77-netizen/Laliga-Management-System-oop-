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
            var teamRepo = new TeamRepository();
            var playerRepo = new PlayerRepository();
            var matchRepo = new MatchRepository();

            // Initialize each data file independently so one missing file does not
            // cause existing season data in the other files to be overwritten.
            if (!HasData(Constants.TeamsFile))
                SeedTeams(teamRepo);

            if (!HasData(Constants.PlayersFile))
                SeedPlayers(playerRepo);

            if (!HasData(Constants.MatchesFile))
                SeedMatches(matchRepo, teamRepo.LoadAll());

            var teamService = new TeamService(teamRepo);
            var playerService = new PlayerService(playerRepo);
            var matchService = new MatchService(matchRepo, teamService);

            var teamUI = new TeamManagementUI(teamService, playerService);
            var matchUI = new MatchExecutionUI(matchService);
            var mainUI = new MainMenuUI(teamUI, matchUI);
            mainUI.ShowMenu();
        }

        private static bool HasData(string path)
        {
            return File.Exists(path) && new FileInfo(path).Length > 0;
        }

        private static void SeedTeams(TeamRepository repository)
        {
            var teams = new List<Team>
            {
                new Team(1, "FC Barcelona"), new Team(2, "Real Madrid"),
                new Team(3, "Atletico Madrid"), new Team(4, "Sevilla"),
                new Team(5, "Real Sociedad"), new Team(6, "Villarreal"),
                new Team(7, "Real Betis"), new Team(8, "Athletic Club"),
                new Team(9, "Valencia"), new Team(10, "Osasuna")
            };

            repository.SaveAll(teams);
        }

        private static void SeedPlayers(PlayerRepository repository)
        {
            var players = new List<Player>();
            int playerId = 1;

            for (int teamId = 1; teamId <= 10; teamId++)
            {
                players.Add(new Player(playerId++, "Goalkeeper " + teamId, teamId, PlayerPosition.Goalkeeper));
                for (int i = 1; i <= 5; i++)
                    players.Add(new Player(playerId++, "Defender " + i + " (T" + teamId + ")", teamId, PlayerPosition.Defender));
                for (int i = 1; i <= 6; i++)
                    players.Add(new Player(playerId++, "Midfielder " + i + " (T" + teamId + ")", teamId, PlayerPosition.Midfielder));
                for (int i = 1; i <= 3; i++)
                    players.Add(new Player(playerId++, "Forward " + i + " (T" + teamId + ")", teamId, PlayerPosition.Forward));
            }

            players[0].Name = "Marc-Andre ter Stegen";
            players[14].Name = "Robert Lewandowski";
            players[15].Name = "Thibaut Courtois";
            players[29].Name = "Vinicius Junior";

            repository.SaveAll(players);
        }

        private static void SeedMatches(MatchRepository repository, List<Team> teams)
        {
            var matches = new List<Match>();
            int matchId = 1;

            for (int i = 0; i < teams.Count - 1; i++)
                for (int j = i + 1; j < teams.Count; j++)
                    matches.Add(new Match(matchId++, teams[i].Id, teams[j].Id));

            repository.SaveAll(matches);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using LaLigaSystem.Models;
using LaLigaSystem.Common;

namespace LaLigaSystem.DAL
{
    public class TeamRepository
    {
        public void SaveAll(List<Team> teams)
        {
            if (teams == null) throw new LeagueException("Team data cannot be null.");
            using (var writer = new StreamWriter(Constants.TeamsFile, false))
                foreach (var team in teams) writer.WriteLine(team.ToString());
        }

        public List<Team> LoadAll()
        {
            var teams = new List<Team>();
            if (!File.Exists(Constants.TeamsFile)) return teams;

            int lineNumber = 0;
            foreach (var line in File.ReadAllLines(Constants.TeamsFile))
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(line)) continue;
                var data = line.Split(',');
                if (data.Length != 6 && data.Length != 9)
                    throw new LeagueException("Invalid team data at line " + lineNumber + " in " + Constants.TeamsFile + ".");

                int id, points, played, goalsFor, goalsAgainst;
                if (!int.TryParse(data[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out id) ||
                    !int.TryParse(data[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out points) ||
                    !int.TryParse(data[3], NumberStyles.Integer, CultureInfo.InvariantCulture, out played))
                    throw new LeagueException("Invalid numeric team data at line " + lineNumber + ".");

                int wins = 0, draws = 0, losses = 0;
                int goalsForIndex = 4, goalsAgainstIndex = 5;
                if (data.Length == 9)
                {
                    if (!int.TryParse(data[4], out wins) || !int.TryParse(data[5], out draws) ||
                        !int.TryParse(data[6], out losses))
                        throw new LeagueException("Invalid win/draw/loss data at line " + lineNumber + ".");
                    goalsForIndex = 7;
                    goalsAgainstIndex = 8;
                }

                if (!int.TryParse(data[goalsForIndex], out goalsFor) ||
                    !int.TryParse(data[goalsAgainstIndex], out goalsAgainst) ||
                    string.IsNullOrWhiteSpace(data[1]))
                    throw new LeagueException("Invalid team record at line " + lineNumber + ".");

                teams.Add(new Team(id, data[1])
                {
                    Points = points, MatchesPlayed = played,
                    Wins = wins, Draws = draws, Losses = losses,
                    GoalsFor = goalsFor, GoalsAgainst = goalsAgainst
                });
            }
            return teams;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using LaLigaSystem.Models;
using LaLigaSystem.Common;

namespace LaLigaSystem.DAL
{
    public class MatchRepository
    {
        public void SaveAll(List<Match> matches)
        {
            if (matches == null) throw new LeagueException("Match data cannot be null.");
            using (var writer = new StreamWriter(Constants.MatchesFile, false))
                foreach (var match in matches) writer.WriteLine(match.ToString());
        }

        public List<Match> LoadAll()
        {
            var matches = new List<Match>();
            if (!File.Exists(Constants.MatchesFile)) return matches;

            int lineNumber = 0;
            foreach (var line in File.ReadAllLines(Constants.MatchesFile))
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(line)) continue;
                var data = line.Split(',');
                int id, homeId, awayId, homeScore, awayScore;
                bool isPlayed;

                if (data.Length != 6 ||
                    !int.TryParse(data[0], out id) ||
                    !int.TryParse(data[1], out homeId) ||
                    !int.TryParse(data[2], out awayId) ||
                    !int.TryParse(data[3], out homeScore) ||
                    !int.TryParse(data[4], out awayScore) ||
                    !bool.TryParse(data[5], out isPlayed))
                    throw new LeagueException("Invalid match data at line " + lineNumber + " in " + Constants.MatchesFile + ".");

                if (homeId == awayId || homeScore < 0 || awayScore < 0)
                    throw new LeagueException("Invalid match values at line " + lineNumber + ".");

                matches.Add(new Match(id, homeId, awayId)
                {
                    HomeScore = homeScore, AwayScore = awayScore, IsPlayed = isPlayed
                });
            }
            return matches;
        }
    }
}
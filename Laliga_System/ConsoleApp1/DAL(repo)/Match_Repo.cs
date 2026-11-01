using System;
using System.Collections.Generic;
using System.IO;
using LaLigaApp.Models;

namespace LaLigaApp.DAL
{
    public class Match_Repo
    {
        private readonly string filePath = "matches.txt";

        public void DeleteMatch(int id)
        {
            var matches = GetAllMatches();
            var matchToRemove = matches.Find(m => m.Id == id);
            if (matchToRemove != null)
            {
                matches.Remove(matchToRemove);
                SaveAllMatches(matches);
            }
        }
        public List<Match> GetAllMatches()
        {
            List<Match> matches = new List<Match>();
            if (!File.Exists(filePath)) return matches;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 5)
                    {
                        matches.Add(new Match
                        {
                            Id = int.Parse(parts[0]),
                            HomeTeamId = int.Parse(parts[1]),
                            AwayTeamId = int.Parse(parts[2]),
                            HomeGoals = int.Parse(parts[3]),
                            AwayGoals = int.Parse(parts[4])
                        });
                    }
                }
            }
            return matches;
        }

        public void SaveAllMatches(List<Match> matches)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var match in matches)
                {
                    writer.WriteLine($"{match.Id},{match.HomeTeamId},{match.AwayTeamId},{match.HomeGoals},{match.AwayGoals}");
                }
            }
        }

        public void AddMatch(Match match)
        {
            var matches = GetAllMatches();
            match.Id = matches.Count > 0 ? matches[matches.Count - 1].Id + 1 : 1;
            matches.Add(match);
            SaveAllMatches(matches);
        }
    }
}
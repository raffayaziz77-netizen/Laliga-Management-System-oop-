using System;
using System.Collections.Generic;
using System.IO;
using LaLigaApp.Models;

namespace LaLigaApp.DAL
{
    public class Team_Repo
    {
        private readonly string filePath = "teams.txt";

        public void DeleteTeam(int id)
        {
            var teams = GetAllTeams();
            var teamToRemove = teams.Find(t => t.Id == id);
            if (teamToRemove != null)
            {
                teams.Remove(teamToRemove);
                SaveAllTeams(teams); // Overwrites the text file with the updated list
            }
        }

        public List<Team> GetAllTeams()
        {
            List<Team> teams = new List<Team>();
            if (!File.Exists(filePath)) return teams;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        teams.Add(new Team
                        {
                            Id = int.Parse(parts[0]),
                            Name = parts[1],
                            Points = int.Parse(parts[2])
                        });
                    }
                }
            }
            return teams;
        }

        public void SaveAllTeams(List<Team> teams)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var team in teams)
                {
                    writer.WriteLine($"{team.Id},{team.Name},{team.Points}");
                }
            }
        }

        public void AddTeam(Team team)
        {
            var teams = GetAllTeams();
            team.Id = teams.Count > 0 ? teams[teams.Count - 1].Id + 1 : 1;
            teams.Add(team);
            SaveAllTeams(teams);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaLigaSystem.Models;
using LaLigaSystem.Common;
using System.IO;

namespace LaLigaSystem.DAL
{
    public class TeamRepository
    {
        public void SaveAll(List<Team> teams)
        {
            using (StreamWriter sw = new StreamWriter(Constants.TeamsFile, false))
            {
                foreach (var t in teams) sw.WriteLine(t.ToString());
            }
        }

        public List<Team> LoadAll()
        {
            var teams = new List<Team>();
            if (!File.Exists(Constants.TeamsFile)) return teams;

            foreach (var line in File.ReadAllLines(Constants.TeamsFile))
            {
                var data = line.Split(',');
                teams.Add(new Team(int.Parse(data[0]), data[1])
                {
                    Points = int.Parse(data[2]),
                    MatchesPlayed = int.Parse(data[3]),
                    GoalsFor = int.Parse(data[4]),
                    GoalsAgainst = int.Parse(data[5])
                });
            }
            return teams;
        }
    }
}

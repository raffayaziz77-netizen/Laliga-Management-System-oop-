using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LaLigaSystem.Models;
using LaLigaSystem.Common;

namespace LaLigaSystem.DAL
{
    public class MatchRepository
    {
        public void SaveAll(List<Match> matches)
        {
            using (StreamWriter sw = new StreamWriter(Constants.MatchesFile, false))
            {
                foreach (var m in matches) sw.WriteLine(m.ToString());
            }
        }

        public List<Match> LoadAll()
        {
            var matches = new List<Match>();
            if (!File.Exists(Constants.MatchesFile)) return matches;

            foreach (var line in File.ReadAllLines(Constants.MatchesFile))
            {
                var data = line.Split(',');
                matches.Add(new Match(int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]))
                {
                    HomeScore = int.Parse(data[3]),
                    AwayScore = int.Parse(data[4]),
                    IsPlayed = bool.Parse(data[5])
                });
            }
            return matches;
        }
    }
}
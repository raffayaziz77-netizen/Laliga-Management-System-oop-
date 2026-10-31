using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaSystem.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int MatchesPlayed { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference => GoalsFor - GoalsAgainst;

        public Team(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => $"{Id},{Name},{Points},{MatchesPlayed},{GoalsFor},{GoalsAgainst}";
    }
}

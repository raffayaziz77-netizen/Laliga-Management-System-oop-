using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaSystem.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public bool IsPlayed { get; set; }

        public Match(int id, int homeTeamId, int awayTeamId)
        {
            Id = id;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            IsPlayed = false;
        }

        public override string ToString() => $"{Id},{HomeTeamId},{AwayTeamId},{HomeScore},{AwayScore},{IsPlayed}";
    }
}

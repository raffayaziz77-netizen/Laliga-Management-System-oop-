using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaLigaSystem.DAL;
using LaLigaSystem.Models;

namespace LaLigaSystem.Logic
{
    public class MatchService
    {
        private readonly MatchRepository _matchRepo;
        private readonly TeamService _teamService;

        public MatchService(MatchRepository matchRepo, TeamService teamService)
        {
            _matchRepo = matchRepo;
            _teamService = teamService;
        }

        public List<Match> GetAllMatches() => _matchRepo.LoadAll();

        public void PlayNextMatch()
        {
            var matches = _matchRepo.LoadAll();
            var nextMatch = matches.FirstOrDefault(m => !m.IsPlayed);
            if (nextMatch == null) throw new Exception("All matches have been played.");

            Random rng = new Random();
            nextMatch.HomeScore = rng.Next(0, 5);
            nextMatch.AwayScore = rng.Next(0, 5);
            nextMatch.IsPlayed = true;

            UpdateTeamStats(nextMatch);
            _matchRepo.SaveAll(matches);
        }

        private void UpdateTeamStats(Match match)
        {
            var teams = _teamService.GetStandings();
            var home = teams.First(t => t.Id == match.HomeTeamId);
            var away = teams.First(t => t.Id == match.AwayTeamId);

            home.MatchesPlayed++;
            away.MatchesPlayed++;
            home.GoalsFor += match.HomeScore;
            home.GoalsAgainst += match.AwayScore;
            away.GoalsFor += match.AwayScore;
            away.GoalsAgainst += match.HomeScore;

            if (match.HomeScore > match.AwayScore) home.Points += 3;
            else if (match.AwayScore > match.HomeScore) away.Points += 3;
            else { home.Points += 1; away.Points += 1; }

            _teamService.UpdateTeams(teams);
        }
    }
}
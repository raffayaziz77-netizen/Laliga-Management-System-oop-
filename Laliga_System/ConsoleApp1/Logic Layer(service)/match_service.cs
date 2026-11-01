using System;
using System.Collections.Generic;
using System.Linq;
using LaLigaSystem.DAL;
using LaLigaSystem.Models;
using LaLigaSystem.Common;

namespace LaLigaSystem.Logic
{
    public class MatchService
    {
        private readonly MatchRepository _matchRepo;
        private readonly TeamService _teamService;
        private static readonly Random RandomGenerator = new Random();
        private static readonly object RandomLock = new object();

        public MatchService(MatchRepository matchRepo, TeamService teamService)
        {
            _matchRepo = matchRepo;
            _teamService = teamService;
        }

        public List<Match> GetAllMatches() { return _matchRepo.LoadAll(); }

        public void PlayNextMatch()
        {
            var matches = _matchRepo.LoadAll();
            var nextMatch = matches.FirstOrDefault(m => !m.IsPlayed);
            if (nextMatch == null)
                throw new LeagueException("There are no unplayed matches remaining.");

            int homeScore, awayScore;
            lock (RandomLock)
            {
                homeScore = RandomGenerator.Next(0, 5);
                awayScore = RandomGenerator.Next(0, 5);
            }

            var teams = _teamService.GetStandings();
            var home = teams.FirstOrDefault(t => t.Id == nextMatch.HomeTeamId);
            var away = teams.FirstOrDefault(t => t.Id == nextMatch.AwayTeamId);
            if (home == null || away == null)
                throw new LeagueException("The match references a team that does not exist. Check the team and match data files.");

            nextMatch.HomeScore = homeScore;
            nextMatch.AwayScore = awayScore;
            nextMatch.IsPlayed = true;
            home.MatchesPlayed++;
            away.MatchesPlayed++;
            home.GoalsFor += homeScore;
            home.GoalsAgainst += awayScore;
            away.GoalsFor += awayScore;
            away.GoalsAgainst += homeScore;

            if (homeScore > awayScore)
            {
                home.Points += 3; home.Wins++; away.Losses++;
            }
            else if (awayScore > homeScore)
            {
                away.Points += 3; away.Wins++; home.Losses++;
            }
            else
            {
                home.Points++; away.Points++; home.Draws++; away.Draws++;
            }

            _teamService.UpdateTeams(teams);
            _matchRepo.SaveAll(matches);
        }
    }
}
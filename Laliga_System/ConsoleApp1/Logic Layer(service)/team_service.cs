using System.Collections.Generic;
using System.Linq;
using LaLigaSystem.DAL;
using LaLigaSystem.Models;
using LaLigaSystem.Common;

namespace LaLigaSystem.Logic
{
    public class TeamService
    {
        private readonly TeamRepository _teamRepo;

        public TeamService(TeamRepository teamRepo) { _teamRepo = teamRepo; }

        public List<Team> GetStandings()
        {
            return _teamRepo.LoadAll()
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalDifference)
                .ThenByDescending(t => t.GoalsFor)
                .ToList();
        }

        public Team GetTeamById(int id)
        {
            return _teamRepo.LoadAll().FirstOrDefault(t => t.Id == id);
        }

        public void UpdateTeams(List<Team> teams) { _teamRepo.SaveAll(teams); }

        // Rebuild W/D/L from saved match results. This also migrates older team
        // files that predate the W/D/L columns without resetting league points.
        public void RebuildWinDrawLoss(IEnumerable<Match> matches)
        {
            var teams = _teamRepo.LoadAll();
            foreach (var team in teams)
            {
                team.Wins = 0;
                team.Draws = 0;
                team.Losses = 0;
            }

            foreach (var match in matches.Where(m => m.IsPlayed))
            {
                var home = teams.FirstOrDefault(t => t.Id == match.HomeTeamId);
                var away = teams.FirstOrDefault(t => t.Id == match.AwayTeamId);
                if (home == null || away == null)
                    throw new LeagueException("A played match references a team that does not exist.");

                if (match.HomeScore > match.AwayScore)
                {
                    home.Wins++;
                    away.Losses++;
                }
                else if (match.AwayScore > match.HomeScore)
                {
                    away.Wins++;
                    home.Losses++;
                }
                else
                {
                    home.Draws++;
                    away.Draws++;
                }
            }

            _teamRepo.SaveAll(teams);
        }
    }
}
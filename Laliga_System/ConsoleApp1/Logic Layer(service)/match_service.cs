using System;
using LaLigaApp.DAL;
using LaLigaApp.Models;
using System.Collections.Generic;

namespace LaLigaApp.Logic
{
    public class match_service
    {
        private readonly Match_Repo _matchRepo = new Match_Repo();
        private readonly Team_Repo _teamRepo = new Team_Repo();

        public List<Match> GetAllMatches()
        {
            return _matchRepo.GetAllMatches();
        }

        public void DeleteMatch(int id)
        {
            var matches = _matchRepo.GetAllMatches();
            if (!matches.Exists(m => m.Id == id))
            {
                Console.WriteLine("Match not found.");
                return;
            }
            _matchRepo.DeleteMatch(id);
            Console.WriteLine("Match deleted successfully. (Note: League points were not reverted)");
        }
        public void RecordMatch(int homeTeamId, int awayTeamId, int homeGoals, int awayGoals)
        {
            if (homeTeamId == awayTeamId)
            {
                Console.WriteLine("A team cannot play a match against itself.");
                return;
            }

            var teams = _teamRepo.GetAllTeams();
            var homeTeam = teams.Find(t => t.Id == homeTeamId);
            var awayTeam = teams.Find(t => t.Id == awayTeamId);

            if (homeTeam == null || awayTeam == null)
            {
                Console.WriteLine("Error: One or both teams could not be found.");
                return;
            }

            Match newMatch = new Match
            {
                HomeTeamId = homeTeamId,
                AwayTeamId = awayTeamId,
                HomeGoals = homeGoals,
                AwayGoals = awayGoals
            };

            _matchRepo.AddMatch(newMatch);

            if (homeGoals > awayGoals)
            {
                homeTeam.Points += 3;
            }
            else if (awayGoals > homeGoals)
            {
                awayTeam.Points += 3;
            }
            else
            {
                homeTeam.Points += 1;
                awayTeam.Points += 1;
            }

            _teamRepo.SaveAllTeams(teams);
            Console.WriteLine("Match recorded and league standings updated successfully.");
        }
    }
}
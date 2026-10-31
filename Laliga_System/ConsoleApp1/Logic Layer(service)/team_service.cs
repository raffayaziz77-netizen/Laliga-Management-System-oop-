using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaLigaSystem.DAL;
using LaLigaSystem.Models;

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
                .ToList();
        }

        public Team GetTeamById(int id)
        {
            return _teamRepo.LoadAll().FirstOrDefault(t => t.Id == id);
        }

        public void UpdateTeams(List<Team> teams) => _teamRepo.SaveAll(teams);
    }
}

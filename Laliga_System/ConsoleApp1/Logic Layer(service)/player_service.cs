using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaLigaSystem.DAL;
using LaLigaSystem.Models;

namespace LaLigaSystem.Logic
{
    public class PlayerService
    {
        private readonly PlayerRepository _playerRepo;
        public PlayerService(PlayerRepository playerRepo) { _playerRepo = playerRepo; }

        public List<Player> GetPlayersByTeam(int teamId)
        {
            return _playerRepo.LoadAll().Where(p => p.TeamId == teamId).ToList();
        }

        public Player SearchPlayerByName(string name)
        {
            return _playerRepo.LoadAll().FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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
            if (string.IsNullOrWhiteSpace(name)) return null;
            return _playerRepo.LoadAll().FirstOrDefault(
                p => p.Name != null && p.Name.IndexOf(name.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}
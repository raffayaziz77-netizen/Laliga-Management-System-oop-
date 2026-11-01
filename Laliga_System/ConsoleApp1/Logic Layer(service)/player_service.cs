using System;
using LaLigaApp.DAL;
using LaLigaApp.Models;
using System.Collections.Generic;

namespace LaLigaApp.Logic
{
    public class player_service
    {
        private readonly Player_Repo _playerRepo = new Player_Repo();
        private readonly Team_Repo _teamRepo = new Team_Repo();

        public List<Player> GetAllPlayers()
        {
            return _playerRepo.GetAllPlayers();
        }

        public void DeletePlayer(int id)
        {
            var players = _playerRepo.GetAllPlayers();
            if (!players.Exists(p => p.Id == id))
            {
                Console.WriteLine("Player not found.");
                return;
            }
            _playerRepo.DeletePlayer(id);
            Console.WriteLine("Player deleted successfully.");
        }
        public void AddPlayer(string name, string position, int teamId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Player name cannot be empty.");
                return;
            }

            var teams = _teamRepo.GetAllTeams();
            if (!teams.Exists(t => t.Id == teamId))
            {
                Console.WriteLine("Error: Team ID does not exist.");
                return;
            }

            Player newPlayer = new Player
            {
                Name = name,
                Position = position,
                TeamId = teamId
            };

            _playerRepo.AddPlayer(newPlayer);
            Console.WriteLine("Player registered successfully.");
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using LaLigaApp.Models;

namespace LaLigaApp.DAL
{
    public class Player_Repo
    {
        private readonly string filePath = "players.txt";

        public void DeletePlayer(int id)
        {
            var players = GetAllPlayers();
            var playerToRemove = players.Find(p => p.Id == id);
            if (playerToRemove != null)
            {
                players.Remove(playerToRemove);
                SaveAllPlayers(players);
            }
        }
        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            if (!File.Exists(filePath)) return players;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        players.Add(new Player
                        {
                            Id = int.Parse(parts[0]),
                            Name = parts[1],
                            Position = parts[2],
                            TeamId = int.Parse(parts[3])
                        });
                    }
                }
            }
            return players;
        }

        public void SaveAllPlayers(List<Player> players)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var player in players)
                {
                    writer.WriteLine($"{player.Id},{player.Name},{player.Position},{player.TeamId}");
                }
            }
        }

        public void AddPlayer(Player player)
        {
            var players = GetAllPlayers();
            player.Id = players.Count > 0 ? players[players.Count - 1].Id + 1 : 1;
            players.Add(player);
            SaveAllPlayers(players);
        }
    }
}
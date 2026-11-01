using System;
using System.Collections.Generic;
using System.IO;
using LaLigaSystem.Models;
using LaLigaSystem.Common;

namespace LaLigaSystem.DAL
{
    public class PlayerRepository
    {
        public void SaveAll(List<Player> players)
        {
            if (players == null) throw new LeagueException("Player data cannot be null.");
            using (var writer = new StreamWriter(Constants.PlayersFile, false))
                foreach (var player in players) writer.WriteLine(player.ToString());
        }

        public List<Player> LoadAll()
        {
            var players = new List<Player>();
            if (!File.Exists(Constants.PlayersFile)) return players;

            int lineNumber = 0;
            foreach (var line in File.ReadAllLines(Constants.PlayersFile))
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(line)) continue;
                var data = line.Split(',');
                int id, teamId, goals, assists;
                PlayerPosition position;

                if (data.Length != 6 ||
                    !int.TryParse(data[0], out id) ||
                    !int.TryParse(data[2], out teamId) ||
                    !Enum.TryParse(data[3], true, out position) ||
                    !Enum.IsDefined(typeof(PlayerPosition), position) ||
                    !int.TryParse(data[4], out goals) ||
                    !int.TryParse(data[5], out assists) ||
                    string.IsNullOrWhiteSpace(data[1]))
                    throw new LeagueException("Invalid player data at line " + lineNumber + " in " + Constants.PlayersFile + ".");

                players.Add(new Player(id, data[1], teamId, position) { Goals = goals, Assists = assists });
            }
            return players;
        }
    }
}
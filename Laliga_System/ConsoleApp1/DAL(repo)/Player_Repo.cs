using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LaLigaSystem.Models;
using LaLigaSystem.Common;

namespace LaLigaSystem.DAL
{
    public class PlayerRepository
    {
        public void SaveAll(List<Player> players)
        {
            using (StreamWriter sw = new StreamWriter(Constants.PlayersFile, false))
            {
                foreach (var p in players) sw.WriteLine(p.ToString());
            }
        }

        public List<Player> LoadAll()
        {
            var players = new List<Player>();
            if (!File.Exists(Constants.PlayersFile)) return players;

            foreach (var line in File.ReadAllLines(Constants.PlayersFile))
            {
                var data = line.Split(',');
                players.Add(new Player(int.Parse(data[0]), data[1], int.Parse(data[2]), data[3])
                {
                    Goals = int.Parse(data[4]),
                    Assists = int.Parse(data[5])
                });
            }
            return players;
        }
    }
}
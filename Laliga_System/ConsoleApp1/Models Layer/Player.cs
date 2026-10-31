using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaSystem.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public string Position { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }

        public Player(int id, string name, int teamId, string position)
        {
            Id = id;
            Name = name;
            TeamId = teamId;
            Position = position;
        }

        public override string ToString() => $"{Id},{Name},{TeamId},{Position},{Goals},{Assists}";
    }
}

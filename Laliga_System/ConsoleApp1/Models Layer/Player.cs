namespace LaLigaSystem.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public PlayerPosition Position { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }

        public Player(int id, string name, int teamId, PlayerPosition position)
        {
            Id = id;
            Name = name;
            TeamId = teamId;
            Position = position;
        }

        public override string ToString()
        {
            return string.Join(",", Id, Name, TeamId, Position, Goals, Assists);
        }
    }
}
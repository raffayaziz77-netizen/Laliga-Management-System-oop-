namespace LaLigaSystem.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get { return GoalsFor - GoalsAgainst; } }

        public Team(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // Keep the original six-column file format readable in TeamRepository.
        public override string ToString()
        {
            return string.Join(",", Id, Name, Points, MatchesPlayed, Wins, Draws, Losses, GoalsFor, GoalsAgainst);
        }
    }
}
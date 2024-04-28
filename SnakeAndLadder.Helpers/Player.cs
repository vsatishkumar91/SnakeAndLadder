namespace SnakeAndLadder.Helpers
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PlayerStats Stats { get; set; } = new PlayerStats();

        public int PlayerPosition { get; set; } = 0;


        public Player(int playerId, string name, string description) {
            PlayerId = playerId;
            Name = name;
            Description = description;
        }
    }
}

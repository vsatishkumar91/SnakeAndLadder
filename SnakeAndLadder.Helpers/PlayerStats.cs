namespace SnakeAndLadder.Helpers
{
    public class PlayerStats
    {
        public int TotalPlayed { get; set; }
        public int TotalWon { get; set; }
        public int TotalLost { get; set; }

        public PlayerStats() { 
            TotalLost = 0;
            TotalPlayed = 0;
            TotalWon = 0;
        }

    }
}

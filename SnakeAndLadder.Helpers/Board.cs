using System.Collections.Generic;

namespace SnakeAndLadder.Helpers
{
    public class Board
    {
        public int BoardWidth {  get; set; }
        public int BoardHeight { get; set;}
        public int NoOfLadders { get; set; }
        public int NoOfSnakes { get; set; }
        public int NoOfPlayers { get; set; }
        public int NoOfDices { get; set; }

        public List<Snake> Snakes { get; set; } = new List<Snake>();
        public List<Ladder> Ladders { get; set; } = new List<Ladder>();
        public List<Player> Players { get; set; } = new List<Player>();

        public Board(int boardWidth=10, int boardHeight = 10, int noOfLadders = 10, int noOfSnakes = 10, int noOfDices = 1)
        {
            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
            NoOfLadders = noOfLadders;
            NoOfSnakes = noOfSnakes;
            NoOfDices = noOfDices;
        }

        public void StartGame()
        {
            CreateBoard();
            Random ran = new Random();
            bool gameOver = false;
            while(gameOver == false)
            {
                for(int i=0;i<Players.Count;i++)
                {
                    var diceScore = ran.Next(1, 6);
                    var playerScore = Players[i].PlayerPosition + diceScore;
                    if(playerScore == BoardHeight * BoardWidth)
                    {
                        Players[i].PlayerPosition = playerScore;
                        Console.WriteLine("Player won");
                        gameOver = true;
                        Players[i].Stats.TotalWon++;
                    } else if(playerScore > BoardWidth *BoardHeight)
                    {
                        break;
                    }
                    else
                    {
                        if (Ladders.Where(x=>x.StartPoint == playerScore).Any())
                        {
                            var destination = Ladders.Find(x => x.StartPoint == playerScore).EndPoint;
                            Console.WriteLine(Players[i].Name + " -> took Ladder from " + playerScore + " to " + destination);
                            Players[i].PlayerPosition = destination;
                        } else if (Snakes.Where(x => x.StartPoint == playerScore).Any())
                        {
                            var destination = Snakes.Find(x => x.StartPoint == playerScore).EndPoint;
                            Console.WriteLine(Players[i].Name + " -> caught by Snake at " + playerScore + " take him/her to " + destination);
                            Players[i].PlayerPosition = destination;

                        }
                        else
                        {
                            Players[i].PlayerPosition = playerScore;
                        }
                    }
                    Console.WriteLine(Players[i].Name + " +" + diceScore + " -> "  + Players[i].PlayerPosition);
                }
            }
        }

        private void CreateBoard()
        {
            bool isValidBoard;
            do
            {
                createSnakes();
                createLadders();
                isValidBoard = checkIsvalidBoard();
            } while (isValidBoard == false);
        }

        private void createSnakes()
        {
            Random ran = new Random();
            Snakes = new List<Snake>();
            Console.WriteLine("Snakes");
            for (int i = 0; i < NoOfSnakes; i++) {
                int getRanNum1 = ran.Next(1, BoardHeight*BoardWidth);
                int getRanNum2 = ran.Next(1,BoardHeight * BoardWidth);
                Snakes.Add(new Snake(Math.Max(getRanNum1, getRanNum2), Math.Min(getRanNum1, getRanNum2)));
                Console.WriteLine(Snakes[i].StartPoint + "," + Snakes[i].EndPoint);
            }
        }

        private void createLadders()
        {
            Random ran = new Random();
            Ladders = new List<Ladder>();
            Console.WriteLine("Ladders");
            for (int i = 0; i < NoOfLadders; i++)
            {
                int getRanNum1 = ran.Next(1, BoardHeight * BoardWidth);
                int getRanNum2 = ran.Next(1, BoardHeight * BoardWidth);
                Ladders.Add(new Ladder(Math.Min(getRanNum1, getRanNum2), Math.Max(getRanNum1, getRanNum2)));
                Console.WriteLine(Ladders[i].StartPoint + "," + Ladders[i].EndPoint);
            }
        }

        private bool checkIsvalidBoard()
        {
            return checkRange(Snakes, Ladders);
        }

        private bool checkRange(List<Snake> snakes, List<Ladder> ladders)
        {
            bool isValid = true;

            isValid = snakes.All(x => x.StartPoint != x.EndPoint);
            isValid = ladders.All(x => x.StartPoint != x.EndPoint);
            isValid = !snakes.GroupBy(x => x.StartPoint).Any(x => x.Skip(1).Any());
            isValid = !ladders.GroupBy(x => x.StartPoint).Any(x => x.Skip(1).Any());
            isValid = !snakes.GroupBy(x => x.EndPoint).Any(x => x.Skip(1).Any());
            isValid = !ladders.GroupBy(x => x.EndPoint).Any(x => x.Skip(1).Any());
            for (int i = 0; i < snakes.Count; i++)
            {
                for (int j = 0; j < snakes.Count; j++)
                {
                    if (i != j && snakes[i].StartPoint == snakes[j].EndPoint)
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid == false)
                {
                    break;
                }
            }

            for (int i = 0; i < ladders.Count; i++)
            {
                for (int j = 0; j < ladders.Count; j++)
                {
                    if (i != j && ladders[i].StartPoint == ladders[j].EndPoint)
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid == false)
                {
                    break;
                }
            }

            for (int i = 0; i < snakes.Count; i++)
            {
                for (int j = 0; j < ladders.Count; j++)
                {
                    if (ladders[j].EndPoint == snakes[i].StartPoint || ladders[j].StartPoint == snakes[i].EndPoint)
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid == false)
                {
                    break;
                }
            }

            return isValid;
        }
    }
}

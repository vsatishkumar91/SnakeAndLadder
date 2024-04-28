// See https://aka.ms/new-console-template for more information

using SnakeAndLadder.Helpers;

Player p1 = new Player(1, "P1", "");
Player p2 = new Player(2, "P2", "");
Board board = new Board();
board.Players.Add(p1);
board.Players.Add(p2);

board.StartGame();
Console.WriteLine("Hello, World!");

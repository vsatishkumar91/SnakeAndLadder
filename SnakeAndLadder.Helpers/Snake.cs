namespace SnakeAndLadder.Helpers
{
    public class Snake: Range
    {
        public Snake(int startPoint, int endPoint) {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }

    public class Range
    {
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}

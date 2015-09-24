namespace Hadouken.RealSense {
    public class Hand {
        public Hand(Side side) {
            Side = side;
        }
        public Side Side { get; set; }
        public bool Open { get; set; }
        public bool Visible { get; set; }
        public int Distance { get; set; }
    }
}
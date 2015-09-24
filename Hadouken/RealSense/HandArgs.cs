using System;

namespace Hadouken.RealSense {
    public class HandArgs : EventArgs {
        public Hand HandLeft { get; set; }
        public Hand HandRight { get; set; }

        public HandArgs(Hand handLeft, Hand handRight) {
            HandLeft = handLeft;
            HandRight = handRight;
        }
    }
}
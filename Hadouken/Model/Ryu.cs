using Dear;
using Dear.KeyboardControl;

namespace Hadouken.Model {
    public class Ryu {

        private IKeyboard _keyboard;

        public Ryu() {
            _keyboard = new MrWindows().Keyboard;
        }

        public void DoHadouken() {
            _keyboard.Press(VirtualKey.Down).Wait(20)
                     .Press(VirtualKey.Right).Wait(20)
                     .Release(VirtualKey.Down)
                     .Press(VirtualKey.X).Wait(20)
                     .Release(VirtualKey.Right)
                     .Release(VirtualKey.X);
        }
    }
}

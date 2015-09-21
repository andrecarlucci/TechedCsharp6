using Dear.KeyboardControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dear;

namespace Hadouken.Services {
    public class Hadouken : ISpecialAttack {

        public string Name {
            get {
                return "hadouken";
            }
        }

        public void DoIt(IKeyboard keyboard) {
            keyboard.Press(VirtualKey.Down).Wait(30)
                    .Press(VirtualKey.Right).Wait(30)
                    .Release(VirtualKey.Down)
                    .Press(VirtualKey.X).Wait(30)
                    .Release(VirtualKey.Right)
                    .Release(VirtualKey.X);
        }
    }
}

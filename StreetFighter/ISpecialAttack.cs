using Dear.KeyboardControl;

namespace Hadouken.Services {
    public interface ISpecialAttack {
        string Name { get; }
        void DoIt(IKeyboard keyboard);
}
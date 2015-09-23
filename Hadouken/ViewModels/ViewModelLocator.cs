using Hadouken.Model;
using Hadouken.Util;
using SharpSenses;

namespace Hadouken.ViewModels {
    public class ViewModelLocator {

        public MainViewModel MainViewModel { get; set; }

        public ViewModelLocator() {
            MainViewModel = new MainViewModel(Camera.Create(), new Ryu(), new Logger());
        }
    }
}

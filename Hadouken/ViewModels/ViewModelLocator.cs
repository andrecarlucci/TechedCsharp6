using Hadouken.Model;
using Hadouken.RealSense;
using Hadouken.Util;

namespace Hadouken.ViewModels {
    public class ViewModelLocator {

        public MainViewModel MainViewModel { get; set; }

        public ViewModelLocator() {
            
            MainViewModel = new MainViewModel(new RsCamera(), new Ryu(), new Logger());
        }
    }
}
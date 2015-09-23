using Hadouken.Model;
using SharpSenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadouken.ViewModels {
    public class ViewModelLocator {

        public MainViewModel MainViewModel { get; set; }

        public ViewModelLocator() {
            MainViewModel = new MainViewModel(Camera.Create(), new Ryu());
        }
    }
}

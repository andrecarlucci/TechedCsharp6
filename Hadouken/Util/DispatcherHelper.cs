using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Hadouken.Util {
    public static class DispatcherHelper {

        private static Dispatcher Dispatcher {
            get {
                return Application.Current.Dispatcher;
            }
        }

        public static void RunInUI(Action action) {
            if (Application.Current.Dispatcher.CheckAccess()) {
                action.Invoke();
            }
            Dispatcher.BeginInvoke(action);
        }

        public static void RunInUI(Func<Task> action) {
            RunInUI(action);
        }
    }
}

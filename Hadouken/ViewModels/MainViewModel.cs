using Hadouken.Model;
using SharpSenses;
using SharpSenses.Gestures;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace Hadouken.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {
        private readonly ICamera _camera;
        private readonly Ryu _ryu;
        private bool _realSenseOn;
        private string _progressText;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title {
            get {
                return "Hadouken!";
            }
        }

        public bool RealSenseOn {
            get { return _realSenseOn; }
            set {
                if (_realSenseOn == value) {
                    return;
                }
                _realSenseOn = value;
                OnPropertyChanged(new PropertyChangedEventArgs("RealSenseOn"));
            }
        }

        public string ProgressText {
            get { return _progressText; }
            set {
                if (_progressText == value) {
                    return;
                }
                _progressText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProgressText"));
            }
        }

        public MainViewModel(ICamera camera, Ryu ryu) {
            _camera = camera;
            _ryu = ryu;
        }

        public void TurnRealSenseOn() {
            RealSenseOn = true;
            _camera.LeftHand.Visible += (s, a) => {
                ProgressText = "Ready!";
            };
            var movement = Movement.Forward(_camera.LeftHand, 8);
            movement.Progress += p => {

            };
            movement.Completed += () => {
                DoHadoukenUI();
            };
            movement.Activate();
            _camera.Start();
        }

        public async Task DoHadouken() {
            await Task.Delay(3000);
            _ryu.DoHadouken();
        }

        public void DoHadoukenUI() {
            Application.Current.Dispatcher.BeginInvoke(new Action(async()=>await DoHadouken()));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) {
            var handler = PropertyChanged;
            if (handler != null) {
                handler.Invoke(this, e);
            }
        }
    }
}
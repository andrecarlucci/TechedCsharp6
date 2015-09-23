using Hadouken.Model;
using Hadouken.Util;
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
        private readonly Logger _logger;
        private bool _realSenseOn;
        private int _progressValue;
        private string _progressText;
        private int _hadoukenCount;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Title {
            get {
                return String.Format("TechEd Hadouken! Hoje é {0:d}. Hadoukens: {1}",
                                     DateTime.Today, HadoukenCount);
            }
        }

        public bool RealSenseOn {
            get { return _realSenseOn; }
            set {
                if (_realSenseOn == value) {
                    return;
                }
                _realSenseOn = value;
                OnPropertyChanged("RealSenseOn");
            }
        }

        public string ProgressText {
            get { return _progressText; }
            set {
                if (_progressText == value) {
                    return;
                }
                _progressText = value;
                OnPropertyChanged("ProgressText");
            }
        }
        public int ProgressValue {
            get { return _progressValue; }
            set {
                if (_progressValue == value) {
                    return;
                }
                _progressValue = value;
                OnPropertyChanged("ProgressValue");
            }
        }

        public int HadoukenCount {
            get { return _hadoukenCount; }
            private set {
                if (_hadoukenCount == value) {
                    return;
                }
                _hadoukenCount = value;
                OnPropertyChanged("Title");
            }
        }

        public MainViewModel(ICamera camera, Ryu ryu, Logger logger) {
            _camera = camera;
            _ryu = ryu;
            _logger = logger;
            ConfigureCamera();
        }

        public async Task TurnRealSenseOn() {
            try {
                await Task.Run(() => {
                    _camera.Start();
                });
            }
            catch (CameraException ex)  {
                MessageBox.Show("Não consegui detectar a câmera");
                _logger.Debug(ex.ToString());
            }
        }

        private void ConfigureCamera() {
            _camera.LeftHand.Visible += (s, a) => {
                ProgressText = "Ready!";
            };
            _camera.LeftHand.NotVisible += (s, a) => {
                ProgressText = "Show your hand";
            };
            var movement = Movement.Forward(_camera.LeftHand, 8);
            movement.Progress += p => {
                ProgressValue = (int) p;
            };
            movement.Completed += async () => {
                await DoTheHadouken();
            };
            movement.Activate();
        }

        public async Task DoTheHadouken(int delay) {
            await Task.Delay(delay);
            await DoTheHadouken();
        }

        public async Task DoTheHadouken() {
            ProgressText = "Hadouken!";
            _ryu.DoHadouken();
            await Task.Delay(2000);
            ProgressText = "Show your hand";
            HadoukenCount++;
        }

        protected virtual void OnPropertyChanged(string propertyName) {
            var handler = PropertyChanged;
            if (handler == null) {
                return;
            }
            DispatcherHelper.RunInUI(() => {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }
    }
}
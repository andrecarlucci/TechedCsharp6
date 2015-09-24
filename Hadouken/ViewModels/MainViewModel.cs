using Hadouken.Model;
using Hadouken.Util;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Hadouken.RealSense;
using Hadouken.Services;

namespace Hadouken.ViewModels {
    public class MainViewModel : INotifyPropertyChanged {
        private readonly RsCamera _camera;
        private readonly Ryu _ryu;
        private readonly Logger _logger;
        private readonly LightService _lightService;
        private bool _realSenseOn;
        private int _progressValue;
        private string _progressText;
        private int _hadoukenCount;
        private bool _ready;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel(RsCamera camera, Ryu ryu, Logger logger, LightService lightService) {
            _camera = camera;
            _ryu = ryu;
            _logger = logger;
            _lightService = lightService;
            ConfigureCamera();
        }

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

        public async Task TurnRealSenseOn() {
            try {
                await Task.Run(() => {
                    _camera.Init();
                    RealSenseOn = true;
                });
            }
            catch (Exception ex)  {
                MessageBox.Show("Não consegui detectar a câmera");
                _logger.Debug(ex.ToString());
            }
        }

        private const int Max = 36;
        private const int Min = 18;

        private void ConfigureCamera() {
            _camera.HandChanged += (sender, args) => {
                var left = args.HandLeft;
                if (_ready) {
                    ProgressValue = ToProgressScale(left.Distance);
                }
                if (_ready && left.Distance <= Min) {
                    DoTheHadouken().Wait();
                    _ready = false;
                    return;
                }
                if (left.Visible && left.Open && left.Distance > Max) {
                    _ready = true;
                    ProgressText = "Ready!";
                    return;
                }
                if (!left.Visible) {
                    _ready = false;
                    ProgressValue = 0;
                    ProgressText = "Prepare!";
                }
            };
        }

        private int ToProgressScale(int value) {
            return Max + Min - value - 5;
        }

        public async Task DoTheHadouken(int delay) {
            await Task.Delay(delay);
            await DoTheHadouken();
        }

        public async Task DoTheHadouken() {
            ProgressText = "Hadouken!!!";
            _ryu.DoHadouken();
            _lightService.Blink();
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
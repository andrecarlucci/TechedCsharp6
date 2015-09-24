using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hadouken.RealSense {
    public class RsCamera {
        public static pxcmStatus NoError = pxcmStatus.PXCM_STATUS_NO_ERROR;

        public PXCMSenseManager _manager;

        public event EventHandler<HandArgs> HandChanged;

        private Hand _handLeft = new Hand(Side.Left);
        private Hand _handRight = new Hand(Side.Right);

        public void Init() {
            var session = PXCMSession.CreateInstance();
            _manager = session.CreateSenseManager();
            _manager.EnableHand();
            var status = _manager.Init();
            if (status != NoError) {
                throw new Exception(status.ToString());
            }
            Task.Factory.StartNew(Loop,
                TaskCreationOptions.LongRunning);
        }

        private void Loop(object obj) {
            Debug.WriteLine("Loop started");
            var handModule = _manager.QueryHand();
            var handData = handModule.CreateOutput();
            while (true) {
                _manager.AcquireFrame(true);
                handData.Update();

                ExtractHand(handData, _handLeft, PXCMHandData.AccessOrderType.ACCESS_ORDER_LEFT_HANDS);
                ExtractHand(handData, _handRight, PXCMHandData.AccessOrderType.ACCESS_ORDER_RIGHT_HANDS);

                OnHandChanged(new HandArgs(_handLeft, _handRight));

                _manager.ReleaseFrame();
            }
        }

        private void ExtractHand(PXCMHandData handData, Hand hand, PXCMHandData.AccessOrderType access) {
            PXCMHandData.IHand handInfo;
            if (handData.QueryHandData(access, 0, out handInfo) != NoError) {
                hand.Visible = false;
                return;
            }
            hand.Visible = true;
            var openness = handInfo.QueryOpenness();
            hand.Open = openness > 75;
            var position = handInfo.QueryMassCenterWorld();
            hand.Distance = (int)(position.z * 100);
        }

        protected virtual void OnHandChanged(HandArgs e) {
            HandChanged?.Invoke(this, e);
        }
    }
}
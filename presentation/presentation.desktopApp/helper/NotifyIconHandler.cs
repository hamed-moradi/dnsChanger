using domain.office.containers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentation.desktopApp.helper {
    sealed class NotifyIconHandler {
        #region ctor
        private static NotifyIconUtility _instance;
        private static readonly object _syncLock = new object();
        public NotifyIconHandler() { }
        #endregion

        #region private
        private static NotifyIconUtility PrepareIcon() {
            var notifyIconUtility = new NotifyIconUtility {
                Icon = new Icon($@"{Application.StartupPath}\app.ico")
            };

            notifyIconUtility.NotifyIcon.Visible = true;
            return notifyIconUtility;
        }
        #endregion

        public static NotifyIconUtility Instance {
            get {
                if(_instance == null) {
                    lock(_syncLock) {
                        if(_instance == null) {
                            _instance = PrepareIcon();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}

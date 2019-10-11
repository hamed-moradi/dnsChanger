using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace domain.office._app {
    public class SysInfo {
        private readonly static bool _isWindowsNT = Environment.OSVersion.Platform == PlatformID.Win32NT;

        public static bool IsWindowsVistaOrLater {
            get { return _isWindowsNT && Environment.OSVersion.Version >= new Version(6, 0, 0); }
        }
        public static bool IsWindows7OrLater {
            get { return _isWindowsNT && Environment.OSVersion.Version >= new Version(6, 0, 7600); }
        }

        public static bool ForegroundWindowIsFullScreen {
            get {
                var foreWindow = NativeMethods.GetForegroundWindow();

                NativeMethods.RECT foreRect;
                NativeMethods.GetWindowRect(foreWindow, out foreRect);

                var screenSize = System.Windows.Forms.SystemInformation.PrimaryMonitorSize;

                return (foreRect.left <= 0 && foreRect.top <= 0 &&
                    foreRect.right >= screenSize.Width && foreRect.bottom >= screenSize.Height);
            }
        }

        public static bool IsRemoteSession {
            get {
                return System.Windows.Forms.SystemInformation.TerminalServerSession;
                //return (SystemParameters.IsRemoteSession || SystemParameters.IsRemotelyControlled);
                //can't use the above since they were introduced with .net 4.0
            }
        }

        public static bool IsDWMEnabled {
            get {
                if(!IsWindowsVistaOrLater)
                    return false;
                bool result;
                NativeMethods.DwmIsCompositionEnabled(out result);
                return result;
            }
        }
    }
}

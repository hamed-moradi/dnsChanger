using Presentation.DesktopApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Presentation.DesktopApp.Helper {
    public class NotifyAreaUtility {
        public static Rectangle GetRectangle() {
            var hTaskbarHandle = NativeMethods.FindWindow("Shell_TrayWnd", null);
            if(hTaskbarHandle != IntPtr.Zero) {
                var hSystemTray = NativeMethods.FindWindowEx(hTaskbarHandle, IntPtr.Zero, "TrayNotifyWnd", null);
                if(hSystemTray != IntPtr.Zero) {
                    NativeMethods.RECT rect;
                    NativeMethods.GetWindowRect(hSystemTray, out rect);
                    if(rect.HasSize())
                        return rect;
                }
            }

            return Rectangle.Empty;
        }

        /// <summary>
        /// Retrieves the rectangle of the 'Show Hidden Icons' button, or null if it can't be found.
        /// </summary>
        /// <returns>Rectangle containing bounds of 'Show Hidden Icons' button, or null if it can't be found.</returns>
        public static Rectangle GetButtonRectangle() {
            // find the handle of the taskbar
            IntPtr taskbarparenthandle = NativeMethods.FindWindow("Shell_TrayWnd", null);

            if(taskbarparenthandle == (IntPtr)null)
                return Rectangle.Empty;

            // find the handle of the notification area
            var naparenthandle = NativeMethods.FindWindowEx(taskbarparenthandle, IntPtr.Zero, "TrayNotifyWnd", null);

            if(naparenthandle == (IntPtr)null)
                return Rectangle.Empty;

            var nabuttonwindows = NativeMethods.GetChildButtonWindows(naparenthandle);

            if(nabuttonwindows.Count == 0)
                return Rectangle.Empty; // found no buttons

            var buttonpointer = nabuttonwindows[0]; // just take the first button

            NativeMethods.RECT result;

            if(!NativeMethods.GetWindowRect(buttonpointer, out result))
                return Rectangle.Empty; // return null if we can't find the button

            return result;
        }
    }
}

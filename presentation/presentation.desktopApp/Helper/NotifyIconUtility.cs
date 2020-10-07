using Presentation.DesktopApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Presentation.DesktopApp.Helper {
    public class DropRefreshedEventArgs: EventArgs {
        public bool Successful { get; internal set; }
        public DropRefreshedEventArgs() { }
    }

    public class NotifyIconUtility {
        public NotifyIcon NotifyIcon { get; set; }

        private string _text;
        Color nearColor;
        bool nearColorSet = false;
        NotifyIcon _notifyIcon;

        public NotifyIconUtility(NotifyIcon notifyIcon) {
            _notifyIcon = notifyIcon;
        }

        public NotifyIconUtility() {
            AutoIconCleanup = true;
            NotifyIcon = new NotifyIcon();
            Animation = new Animation() { _parent = this };
        }

        // These don't attach/detach from the events in FormDrop as we should allow attaching and detaching even
        // when FormDrop isn't initialised.
        public event DragEventHandler DragDrop;
        public event DragEventHandler DragEnter;
        public event EventHandler DragLeave;
        public event DragEventHandler DragOver;

        internal void HandleDragDrop(object sender, DragEventArgs e) {
            DragDrop?.Invoke(sender, e);
        }

        internal void HandleDragEnter(object sender, DragEventArgs e) {
            DragEnter?.Invoke(sender, e);
        }

        internal void HandleDragLeave(object sender, EventArgs e) {
            DragLeave?.Invoke(sender, e);
        }

        internal void HandleDragOver(object sender, DragEventArgs e) {
            DragOver?.Invoke(sender, e);
        }

        // Call 1-800-DropRefreshed
        public delegate void DropRefreshedEventHandler(object sender, DropRefreshedEventArgs e);
        public event DropRefreshedEventHandler DropRefreshed;

        internal void DropRefreshCallback(bool successful) {
            DropRefreshed?.Invoke(this, new DropRefreshedEventArgs() { Successful = successful });
        }

        public void Dispose() {
            GC.SuppressFinalize(this); // The finalise process no longer needs to be run for this
        }
        public string Text {
            get { return _text; }
            set {
                // Code from http://stackoverflow.com/q/579665/580264#580264
                if(value.Length >= 128)
                    throw new ArgumentOutOfRangeException("ToolTip text must be less than 128 characters long");
                _text = value;
                Type t = typeof(NotifyIcon);
                BindingFlags hidden = BindingFlags.NonPublic | BindingFlags.Instance;
                t.GetField("text", hidden).SetValue(NotifyIcon, _text);
                if((bool)t.GetField("added", hidden).GetValue(NotifyIcon))
                    t.GetMethod("UpdateIcon", hidden).Invoke(NotifyIcon, new object[] { true });
            }
        }

        public bool AutoIconCleanup { get; set; }
        public Icon Icon {
            get { return NotifyIcon.Icon; }
            set {
                // Automatically cleaning up the icon helps a lot with GDI handle usage
                IntPtr oldIconHandle = NotifyIcon.Icon == null ? IntPtr.Zero : NotifyIcon.Icon.Handle;
                NotifyIcon.Icon = value;
                if(AutoIconCleanup && oldIconHandle != IntPtr.Zero)
                    DestroyIcon(oldIconHandle);
            }
        }

        public Animation Animation { get; private set; }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        public Point? GetLocation() {
            return GetLocation(0);
        }
        public Point? GetLocation(int accuracy) {
            return GetLocation(accuracy, false);
        }
        public Point? GetLocation(bool tryReturnIfHidden) {
            return GetLocation(0, tryReturnIfHidden);
        }

        public Point? GetLocation(int accuracy, bool tryReturnIfHidden) {
            // Try using APIs first
            var rect = GetNotifyIconRectangle(NotifyIcon, tryReturnIfHidden);
            if(!rect.IsEmpty)
                return rect.Location;

            // Don't fallback if the icon isn't visible
            if(!tryReturnIfHidden) {
                var rect2 = GetNotifyIconRectangle(NotifyIcon, true);
                if(rect2.IsEmpty)
                    return null;
            }

            // Ugly fallback time :(
            Point? point = GetLocation2(accuracy);
            if(point.HasValue)
                return point;

            return null;
        }

        public static Rectangle GetNotifyIconRectangle(NotifyIcon notifyIcon, bool returnIfHidden) {
            Rectangle? rect;

            if(SysInfo.IsWindows7OrLater)
                rect = GetNotifyIconRectangleWin7(notifyIcon, returnIfHidden);
            else
                rect = GetNotifyIconRectangleLegacy(notifyIcon, returnIfHidden);

            if(rect.HasValue)
                return rect.Value;

            return Rectangle.Empty;
        }

        /// <summary>
        /// Determines whether the specified System.Windows.Forms.NotifyIcon is contained within the Windows 7 notification area fly-out.
        /// Note that this function will return false if the fly-out is closed, or if run on older versions of Windows.
        /// </summary>
        /// <param name="notifyIcon">Notify icon to test.</param>
        /// <returns>True if the notify icon is in the fly-out, false if not.</returns>
        public static bool IsNotifyIconInFlyOut(NotifyIcon notifyIcon) {
            if(!SysInfo.IsWindows7OrLater)
                return false;

            Rectangle notifyIconRect = GetNotifyIconRectangle(notifyIcon, true);
            if(notifyIconRect.IsEmpty)
                return false;

            return IsRectangleInFlyOut(notifyIconRect);
        }

        /// <summary>
        /// Determines whether the specified System.Drawing.Rectangle is contained within the Windows 7 notification area fly-out.
        /// Note that this function will return false if the fly-out is closed, or if run on older versions of Windows.
        /// </summary>
        /// <param name="point">System.Drawing.Rectangle to test.</param>
        /// <returns>True if the notify icon is in the fly-out, false if not.</returns>
        public static bool IsRectangleInFlyOut(Rectangle rectangle) {
            if(!SysInfo.IsWindows7OrLater)
                return false;

            Rectangle taskbarRect = Taskbar.GetTaskbarRectangle();

            // Don't use Rectangle.IntersectsWith since we want to check if it's ENTIRELY inside
            return (rectangle.Left > taskbarRect.Right || rectangle.Right < taskbarRect.Left
                 || rectangle.Bottom < taskbarRect.Top || rectangle.Top > taskbarRect.Bottom);
        }

        /// <summary>
        /// Checks whether a point is within the bounds of the specified notify icon.
        /// </summary>
        /// <param name="point">Point to check.</param>
        /// <param name="notifyicon">Notify icon to check.</param>
        /// <returns>True if the point is contained in the bounds, false otherwise.</returns>
        public static bool IsPointInNotifyIcon(Point point, NotifyIcon notifyicon) {
            Rectangle? nirect = GetNotifyIconRectangle(notifyicon, true);
            if(nirect == null)
                return false;
            return ((Rectangle)nirect).Contains(point);
        }

        private static bool CanGetNotifyIconIdentifier(NotifyIcon notifyIcon,
            out NativeMethods.NOTIFYICONIDENTIFIER identifier) {
            // You can either use uID + hWnd or a GUID, but GUID is new to Win7 and isn't used by NotifyIcon anyway.

            identifier = new NativeMethods.NOTIFYICONIDENTIFIER();

            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var niType = typeof(NotifyIcon);

            var idFieldInfo = niType.GetField("id", flags);
            identifier.uID = (uint)(int)idFieldInfo.GetValue(notifyIcon);

            var windowFieldInfo = niType.GetField("window", flags);
            var nativeWindow = (NativeWindow)windowFieldInfo.GetValue(notifyIcon);
            identifier.hWnd = nativeWindow.Handle;

            if(identifier.hWnd == null || identifier.hWnd == IntPtr.Zero)
                return false;

            identifier.cbSize = (uint)Marshal.SizeOf(identifier);
            return true;
        }

        private static Rectangle? GetNotifyIconRectangleWin7(NotifyIcon notifyIcon, bool returnIfHidden) {
            // Get the identifier
            NativeMethods.NOTIFYICONIDENTIFIER identifier;
            if(!CanGetNotifyIconIdentifier(notifyIcon, out identifier))
                return null;

            // And plug it in to get our rectangle!
            var iconLocation = new NativeMethods.RECT();
            int result = NativeMethods.Shell_NotifyIconGetRect(ref identifier, out iconLocation);
            Rectangle rect = iconLocation;

            // 0 means success, 1 means the notify icon is in the fly-out - either is fine
            if((result == 0 || (result == 1 && returnIfHidden)) && rect.Width > 0 && rect.Height > 0)
                return iconLocation;
            else
                return null;
        }

        private static Rectangle? GetNotifyIconRectangleLegacy(NotifyIcon notifyIcon, bool returnIfHidden) {
            Rectangle? nirect = null;

            NativeMethods.NOTIFYICONIDENTIFIER niidentifier;
            if(!CanGetNotifyIconIdentifier(notifyIcon, out niidentifier))
                return null;

            // find the handle of the task bar
            IntPtr taskbarparenthandle = NativeMethods.FindWindow("Shell_TrayWnd", null);

            if(taskbarparenthandle == IntPtr.Zero)
                return null;

            // find the handle of the notification area
            IntPtr naparenthandle = NativeMethods.FindWindowEx(taskbarparenthandle, IntPtr.Zero, "TrayNotifyWnd", null);

            if(naparenthandle == IntPtr.Zero)
                return null;

            // make a list of toolbars in the notification area (one of them should contain the icon)
            List<IntPtr> natoolbarwindows = NativeMethods.GetChildToolbarWindows(naparenthandle);

            bool found = false;

            for(int i = 0; !found && i < natoolbarwindows.Count; i++) {
                IntPtr natoolbarhandle = natoolbarwindows[i];

                // retrieve the number of toolbar buttons (i.e. notify icons)
                int buttoncount = NativeMethods.SendMessage(natoolbarhandle, NativeMethods.TB_BUTTONCOUNT, IntPtr.Zero, IntPtr.Zero).ToInt32();

                // get notification area's process id
                uint naprocessid;
                NativeMethods.GetWindowThreadProcessId(natoolbarhandle, out naprocessid);

                // get handle to notification area's process
                IntPtr naprocesshandle = NativeMethods.OpenProcess(NativeMethods.ProcessAccessFlags.All, false, naprocessid);

                if(naprocesshandle == IntPtr.Zero)
                    return null;

                // allocate enough memory within the notification area's process to store the button info we want
                IntPtr toolbarmemoryptr = NativeMethods.VirtualAllocEx(naprocesshandle, (IntPtr)null, (uint)Marshal.SizeOf(typeof(NativeMethods.TBBUTTON)), NativeMethods.AllocationType.Commit, NativeMethods.MemoryProtection.ReadWrite);

                if(toolbarmemoryptr == IntPtr.Zero)
                    return null;

                try {
                    // loop through the toolbar's buttons until we find our notify icon
                    for(int j = 0; !found && j < buttoncount; j++) {
                        int bytesread = -1;

                        // ask the notification area to give us information about the current button
                        NativeMethods.SendMessage(natoolbarhandle, NativeMethods.TB_GETBUTTON, new IntPtr(j), toolbarmemoryptr);

                        // retrieve that information from the notification area's process
                        NativeMethods.TBBUTTON buttoninfo = new NativeMethods.TBBUTTON();
                        NativeMethods.ReadProcessMemory(naprocesshandle, toolbarmemoryptr, out buttoninfo, Marshal.SizeOf(buttoninfo), out bytesread);

                        if(bytesread != Marshal.SizeOf(buttoninfo) || buttoninfo.dwData == IntPtr.Zero)
                            return null;

                        // the dwData field contains a pointer to information about the notify icon:
                        // the handle of the notify icon (an 4/8 bytes) and the id of the notify icon (4 bytes)
                        IntPtr niinfopointer = buttoninfo.dwData;

                        // read the notify icon handle
                        IntPtr nihandlenew;
                        NativeMethods.ReadProcessMemory(naprocesshandle, niinfopointer, out nihandlenew, Marshal.SizeOf(typeof(IntPtr)), out bytesread);

                        if(bytesread != Marshal.SizeOf(typeof(IntPtr)))
                            return null;

                        // read the notify icon id
                        uint niidnew;
                        NativeMethods.ReadProcessMemory(naprocesshandle, (IntPtr)((int)niinfopointer + (int)Marshal.SizeOf(typeof(IntPtr))), out niidnew, Marshal.SizeOf(typeof(uint)), out bytesread);

                        if(bytesread != Marshal.SizeOf(typeof(uint)))
                            return null;

                        // if we've found a match
                        if(nihandlenew == niidentifier.hWnd && niidnew == niidentifier.uID) {
                            // check if the button is hidden: if it is, return the rectangle of the 'show hidden icons' button
                            if((byte)(buttoninfo.fsState & NativeMethods.TBSTATE_HIDDEN) != 0) {
                                if(returnIfHidden)
                                    nirect = NotifyAreaUtility.GetButtonRectangle();
                                else
                                    return null;
                            }
                            else {
                                NativeMethods.RECT result = new NativeMethods.RECT();

                                // get the relative rectangle of the toolbar button (notify icon)
                                NativeMethods.SendMessage(natoolbarhandle, NativeMethods.TB_GETITEMRECT, new IntPtr(j), toolbarmemoryptr);

                                NativeMethods.ReadProcessMemory(naprocesshandle, toolbarmemoryptr, out result, Marshal.SizeOf(result), out bytesread);

                                if(bytesread != Marshal.SizeOf(result))
                                    return null;

                                // find where the rectangle lies in relation to the screen
                                NativeMethods.MapWindowPoints(natoolbarhandle, (IntPtr)null, ref result, 2);

                                nirect = result;
                            }

                            found = true;
                        }
                    }
                }
                finally {
                    // free memory within process
                    NativeMethods.VirtualFreeEx(naprocesshandle, toolbarmemoryptr, 0, NativeMethods.FreeType.Release);

                    // close handle to process
                    NativeMethods.CloseHandle(naprocesshandle);
                }
            }

            return nirect;
        }


        public void GetLocationPrepare() {
            // Get a screenshot of the notification area...
            Rectangle notifyAreaRect = NotifyAreaUtility.GetRectangle();
            Size notifyAreaSize = notifyAreaRect.Size;
            using(Bitmap notifyAreaBitmap = GetNotifyAreaScreenshot()) {
                // Something gone wrong? Give up.
                if(notifyAreaBitmap == null)
                    return;

                // Determine a good spot...
                if(notifyAreaSize.Width > notifyAreaSize.Height)
                    nearColor = notifyAreaBitmap.GetPixel(notifyAreaSize.Width - 3, notifyAreaSize.Height / 2);
                else
                    nearColor = notifyAreaBitmap.GetPixel(notifyAreaSize.Width / 2, notifyAreaSize.Height - 3);

                // And now we have our base colour!
                nearColorSet = true;
            }
        }

        public Point? GetLocation2(int accuracy) {
            // Got something fullscreen running? Of course we can't find our icon!
            if(SysInfo.ForegroundWindowIsFullScreen)
                return null;

            // The accuracy can't be below 0!
            if(accuracy < 0)
                throw new ArgumentOutOfRangeException("accuracy", "The accuracy value provided can't be negative!");

            // The notification area
            var notifyAreaRect = NotifyAreaUtility.GetRectangle();
            if(notifyAreaRect.IsEmpty)
                return null;

            // Back up the NotifyIcon's icon so we can reset it later on
            var notifyIconIcon = _notifyIcon.Icon;

            // Have we got a colour we could base the find pixel off?
            if(!nearColorSet)
                GetLocationPrepare();

            // Blah
            var colMatchIndexes = new List<int>();
            Point last = new Point(-1, -1);
            int hits = 0;
            int hitsMax = accuracy + 1;

            // Our wonderful loop
            for(int attempt = 0; attempt < 5 && hits < hitsMax; attempt++) {
                // Set the notify icon thingy to a random colour
                Random random = new Random();
                int rgbRange = 32;
                Color col;
                if(nearColorSet)
                    col = Color.FromArgb(
                        SafeColourVal(nearColor.R + random.Next(rgbRange) - 8),
                        SafeColourVal(nearColor.G + random.Next(rgbRange) - 8),
                        SafeColourVal(nearColor.B + random.Next(rgbRange) - 8));
                else
                    col = Color.FromArgb(
                        SafeColourVal(255 - random.Next(rgbRange)),
                        SafeColourVal(255 - random.Next(rgbRange)),
                        SafeColourVal(255 - random.Next(rgbRange)));

                // Set the find colour...
                SetFindColour(col);

                // Take a screenshot...
                Color[] taskbarPixels;
                using(Bitmap notifyAreaBitmap = GetNotifyAreaScreenshot()) {
                    // If something goes wrong, let's just assume we don't know where we should be
                    if(notifyAreaBitmap == null)
                        return null;

                    // We can reset the NotifyIcon now, and then...
                    _notifyIcon.Icon = notifyIconIcon;

                    // Grab the pixels of the taskbar using my very own Pfz-derived bitmap to pixel array awesomeness
                    taskbarPixels = BitmapToPixelArray(notifyAreaBitmap);
                }

                // Get every occurence of our lovely colour next to something the same...
                bool colMatched = false;
                int colMatchIndex = -1;
                int colAttempt = 0; // this determines whether we -1 any of the RGB
                while(true) {
                    Color col2 = Color.FromArgb(0, 0, 0);
                    //int colModAmount = colAttempt < 8 ? -1 : 1;
                    int colMod1 = (colAttempt % 8) < 4 ? 0 : -1;
                    int colMod2 = (colAttempt % 8) < 4 ? -1 : 0;

                    switch(colAttempt % 4) {
                        case 0:
                            col2 = Color.FromArgb(SafeColourVal(col.R + colMod1), SafeColourVal(col.G + colMod1), SafeColourVal(col.B + colMod1));
                            break;
                        case 1:
                            col2 = Color.FromArgb(SafeColourVal(col.R + colMod1), SafeColourVal(col.G + colMod1), SafeColourVal(col.B + colMod2));
                            break;
                        case 2:
                            col2 = Color.FromArgb(SafeColourVal(col.R + colMod1), SafeColourVal(col.G + colMod2), SafeColourVal(col.B + colMod1));
                            break;
                        case 3:
                            col2 = Color.FromArgb(SafeColourVal(col.R + colMod1), SafeColourVal(col.G + colMod2), SafeColourVal(col.B + colMod2));
                            break;
                    }

                    colAttempt++;

                    colMatchIndex = Array.FindIndex<Color>(taskbarPixels, colMatchIndex + 1, (Color c) => { return c == col2; });

                    if(colMatchIndex == -1) {
                        if(colAttempt < 8)
                            continue;
                        else
                            break;
                    }
                    else {
                        if(taskbarPixels[colMatchIndex + 1] == col2) {
                            colMatched = true;
                            break;
                        }
                    }
                }

                if(colMatched) {
                    hits++;
                    last.X = colMatchIndex % notifyAreaRect.Width;
                    last.Y = colMatchIndex / notifyAreaRect.Width; // Integer rounding is always downwards
                }
                else {
                    hits = 0;
                    last.X = -1;
                    last.Y = -1;
                }
            }

            // Don't forget, our current values are relative to the notification area and are at the bottom right of the icon!
            Point location = new Point(last.X, last.Y);
            if(location != new Point(-1, -1))
                return new Point(notifyAreaRect.X + (last.X - 16), notifyAreaRect.Y + (last.Y - 14));
            else
                return null;
        }

        private static Bitmap GetNotifyAreaScreenshot() {
            Rectangle notifyAreaRect = NotifyAreaUtility.GetRectangle();
            Bitmap notifyAreaBitmap = new Bitmap(notifyAreaRect.Width, notifyAreaRect.Height);
            using(Graphics notifyAreaGraphics = Graphics.FromImage(notifyAreaBitmap)) {
                try {
                    notifyAreaGraphics.CopyFromScreen(notifyAreaRect.X, notifyAreaRect.Y, 0, 0, notifyAreaRect.Size);
                }
                catch(System.ComponentModel.Win32Exception) {
                    return null;
                }
            }
            return notifyAreaBitmap;
        }

        private void SetFindColour(Color col) {
            // Grab the notification icon
            Bitmap iconBitmap = _notifyIcon.Icon.ToBitmap();

            // Draw on it
            Graphics iconGraphics = Graphics.FromImage(iconBitmap);
            iconGraphics.DrawRectangle(new Pen(col, 1), 12, 14, 3, 2);
            _notifyIcon.Icon = Icon.FromHandle(iconBitmap.GetHicon());
            iconGraphics.Dispose();
        }

        private static int SafeColourVal(int val) {
            return Math.Min(255, Math.Max(0, val) + 0);
        }

        /// <summary>
        /// Converts a System.Drawing.Bitmap to an array of System.Drawing.Colors.
        /// Based on code by Paulo Zemek written under The Code Project Open License
        /// http://www.codeproject.com/KB/graphics/ManagedBitmaps.aspx
        /// </summary>
        /// <param name="bitmap">The bitmap to convert.</param>
        /// <returns>An array of System.Drawing.Colors</returns>
        private static Color[] BitmapToPixelArray(Bitmap bitmap) {
            Bitmap fOriginalSystemBitmap = bitmap;
            Color[] cols = new Color[fOriginalSystemBitmap.Size.Width * fOriginalSystemBitmap.Size.Height];

            System.Drawing.Imaging.BitmapData sourceData = null;

            // The below structure of try/finally runs a block of code, guaranting that:
            // The allocation block will not be aborted.
            // The finally block will be called, independent if the allocation block was
            // run.
            // The code block is the only one that could be aborted.
            try {
                try { }
                finally {
                    sourceData = fOriginalSystemBitmap.LockBits(
                        new Rectangle(new Point(), fOriginalSystemBitmap.Size),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                }

                var size = fOriginalSystemBitmap.Size;
                unsafe {
                    byte* sourceScanlineBytes = (byte*)sourceData.Scan0;
                    for(int y = 0; y < size.Height; y++) {
                        int* sourceScanline = (int*)sourceScanlineBytes;

                        for(int x = 0; x < size.Width; x++) {
                            int color = sourceScanline[x];
                            int index = x % size.Width + y * size.Width;
                            cols[index] = Color.FromArgb((color >> 16) & 0xFF, (color >> 8) & 0xFF, color & 0xFF);
                        }

                        sourceScanlineBytes += sourceData.Stride;
                    }
                }
            }
            finally {
                if(sourceData != null)
                    fOriginalSystemBitmap.UnlockBits(sourceData);
            }

            return cols;
        }
    }
}

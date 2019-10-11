using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace presentation.desktopApp.helper {
    public static class Extensions {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short VkKeyScan(char character);

        public static Keys ToKeyCode(this char character) {
            short vkey = VkKeyScan(character);
            var retval = (Keys)(vkey & 0xff);
            int modifiers = vkey >> 8;
            if((modifiers & 1) != 0) retval |= Keys.Shift;
            if((modifiers & 2) != 0) retval |= Keys.Control;
            if((modifiers & 4) != 0) retval |= Keys.Alt;
            return retval;
        }

        public static int ToInt(this object obj) {
            if(obj != null) {
                var isValidNumber = int.TryParse(obj.ToString(), out int result);
                if(isValidNumber)
                    return result;
            }
            return 0;
        }
    }
}
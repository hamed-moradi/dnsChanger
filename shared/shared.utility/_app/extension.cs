using System;
using System.Collections.Generic;
using System.Text;

namespace assets.utility {
    public static class Extension {
        public static byte ToTypeNumber(this object obj) {
            if(obj is null)
                return 0;
            switch(obj.ToString().ToLower()) {
                case "preferred":
                    return 1;
                case "alternate":
                    return 2;
                default:
                    return 0;
            }
        }
    }
}

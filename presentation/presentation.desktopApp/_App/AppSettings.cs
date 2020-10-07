using System;
using System.Collections.Generic;
using System.Configuration;

namespace Presentation.DesktopApp {
    public class AppSettings {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}
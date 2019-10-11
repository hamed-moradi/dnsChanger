using System;
using System.Collections.Generic;
using System.Configuration;

namespace domain.office._app {
    public class AppSettings {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}
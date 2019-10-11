using System;
using System.Net;
using System.Reflection;

namespace shared.model.dashboard_models {
    public interface IBase_DashboardModel {
    }
    public class Base_DashboardModel: IBase_DashboardModel {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int? TotalPages { get; set; }
        public string Version { get { return Assembly.GetEntryAssembly().GetName().Version.ToString(); } }
    }
}

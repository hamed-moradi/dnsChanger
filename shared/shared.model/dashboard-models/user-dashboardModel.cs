using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shared.model.dashboard_models {
    public class User_DashboardModel: IBase_DashboardModel {
        public int? CustomerId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Avatar { get; set; }
        public string CreatedAt { get; set; }
        public byte? Status { get; set; }
    }
}

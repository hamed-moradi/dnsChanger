using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shared.model.dashboard_models {
    public class Admin_DashboardModel: IBase_DashboardModel {
        public int? Id { get; set; }
        public int? RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public byte? Gender { get; set; }
        public string Avatar { get; set; }
        public string CreatedAt { get; set; }
        public byte? Status { get; set; }
    }
}

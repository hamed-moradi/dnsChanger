using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shared.model.viewModels {
    public class User_ViewModel: Base_Paging_ViewModel {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Fullname { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
    }
    public class User_SignUp_ViewModel: IBase_ViewModel {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Fullname { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public string Token { get; set; }
        public List<UserProperty_ViewModel> Properties { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shared.model.viewModels {
    public class Customer_ViewModel: IBase_ViewModel {
        public string NationalCode { get; set; }
        public string BirthDate { get; set; }
    }
    public class Customer_GetById_ViewModel: IBase_ViewModel {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Avatar { get; set; }
    }
}

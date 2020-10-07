using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shared.model.bindingModels {
    public class Customer_SignUp_BindingModel: Header_BindingModel {
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
    }
    public class Customer_GetById_BindingModel: FullHeader_BindingModel {
        public int Id { get; set; }
    }
}

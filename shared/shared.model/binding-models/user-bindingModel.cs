using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shared.model.bindingModels {
    public class User_SignUp_BindingModel: Header_BindingModel {
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
    }
    public class ChangeMe_BindingModel: FullHeader_BindingModel {
        public string CellPhone { get; set; }
    }
    public class User_Get_BindingModel: Paging_BindingModel {
        public string Tilte { get; set; }
        public long? FromDate { get; set; }
        public long? ToDate { get; set; }
    }
    public class User_TwoFactorAuthentication_BindingModel: FullHeader_BindingModel {
        public int VerificationCode { get; set; }
        public string Password { get; set; }
    }
    public class User_Verify_BindingModel: FullHeader_BindingModel {
        public string @CellPhone { get; set; }
        public string @Email { get; set; }
        public int VerificationCode { get; set; }
    }
    public class User_SignIn_BindingModel: FullHeader_BindingModel {
        public string Password { get; set; }
        public string VerificationCode { get; set; }
    }
    public class User_Update_BindingModel: FullHeader_BindingModel {
        public IFormFile Avatar { get; set; }
        public string Nickname { get; set; }
        public long? BirthDate { get; set; }
    }
    public class User_DisableMe_BindingModel: FullHeader_BindingModel {
        public string Description { get; set; }
    }
}
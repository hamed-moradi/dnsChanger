using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace shared.model.bindingModels {
    public class Business_GetByLocation_BindingModel: Header_BindingModel {
        public string Title { get; set; }
        public string Categories { get; set; } // Comma seprated CategoryId's list
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? Radius { get; set; } // In meters
    }
    public class Business_GetPaging_BindingModel: Paging_BindingModel{
        public string Title { get; set; }
        public string BusinessCode { get; set; }
        public string Categories { get; set; } // Comma seprated CategoryId's list
    }
    public class Business_New_BindingModel: FullHeader_BindingModel {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string BusinessCode { get; set; }
        public string ThumbImage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Business_Edit_BindingModel: Business_New_BindingModel {
        public int Id { get; set; }
    }
}

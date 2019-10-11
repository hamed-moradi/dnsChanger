using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace shared.model.bindingModels {
    public class Product_Get_BindingModel: FullHeader_BindingModel {
        public int Id { get; set; }
    }
    public class Product_GetByLocation_BindingModel: Header_BindingModel {
        public string Title { get; set; }
        public string Categories { get; set; } // Comma seprated CategoryId's list
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? Radius { get; set; } // In meters
    }
    public class Product_GetPaging_BindingModel: Paging_BindingModel{
        public int BusinessId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; } // Comma seprated TagId's list
    }
    public class Product_New_BindingModel: FullHeader_BindingModel {
        public int BusinessId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public List<Image_BindingModel> Images { get; set; }
    }
    public class Product_Edit_BindingModel: FullHeader_BindingModel {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public List<Image_BindingModel> Images { get; set; }
    }
}

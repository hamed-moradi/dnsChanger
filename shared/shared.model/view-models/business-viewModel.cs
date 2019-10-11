using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shared.model.viewModels {
    public class Business_ViewModel: IBase_ViewModel {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string BusinessCode { get; set; }
        public string ThumbImage { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //public SqlGeography Point { get; set; }
    }
}

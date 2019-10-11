using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shared.model.viewModels {
    public class Image_ViewModel: IBase_ViewModel {
        public int? ScaleId { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
    }
}

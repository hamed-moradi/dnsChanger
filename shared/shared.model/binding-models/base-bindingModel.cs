using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace shared.model.bindingModels {
    public interface IBase_BindingModel { }
    public class Header_BindingModel: IBase_BindingModel {
        public string TimeZone { get; set; } = "UTC";
        public string Language { get; set; } = "en-US";
    }
    public class FullHeader_BindingModel: Header_BindingModel {
        public string Token { get; set; }
        public string DeviceId { get; set; }
    }
    public class Paging_BindingModel: FullHeader_BindingModel {
        public string OrderBy { get; set; } = "Id";
        public string Order { get; set; } = "DESC";
        public int? PageIndex { get; set; } = 0;
        public int? PageSize { get; set; } = 10;
        public int Skip { get { return (PageIndex * PageSize).Value; } }
        public int Take { get { return PageSize.Value; } }
    }
}

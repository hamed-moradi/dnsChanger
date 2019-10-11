using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace shared.model.bindingModels {
    public class Comment_GetPaging_BindingModel: Paging_BindingModel {
        public int EntityId { get; set; }
        public string Entity { get; set; }
        public string Keyword { get; set; }
    }
    public class Comment_New_BindingModel: FullHeader_BindingModel {
        public int? ParentId { get; set; }
        public int EntityId { get; set; }
        public string Entity { get; set; }
        public string Body { get; set; }
    }

    public class Comment_Edit_BindingModel: Comment_New_BindingModel {
        public int Id { get; set; }
        public string Body { get; set; }
    }
}

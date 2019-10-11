using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shared.model.viewModels {
    public class Comment_ViewModel: IBase_ViewModel {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public long EntityId { get; set; }
        public string Entity { get; set; }
        public string Body { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shared.model.bindingModels {
    public class SendMessageQueue_GetPaging_BindingModel: Paging_BindingModel {
        public byte? TypeId { get; set; }
        public byte? CategoryId { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string Gateway { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public byte? Status { get; set; }
    }
}

using System.Collections.Generic;

namespace shared.model.generalModels {
    public class DropDownItemModel {
        public int id { get; set; }
        public int? parentId { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string data_link { get; set; }
        public string text { get { return name; } }
    }
    public class DropDownModel {
        public int TotalCount { get; set; }
        public List<DropDownItemModel> Items { get; set; }
    }
}

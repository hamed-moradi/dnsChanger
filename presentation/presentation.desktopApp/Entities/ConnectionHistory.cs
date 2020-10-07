using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DesktopApp.Entities {
    [Table("ConnectionHistory")]
    public partial class ConnectionHistory {
        [Key]
        public int Id { get; set; }
        public string ConnectedIP { get; set; }
        public string PreferredDNSIP { get; set; }
        public string AlternateDNSIP { get; set; }
        public int? Duration { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

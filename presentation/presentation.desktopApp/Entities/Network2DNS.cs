using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DesktopApp.Entities {
    [Table("Network2DNS")]
    public partial class Network2DNS {
        [Key]
        public int Id { get; set; }
        public int NetworkId { get; set; }
        public int DNSAddressId { get; set; }
        public byte Priority { get; set; }
    }

    public partial class Network2DNS {
        [ForeignKey("NetworkId")]
        public NetworkAdapter NetworkAdapter { get; set; }
        [ForeignKey("DNSAddressId")]
        public DNSAddress DNSAddress { get; set; }
    }
}

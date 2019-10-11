using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.office.entities {
    [Table("Network2DNS")]
    public class Network2DNS {
        [Key]
        public int Id { get; set; }
        public int NetworkId { get; set; }
        public int DNSId { get; set; }
        public byte Priority { get; set; }

        [ForeignKey("NetworkId")]
        public NetworkAdapter NetworkConnection { get; set; }
        [ForeignKey("DNSId")]
        public DNSAddress DNSAddress { get; set; }
    }
}

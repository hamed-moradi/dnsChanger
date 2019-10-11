using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.office.entities {
    [Table("DNSAddress")]
    public class DNSAddress {
        [Key]
        public int Id { get; set; }
        public string IP { get; set; }
        public byte Type { get; set; }
        public byte Priority { get; set; }
    }
}

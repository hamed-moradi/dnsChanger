using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.office.entities {
    [Table("NetworkAdapter")]
    public class NetworkAdapter {
        [Key]
        public int Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid NetworkId { get; set; }
        public byte Priority { get; set; }
        [NotMapped]
        public bool IsConnected { get; set; }
    }
}

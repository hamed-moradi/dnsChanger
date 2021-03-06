﻿using Microsoft.WindowsAPICodePack.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DesktopApp.Entities {
    [Table("NetworkAdapter")]
    public partial class NetworkAdapter {
        [Key]
        public int Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AdapterId { get; set; }
        public byte Priority { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public partial class NetworkAdapter {
        [NotMapped]
        public bool IsConnected { get; set; }
        [NotMapped]
        public string DnsAddress { get; set; }
        [NotMapped]
        public NetworkConnectionCollection ConnectionCollection { get; set; }
    }
}

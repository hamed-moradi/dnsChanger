﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.office.entities {
    [Table("AppConfiguration")]
    public class AppConfiguration {
        [Key]
        public int Id { get; set; }
        public string Language { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
   public class DivarSimCity:IDivarSimCity
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public Guid SimcardGuid { get; set; }
        public Guid CityGuid { get; set; }
       
    }
}

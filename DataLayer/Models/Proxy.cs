using System;
using System.ComponentModel.DataAnnotations;
using DataLayer.Enums;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
    public class Proxy : IProxy
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        [MaxLength(150)]
        public string Server { get; set; }
        public int Port { get; set; }
        [MaxLength(250)]
        public string Secret { get; set; }
        [MaxLength(150)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        public ProxyType Type { get; set; }
    }
}

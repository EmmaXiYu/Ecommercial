using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Order
    {

        [Key]
        [Column(Order = 0)]
        [Required]
        public String userId { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [Required]
        public String billAmount { get; set; }
        [Key]
        [Column(Order = 1)]
        [Timestamp]
        public byte[] OrderedTime { get; set; }

    }
}
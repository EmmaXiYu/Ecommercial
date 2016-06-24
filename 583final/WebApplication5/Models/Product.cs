using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication5.Models
{
    public class Product
    {



        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public String UnitPrice { get; set; }
        [Required]
        public String Category { get; set; }
        [Required]
        public String MembershipVisibleCategory { get; set; }
        [Required]
        public int InventoryNumbers { get; set; }


        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
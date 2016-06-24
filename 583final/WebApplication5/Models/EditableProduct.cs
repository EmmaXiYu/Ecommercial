using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class EditableProduct
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

         public Boolean IsEditable { get; set; }
    }
}
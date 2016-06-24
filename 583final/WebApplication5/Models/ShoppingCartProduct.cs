using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class ShoppingCartProduct
    {
        
        public String userId { get; set; }

     
        public String productId { get; set; }
       
        public int numbers { get; set; }

       
        
        public String Name { get; set; }
        
        public String Description { get; set; }
        
        public String UnitPrice { get; set; }
        
        public String Category { get; set; }
        [Required]
        public int InventoryNumbers { get; set; }
        public String MembershipVisibleCategory { get; set; }
      public String inventoryNum { get; set; }

    }
}
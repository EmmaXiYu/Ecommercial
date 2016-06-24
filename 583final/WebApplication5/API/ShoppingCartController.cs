

using Microsoft.AspNet.Identity;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebApplication5.Models;
namespace WebApplication5.API
{
    [Authorize]
    public class ShoppingCartController : ApiController
    {

        // POST api/<controller>
        public HttpResponseMessage Post(String productId, String currentUserId)
        {

            try
            {


                using (ApplicationContext context = new ApplicationContext())

                {

                    Product p = context.Products.Find(Int32.Parse(productId));
                    ShoppingCartItem shoppingCartItem = context.ShoppingCart.Find(currentUserId, productId);
                    if (shoppingCartItem == null)
                    {
                        ShoppingCartItem newShoppingCartItem = new ShoppingCartItem();
                        newShoppingCartItem.userId = currentUserId;
                        newShoppingCartItem.productId = productId;
                        newShoppingCartItem.numbers = 1;
                        newShoppingCartItem.InventoryNumbers = p.InventoryNumbers;

                        context.ShoppingCart.Add(newShoppingCartItem);
                        context.SaveChanges();
                        HttpContext.Current.Cache.Remove("CartList");
                    }
                    else if (shoppingCartItem != null)
                    {
                        shoppingCartItem.numbers += 1;


                        context.SaveChanges();
                        HttpContext.Current.Cache.Remove("CartList");
                    }

                }

                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "ShoppingCart Updated." });
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Error Occurred. Scary Details:" + e.Message });
            }

        }


        // POST api/<controller>
        public HttpResponseMessage Post(String productId, String currentUserId, String operation)
        {

            try
            {


                using (ApplicationContext context = new ApplicationContext())

                {
                    Product p = context.Products.Find(Int32.Parse(productId));

                    ShoppingCartItem shoppingCartItem = context.ShoppingCart.Find(currentUserId, productId);

                    if (shoppingCartItem != null)
                    {
                        if (operation == "plus"&& shoppingCartItem.numbers<shoppingCartItem.InventoryNumbers)
                        {
                            shoppingCartItem.numbers += 1;
                        }
                        else if (operation == "minus" && shoppingCartItem.numbers >0)
                        {
                            shoppingCartItem.numbers -= 1;
                        }


                        context.SaveChanges();
                        HttpContext.Current.Cache.Remove("CartList");
                    }

                }

                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "ShoppingCart Updated." });
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Error Occurred. Scary Details:" + e.Message });
            }

        }


        public List<ShoppingCartProduct> Get(String userId)
        {
            List<ShoppingCartProduct> list = new List<ShoppingCartProduct>();
            ShoppingCartProduct p1 =null;
            if (HttpContext.Current.Cache["CartList"] != null)
                return (List<ShoppingCartProduct>)HttpContext.Current.Cache["CartList"];

            using (ApplicationContext context = new ApplicationContext())
            {
               
                List<ShoppingCartProduct> shoppingCart = null;
                shoppingCart = context.ShoppingCart.Select(t => new ShoppingCartProduct
                {
                    productId = t.productId,
                    numbers = t.numbers,
                    userId = t.userId

                }).Where(t => t.userId.Equals(userId)).ToList();
                if (shoppingCart != null&&shoppingCart.Count!=0)
                {
                    for (int i = 0; i < shoppingCart.Count; i++)
                    {
                        p1 = shoppingCart.ElementAt(i);
                        var pr = context.Products.Find(Int32.Parse(p1.productId));


                        p1.Description = pr.Description;
                        p1.productId = pr.Id.ToString();
                        p1.Name = pr.Name;
                        p1.Category = pr.Category;
                        p1.UnitPrice = pr.UnitPrice;
                        p1.MembershipVisibleCategory = pr.MembershipVisibleCategory;
                        p1.InventoryNumbers = pr.InventoryNumbers;

                        list.Add(p1);
                    }
                   

                }





            }
            HttpContext.Current.Cache["CartList"] = list;
            return list;

        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(String productId, String currentUserId)
        {

            try
            {


                using (ApplicationContext context = new ApplicationContext())

                {


                    ShoppingCartItem shoppingCartItem = context.ShoppingCart.Find(currentUserId, productId);

                    context.ShoppingCart.Remove(shoppingCartItem);

                    context.SaveChanges();
                    HttpContext.Current.Cache.Remove("CarttList");
                }



                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "ShoppingCart item deleted." });
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Error Occurred. Scary Details:" + e.Message });
            }
        }
    }
}

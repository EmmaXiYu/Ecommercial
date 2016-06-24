using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication5.Models;
using System.Web;
namespace WebApplication5.API
{
    public class OrderController : ApiController
    {

        // POST api/<controller>
        public HttpResponseMessage Post(String OrderUserId, String billAmount, byte[] orderedTime, List<ShoppingCartProduct> OrderItems)
        {

            try
            {


                using (ApplicationContext context = new ApplicationContext())

                {
                    

                    Order order = context.Orders.Find(OrderUserId, orderedTime);
                    if (order == null)
                    {
                        Order orderNew = new Order();
                        orderNew.userId = OrderUserId;
                        orderNew.OrderedTime = orderedTime;
                        orderNew.billAmount = billAmount;
                        context.Orders.Add(orderNew);
                        context.SaveChanges();

                    }
                    for (int i = 0; i < OrderItems.Count(); i++)
                    {
                        OrderItem orderitem = new OrderItem();
                        orderitem.ProductId = OrderItems.ElementAt(i).productId;
                        orderitem.userId = OrderUserId;
                        orderitem.OrderedTime = orderedTime;
                        orderitem.Quantity = OrderItems.ElementAt(i).numbers.ToString();
                        context.OrderItems.Add(orderitem);
                        context.SaveChanges();

                    }

                }

                return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "ShoppingCart Updated." });
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Error Occurred. Scary Details:" + e.Message });
            }

        }
    }
}

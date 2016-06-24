using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Helper;

namespace WebApplication5.Models
{
    [OutputCache(CacheProfile = "StaticUser")]
    public class MembershipController : Controller
    {
     
        
        public ActionResult ViewMembership()
        {
            return View();
        }
         public ActionResult InitiateCreditTransaction()
        {
            //Assign the values for the properties we need to pass to the service
            String AppId = ConfigurationManager.AppSettings["CreditAppId"];
            String SharedKey = ConfigurationManager.AppSettings["CreditAppSharedKey"];
            String AppTransId ="";
            String MemberType = Request.Form["membershipOptions"];
            String AppTransAmount = Request.Form["paymentName"];
            if (MemberType == "SilverMember")
            { AppTransAmount = "23.23";
                AppTransId = "1";
            }

            else if (MemberType == "GoldenMember")
            { AppTransAmount = "34.73";
                AppTransId = "2";
            }
            // Hash the values so the server can verify the values are original
            String hash = HttpUtility.UrlEncode(CreditAuthorizationClient.GenerateClientRequestHash(SharedKey, AppId, AppTransId, AppTransAmount));

            //Create the URL and  concatenate  the Query String values
            String url = "http://ectweb2.cs.depaul.edu/ECTCreditGateway/Authorize.aspx";
            url = url + "?AppId=" + AppId;
            url = url + "&TransId=" + AppTransId;
            url = url + "&AppTransAmount=" + AppTransAmount;
            url = url + "&AppHash=" + hash;

            return Redirect(url);
        }

        public ActionResult ProcessCreditResponse(String TransId, String TransAmount, String StatusCode, String AppHash)
        {
            String AppId = ConfigurationManager.AppSettings["CreditAppId"];
            String SharedKey = ConfigurationManager.AppSettings["CreditAppSharedKey"];
            String MembershipType = "";
            if (TransId == "2")
            { MembershipType = "GoldenMember"; }
            else if (TransId == "1")
            {
                MembershipType = "SilverMember";
            }
            if (Helper.CreditAuthorizationClient.VerifyServerResponseHash(AppHash, SharedKey, AppId, TransId, TransAmount, StatusCode))
            {
                switch (StatusCode)
                {
                    case ("A"):
                        {
                            if (TransId == "3")
                            {
                                ViewBag.TransactionStatus = "Transaction Approved! Your Order will be processed ASAP.";
                                using (ApplicationContext context = new ApplicationContext())
                                {
                                    String currentUserId = ((ClaimsIdentity)this.User.Identity).Claims
                            .Where(c => c.Type == ClaimTypes.NameIdentifier)
                            .Select(c => c.Value).ToList().LastOrDefault();
                                    List<ShoppingCartProduct> shoppingCart = null;
                                    shoppingCart = context.ShoppingCart.Select(t => new ShoppingCartProduct
                                    {
                                        productId = t.productId,
                                        numbers = t.numbers,
                                        userId = t.userId

                                    }).Where(t => t.userId.Equals(currentUserId)).ToList();
                                    if (shoppingCart != null && shoppingCart.Count != 0)
                                    {
                                        for (int i = 0; i < shoppingCart.Count; i++)
                                        {
                                            ShoppingCartItem shoppingCartItem = context.ShoppingCart.Find(currentUserId, shoppingCart.ElementAt(i).productId);
                                            context.ShoppingCart.Remove(shoppingCartItem);

                                            context.SaveChanges();
                                        }


                                    }





                                }
                            }
                            else
                            {
                                ViewBag.TransactionStatus = "Transaction Approved! Thanks for upgrade your membership. We will provide the best service for you.";


                                using (ApplicationContext context = new ApplicationContext())
                                {



                                    var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                                    var Identity = new ClaimsIdentity(User.Identity);
                                    String role = ((ClaimsIdentity)this.User.Identity).Claims
                                                                  .Where(c => c.Type == ClaimTypes.Role)
                                                                  .Select(c => c.Value).First();
                                    // Identity.RemoveClaim(new Claim(ClaimTypes.Role, role));

                                    Identity.AddClaim(new Claim(ClaimTypes.Role, MembershipType));
                                    AuthenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(Identity), new AuthenticationProperties { IsPersistent = true });
                                }
                            }

                            break;
                        }
                    case ("D"): { ViewBag.TransactionStatus = "Transaction Denied! Sorry,try again or contanct customer service"; break; }
                    case ("C"): { ViewBag.TransactionStatus = "Transaction Cancelled!"; break; }

                }
            }
            else
            {
                ViewBag.TransactionStatus = "Hash Verification failed... something went wrong.";
            }

            return View();
        }
    }

    }


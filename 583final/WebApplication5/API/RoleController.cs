using Microsoft.AspNet.Identity;
using System;
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
{ [Authorize]
    public class RoleController : ApiController
    {
        // GET api/<controller>
        public List<CurrentUser> Get()
        {
            CurrentUser cUser = new CurrentUser();
            List<CurrentUser> list = new List<CurrentUser>();
            String userId = "";
            String role = null;


            // var currentUser = Membership.GetUser(User.Identity.Name);
            //String userID = currentUser.ProviderUserKey.ToString();
            using (ApplicationContext context = new ApplicationContext())
            {

            }
            userId= ((ClaimsIdentity)this.User.Identity).Claims
                            .Where(c => c.Type == ClaimTypes.NameIdentifier)
                            .Select(c => c.Value).ToList().LastOrDefault();



            role = ((ClaimsIdentity)this.User.Identity).Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value).ToList().LastOrDefault();


            cUser.Role = role;
            cUser.UserId = userId;
            list.Add(cUser);
            return list;
            //   user.Id = context.Users.Find(this.User.Identity.Name).



        }


    }
}

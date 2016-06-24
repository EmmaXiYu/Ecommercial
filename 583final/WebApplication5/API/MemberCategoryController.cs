using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication5.Models;

namespace WebApplication5.API
{
    public class MemberCategoryController : ApiController
    {

        // GET api/<controller>
        public List<String> Get()
        {
            using (ApplicationContext context = new ApplicationContext())
            {


                var p = context.Products.Select(t =>
                 t.MembershipVisibleCategory).ToList();
                List<String> returnList = new List<String>();
                for (int i = 0; i < p.Count; i++) // Loop with for.
                {
                    if (!returnList.Contains(p[i]))
                    { returnList.Add(p[i]); }
                }

                returnList.Add("All");



                return returnList;
            }
        }
    }
}

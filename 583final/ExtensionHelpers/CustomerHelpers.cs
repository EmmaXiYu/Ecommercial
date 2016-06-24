using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
namespace ExtensionHelpers
{
    public static class CustomerHelpers
    {


        //Based on http://stackoverflow.com/questions/5052752/adding-your-own-htmlhelper-in-asp-net-mvc-3
        //Simplified version
        public static MvcHtmlString MemberVisibilityCategoryDropDownList(this HtmlHelper html)
        {
            List<SelectListItem> CategoryList = new List<SelectListItem>
                    {
                        new SelectListItem() { Value= "Regular",Text= "Regular"},
                         new SelectListItem() {Value="Silver",Text="Silver"},
                         new SelectListItem() {Value="Golden",Text="Golden"},

                    };

            return html.DropDownList("MemberVisibilityCategory", CategoryList);
        }



    }


}


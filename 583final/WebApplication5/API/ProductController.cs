using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using WebApplication5.Models;

namespace WebApplication5.API
{
    [Authorize]
    public class ProductController : ApiController
    {

        static string a = "All";
        static string b = "All";
        // GET api/<controller>
        public List<EditableProduct> Get()
        {
            if (HttpContext.Current.Cache["EditableProductList"] != null)
                return (List<EditableProduct>)HttpContext.Current.Cache["EditableProductList"];
            using (ApplicationContext context = new ApplicationContext())
            {
                var isAdmin = this.User.IsInRole("Admin");


                var p = context.Products.Select(t => new EditableProduct
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    UnitPrice = t.UnitPrice,
                    Category = t.Category,
                    Timestamp = t.Timestamp,
                    InventoryNumbers = t.InventoryNumbers,
                    MembershipVisibleCategory=t.MembershipVisibleCategory,
                    IsEditable = isAdmin
                }).ToList();
                HttpContext.Current.Cache["EditableProductList"] = p;

                return p;
            }
        }


        // GET api/<controller>
        public List<EditableProduct> Get(String category)
        {
            if (HttpContext.Current.Cache["ProductListWithCategory"+category ] != null)
                return (List<EditableProduct>)HttpContext.Current.Cache["ProductListWithCategory" +category];
            using (ApplicationContext context = new ApplicationContext())
            {
                var isAdmin = this.User.IsInRole("Admin");

                var p = context.Products.Select(t => new EditableProduct
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    UnitPrice = t.UnitPrice,
                    Category = t.Category,
                    Timestamp = t.Timestamp,
                    InventoryNumbers = t.InventoryNumbers,
                    MembershipVisibleCategory = t.MembershipVisibleCategory,
                    IsEditable = isAdmin

                }).Where(t => t.Category.Equals(category)).ToList();
                HttpContext.Current.Cache["ProductListWithCategory" +category] = p;

                return p;
            }
        }

       
        // GET api/<controller>
        public List<EditableProduct> Get(String category, String MemberCategory)
        {
            List<EditableProduct> p=null;
            a = category;
            b = MemberCategory;
            var isad = this.User.IsInRole("Admin");
            if (isad)
            {
                if (HttpContext.Current.Cache["managereditlist"+a+b] != null)
                    return (List<EditableProduct>)HttpContext.Current.Cache["managereditlist"+a+b];



            }
            else {

                if (HttpContext.Current.Cache["ProductListWithCategoryMember" + category + MemberCategory] != null)
                    return (List<EditableProduct>)HttpContext.Current.Cache["ProductListWithCategoryMember" + category + MemberCategory];
            }
            using (ApplicationContext context = new ApplicationContext())
            {
                var isAdmin = this.User.IsInRole("Admin");
                if (category.Equals("All") &&( MemberCategory.Equals("All")|| MemberCategory.Equals("Admin")))
                {


                    p = context.Products.Select(t => new EditableProduct
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        UnitPrice = t.UnitPrice,
                        Category = t.Category,
                        Timestamp = t.Timestamp,
                        InventoryNumbers = t.InventoryNumbers,
                        MembershipVisibleCategory = t.MembershipVisibleCategory,
                        IsEditable = isAdmin
                    }).ToList();


                }
                else if (category.Equals("All") && !MemberCategory.Equals("All")&& !MemberCategory.Equals("Admin"))
                {
                    if (MemberCategory.Equals("GoldenMember"))
                    {
                        p = context.Products.Select(t => new EditableProduct
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Description = t.Description,
                            UnitPrice = t.UnitPrice,
                            Category = t.Category,
                            Timestamp = t.Timestamp,
                            InventoryNumbers = t.InventoryNumbers,
                            MembershipVisibleCategory = t.MembershipVisibleCategory,
                             IsEditable = isAdmin
                        }).ToList();
                    }
                    else if (MemberCategory.Equals("SilverMember"))
                    {
                        p = context.Products.Select(t => new EditableProduct
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Description = t.Description,
                            UnitPrice = t.UnitPrice,
                            Category = t.Category,
                            Timestamp = t.Timestamp,
                            InventoryNumbers = t.InventoryNumbers,
                            MembershipVisibleCategory = t.MembershipVisibleCategory,
                             IsEditable = isAdmin
                        }).Where(t => !t.MembershipVisibleCategory.Equals("GoldenMember")).ToList();
                    }
                    else if (MemberCategory.Equals("RegularMember"))
                    {
                        p = context.Products.Select(t => new EditableProduct
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Description = t.Description,
                            UnitPrice = t.UnitPrice,
                            Category = t.Category,
                            Timestamp = t.Timestamp,
                            InventoryNumbers = t.InventoryNumbers,
                            MembershipVisibleCategory = t.MembershipVisibleCategory,
                             IsEditable = isAdmin
                        }).Where(t => !t.MembershipVisibleCategory.Equals("GoldenMember") && !t.MembershipVisibleCategory.Equals("SilverMember")).ToList();
                    }
                    
                }
                else if (!category.Equals("All") && ( MemberCategory.Equals("All")|| MemberCategory.Equals("Admin")))
                {


                     p = context.Products.Select(t => new EditableProduct
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        UnitPrice = t.UnitPrice,
                        Category = t.Category,
                        Timestamp = t.Timestamp,
                         InventoryNumbers = t.InventoryNumbers,
                         MembershipVisibleCategory = t.MembershipVisibleCategory,
                         IsEditable = isAdmin
                     }).Where(t => t.Category.Equals(category)).ToList();


                    
                }
               
                else if (!category.Equals("All") && !MemberCategory.Equals("All")&& !MemberCategory.Equals("Admin"))
                {
                    if (MemberCategory.Equals("GoldenMember"))
                    {
                        p = context.Products.Select(t => new EditableProduct
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Description = t.Description,
                            UnitPrice = t.UnitPrice,
                            Category = t.Category,
                            Timestamp = t.Timestamp,
                            InventoryNumbers = t.InventoryNumbers,
                            MembershipVisibleCategory = t.MembershipVisibleCategory,
                             IsEditable = isAdmin
                        }).Where(t => t.Category.Equals(category)).ToList();
                    }
                    else if (MemberCategory.Equals("SilverMember"))
                    {
                        p = context.Products.Select(t => new EditableProduct
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Description = t.Description,
                            UnitPrice = t.UnitPrice,
                            Category = t.Category,
                            Timestamp = t.Timestamp,
                            InventoryNumbers = t.InventoryNumbers,
                            MembershipVisibleCategory = t.MembershipVisibleCategory,
                             IsEditable = isAdmin
                        }).Where(t => !t.MembershipVisibleCategory.Equals("GoldenMember")&& t.Category.Equals(category)).ToList();
                    }
                    else if (MemberCategory.Equals("RegularMember"))
                    {
                        p = context.Products.Select(t => new EditableProduct
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Description = t.Description,
                            UnitPrice = t.UnitPrice,
                            Category = t.Category,
                            Timestamp = t.Timestamp,
                            InventoryNumbers = t.InventoryNumbers,
                            MembershipVisibleCategory = t.MembershipVisibleCategory,
                             IsEditable = isAdmin
                        }).Where(t => !t.MembershipVisibleCategory.Equals("GoldenMember") && !t.MembershipVisibleCategory.Equals("SilverMember") && t.Category.Equals(category)).ToList();
                    }




                }

            }
            HttpContext.Current.Cache["ProductListWithCategoryMember"+category+MemberCategory] = p;
            HttpContext.Current.Cache["managereditlist"+a+b] = p;
            return p;
        }

        // GET api/<controller>/5
        public EditableProduct Get(int id)
        {
            if (HttpContext.Current.Cache["ProductList"+id] != null)
                return (EditableProduct)HttpContext.Current.Cache["ProductList" +id];
            using (ApplicationContext context = new ApplicationContext())
            {
                var isAdmin = this.User.IsInRole("Admin");
                var pr = context.Products.Find(id);
                EditableProduct p = new EditableProduct
                {
                    Description = pr.Description,
                    Id = pr.Id,
                    Name = pr.Name,
                    Category = pr.Category,
                    UnitPrice = pr.UnitPrice,
                    Timestamp = pr.Timestamp,
                    InventoryNumbers = pr.InventoryNumbers,
                    MembershipVisibleCategory = pr.MembershipVisibleCategory,
                    IsEditable = isAdmin
                };
                HttpContext.Current.Cache["ProductList"+id ] = p;
                return p;
            }
        }


        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Product pProdut)
        {
          
                try
                {


                    using (ApplicationContext context = new ApplicationContext())
                    {
                        if (pProdut.Id == 0)
                        {

                            context.Products.Add(pProdut);
                            context.SaveChanges();
                        HttpContext.Current.Cache.Remove("EditableProductList");
                        HttpContext.Current.Cache.Remove("ProductListWithCategory"+pProdut.Category);
                        HttpContext.Current.Cache.Remove("ProductListWithCategoryMember"+pProdut.Category+pProdut.MembershipVisibleCategory);
                        HttpContext.Current.Cache.Remove("ProductList" + pProdut.Id);
                        HttpContext.Current.Cache.Remove("managereditlist"+a+b);
                        HttpContext.Current.Cache.Remove("managereditlist" + "All" + "All");
                        HttpContext.Current.Cache.Remove("managereditlist" + a + "All");
                        HttpContext.Current.Cache.Remove("managereditlist" + a + "SilverMember");
                        HttpContext.Current.Cache.Remove("managereditlist" + a + "GoldenMember");
                        HttpContext.Current.Cache.Remove("managereditlist" + a + "RegularMember");
                        HttpContext.Current.Cache.Remove("managereditlist" + "Ancient Wine" + b);

                        HttpContext.Current.Cache.Remove("managereditlist" + "All" + b);
                        HttpContext.Current.Cache.Remove("managereditlist" + "Regular Wine" + b);
                        HttpContext.Current.Cache.Remove("managereditlist" + "Special Collection Wine" + b);

                        return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Product Added." });
                        }
                        else
                        {
                            var p = context.Products.Find(pProdut.Id);

                            p.Name = pProdut.Name;
                            p.Description = pProdut.Description;
                            p.UnitPrice = pProdut.UnitPrice;
                            p.Category = pProdut.Category;
                            p.Timestamp = pProdut.Timestamp;
                        p.InventoryNumbers = pProdut.InventoryNumbers;
                            p.MembershipVisibleCategory = pProdut.MembershipVisibleCategory;
                            context.SaveChanges();
                        HttpContext.Current.Cache.Remove("EditableProductList");
                        HttpContext.Current.Cache.Remove("ProductListWithCategory" +p.Category);
                        HttpContext.Current.Cache.Remove("ProductListWithCategoryMember"+p.Category +p.MembershipVisibleCategory);
                        HttpContext.Current.Cache.Remove("ProductList" + pProdut.Id);
                        HttpContext.Current.Cache.Remove("managereditlist"+a+b);
                        HttpContext.Current.Cache.Remove("managereditlist" + "All" + "All");
                        HttpContext.Current.Cache.Remove("managereditlist" + a + "All");
                        HttpContext.Current.Cache.Remove("managereditlist" + a + "SilverMember");
                        HttpContext.Current.Cache.Remove("managereditlist" + a + "GoldenMember");
                        HttpContext.Current.Cache.Remove("managereditlist" + a + "RegularMember");
                        HttpContext.Current.Cache.Remove("managereditlist" + "Ancient Wine" + b);
                       
                        HttpContext.Current.Cache.Remove("managereditlist" + "All" + b);
                        HttpContext.Current.Cache.Remove("managereditlist" + "Regular Wine" + b);
                        HttpContext.Current.Cache.Remove("managereditlist" + "Special Collection Wine" + b);
                     

                        return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Produt Updated." });
                        }

                    }


                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Error Occurred. Scary Details:" + e.Message });
                } 
            
        } 



        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {

                using (ApplicationContext context = new ApplicationContext())
                {
                    Product p = context.Products.Find(id);
                   ;
                    context.Products.Remove(p);
                    HttpContext.Current.Cache.Remove("EditableProductList");
                    HttpContext.Current.Cache.Remove("ProductListWithCategory" +p.Category);
                    HttpContext.Current.Cache.Remove("ProductListWithCategoryMember"+p.Category+p.MembershipVisibleCategory );
                    HttpContext.Current.Cache.Remove("ProductList" +id);
                    HttpContext.Current.Cache.Remove("managereditlist"+a+b);
                    HttpContext.Current.Cache.Remove("managereditlist" + "All" + "All");
                    HttpContext.Current.Cache.Remove("managereditlist" + a + "All");
                    HttpContext.Current.Cache.Remove("managereditlist" + a + "SilverMember");
                    HttpContext.Current.Cache.Remove("managereditlist" + a + "GoldenMember");
                    HttpContext.Current.Cache.Remove("managereditlist" + a + "RegularMember");
                    HttpContext.Current.Cache.Remove("managereditlist" + "Ancient Wine" + b);

                    HttpContext.Current.Cache.Remove("managereditlist" + "All" + b);
                    HttpContext.Current.Cache.Remove("managereditlist" + "Regular Wine" + b);
                    HttpContext.Current.Cache.Remove("managereditlist" + "Special Collection Wine" + b);

                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, new { success = true, message = "Product Deleted." });


                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = false, message = "Error Occurred. Scary Details:" + e.Message });
            }
        }
    }
}


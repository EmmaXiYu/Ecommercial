﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    [OutputCache(CacheProfile = "StaticUser")]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewOrder()
        {
            return View();
        }
    }
}
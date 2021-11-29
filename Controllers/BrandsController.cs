﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Models;

namespace EFDbFirstApproachExample.Controllers
{
    public class BrandsController : Controller
    {
        CompanyDbContext db = new CompanyDbContext();
        public ActionResult Index()
        {
            List<Brand> brands = db.Brands.ToList();
            return View(brands);
        }
    }
}
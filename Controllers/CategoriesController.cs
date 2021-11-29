using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Models;

namespace EFDbFirstApproachExample.Controllers
{
    public class CategoriesController : Controller
    {
        CompanyDbContext db = new CompanyDbContext();
        public ActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();

            return View(categories);
        }
    }
}
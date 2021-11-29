using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Models;

namespace EFDbFirstApproachExample.Controllers
{
    public class ProductsController : Controller
    {
        CompanyDbContext db = new CompanyDbContext();
        public ActionResult Index(string search = "", string sortColumn = "ProductName", string iconClass="fa-sort-desc", int pageNo = 1)
        {
            List<Product> products = db.Products.Where(product => product.ProductName.Contains(search)).ToList();
            ViewBag.search = search;

            // Sorting //
            ViewBag.SortColumn = sortColumn;
            ViewBag.IconClass = iconClass;

            if (ViewBag.SortColumn == "ProductID")
            {
                if (ViewBag.IconClass == "fa-sort-desc")
                    products = products.OrderBy(p => p.ProductID).ToList();
                else
                    products = products.OrderByDescending(p => p.ProductID).ToList();
            }
            else if (ViewBag.SortColumn == "ProductName")
            {
                if (ViewBag.IconClass == "fa-sort-desc")
                    products = products.OrderBy(p => p.ProductName).ToList();
                else
                    products = products.OrderByDescending(p => p.ProductName).ToList();
            }
            else if (ViewBag.SortColumn == "Price")
            {
                if (ViewBag.IconClass == "fa-sort-desc")
                    products = products.OrderBy(p => p.Price).ToList();
                else
                    products = products.OrderByDescending(p => p.Price).ToList();
            }
            else if (ViewBag.SortColumn == "DOP")
            {
                if (ViewBag.IconClass == "fa-sort-desc")
                    products = products.OrderBy(p => p.DOP).ToList();
                else
                    products = products.OrderByDescending(p => p.DOP).ToList();
            }
            else if (ViewBag.SortColumn == "AvailabilityStatus")
            {
                if (ViewBag.IconClass == "fa-sort-desc")
                    products = products.OrderBy(p => p.AvailabilityStatus).ToList();
                else
                    products = products.OrderByDescending(p => p.AvailabilityStatus).ToList();
            }
            else if (ViewBag.SortColumn == "CategoryID")
            {
                if (ViewBag.IconClass == "fa-sort-desc")
                    products = products.OrderBy(p => p.CategoryID).ToList();
                else
                    products = products.OrderByDescending(p => p.CategoryID).ToList();
            }
            else if (ViewBag.SortColumn == "BrandID")
            {
                if (ViewBag.IconClass == "fa-sort-desc")
                    products = products.OrderBy(p => p.BrandID).ToList();
                else
                    products = products.OrderByDescending(p => p.BrandID).ToList();
            }
            else if (ViewBag.SortColumn == "Active")
            {
                if (ViewBag.IconClass == "fa-sort-desc")
                    products = products.OrderBy(p => p.Active).ToList();
                else
                    products = products.OrderByDescending(p => p.Active).ToList();
            }

            // Paging //
            int noRecordsPerPage = 5;
            int noPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(noRecordsPerPage)));
            int noRecordsToSkip = (pageNo - 1) * noRecordsPerPage;
            ViewBag.PageNo = pageNo;
            ViewBag.NoPages = noPages;

            products = products.Skip(noRecordsToSkip).Take(noRecordsPerPage).ToList();
            
            return View(products);
            // Using SQL Procedure with EF
            /*
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter ("@BrandID", 2), // Create parameter @BrandID with value of 2 (bigint) = "Apple"
            };
            List<Product> products = db.Database.SqlQuery<Product>("EXEC getProductsByBrandID @BrandID", sqlParameters).ToList();
            */
        }

        public ActionResult Details(long id)
        {
            Product product = db.Products.Where(p => p.ProductID == id).FirstOrDefault();

            return View(product);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include="ProductID, ProductName, Price, DOP, AvailabilityStatus, CategoryID, BrandID, Active, Img")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    product.Img = base64String;
                }
            
                db.Products.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = db.Categories.ToList();
                ViewBag.Brands = db.Brands.ToList();
                return View();
            }
        }

        public ActionResult Edit(long id)
        {
            Product product = db.Products.Where(p => p.ProductID == id).FirstOrDefault();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            Product oldProduct = db.Products.Where(p => p.ProductID == product.ProductID).FirstOrDefault();
            oldProduct.ProductName = product.ProductName;
            oldProduct.Price = product.Price;
            oldProduct.DOP = product.DOP;
            oldProduct.AvailabilityStatus = product.AvailabilityStatus;
            oldProduct.CategoryID = product.CategoryID;
            oldProduct.BrandID = product.BrandID;
            
            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imgBytes = new Byte[file.ContentLength];
                file.InputStream.Read(imgBytes, 0, file.ContentLength);
                var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                oldProduct.Img = base64String;
            }

            oldProduct.Active = product.Active;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            Product product = db.Products.Where(p => p.ProductID == id).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(long id, Product _prod)
        {
            Product product = db.Products.Where(p => p.ProductID == id).FirstOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
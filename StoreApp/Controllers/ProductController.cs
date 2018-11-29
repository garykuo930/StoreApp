using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreApp.Models;
using StoreApp.Models.ViewModel;
using PagedList;
using PagedList.Mvc;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        private storeEntities db = new storeEntities();

        // GET: Product
        public ActionResult Index(int? page,string sortBy)
        {
            ViewBag.sortProductName = string.IsNullOrEmpty(sortBy) ? "ProductNameDesc" : "";
            ViewBag.sortUnitPrice = sortBy == "UnitPrice" ? "UnitPriceDesc" : "UnitPrice";
            var products = db.Products.AsQueryable();
            switch (sortBy)
            {
                case "ProductNameDesc":
                    products = products.OrderByDescending(p => p.ModelNumber);
                    break;
                case "UnitPrice":
                    products = products.OrderBy(p => p.UnitCost);
                    break;
                case "UnitPriceDesc":
                    products = products.OrderByDescending(p => p.UnitCost);
                    break;
                default:
                    products = products.OrderBy(p => p.ModelNumber);
                    break;
            }

            return View(products.ToList().ToPagedList(page ?? 1,3));
        }

        public ActionResult GetImage(string fileName)
        {
            string imgPath = @"~\ProductImages\" + fileName;
            return File(imgPath, "image/jpeg");
        }

        public ActionResult GetImageByte(int? id = 1)
        {
            Products p = db.Products.Find(id);
            byte[] img = p.BytesImage;
            return File(img, "image/jpeg");
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Cats = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Products products, HttpPostedFileBase ProductImage)
        {
            string imgPath = Request.PhysicalApplicationPath + @"ProductImages\" + ProductImage.FileName;
            ProductImage.SaveAs(imgPath);

            Stream stream = ProductImage.InputStream;
            byte[] img = new byte[stream.Length];
            stream.Read(img, 0, img.Length);

            products.ProductImage = ProductImage.FileName;
            products.BytesImage = img;
            db.Products.Add(products);
            db.SaveChanges();
            ViewBag.Cats = db.Categories.ToList();
            return View();
        }

        public ActionResult Catelog(int id=1)
        {
            DisplayVM vm = new DisplayVM();

            vm.Categories = db.Categories.ToList();
            vm.Products = db.Products.Where(p => p.CategoryID == id);

            return View(vm);
        }

        public ActionResult AddtoCart(int? id)
        {
            ShoppingCart product = new ShoppingCart();

            if(Session["CartID"] == null)
            {
                Session["CartID"] = Guid.NewGuid();
                product.CartID = Session["CartID"].ToString();
                product.ProductID = (int)id;
                product.Quantity = 1;
                product.DateCreated = DateTime.Now;
            }
            else
            {
                product.CartID = Session["CartID"].ToString();
                product.ProductID = (int)id;
                product.Quantity = 1;
                product.DateCreated = DateTime.Now;
            }

            db.ShoppingCart.Add(product);
            db.SaveChanges();
            return RedirectToAction("Catelog");
        }
    }
}
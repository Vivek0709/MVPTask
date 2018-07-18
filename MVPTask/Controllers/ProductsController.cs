using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVPTask.Models;

namespace MVPTask.Controllers
{
    public class ProductsController : Controller
    {
        private MVPDBEntities1 db = new MVPDBEntities1();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProducts()
        {
            //var products = db.Products;

            //var allProduts = new List<ProductViewModel>();

            //foreach(var product in products)
            //{
            //    allProduts.Add(new ProductViewModel
            //    {
            //        Id = product.Id,
            //        Name = product.Name,
            //        Price = product.Price
            //    });
            //}
            //return Json(allProduts, JsonRequestBehavior.AllowGet);

            return Json(db.Products.Select(product => new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price
                };

                db.Products.Add(product);
                db.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Invalid model");
        }

        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var product = db.Products.Find(id);

            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Find(viewModel.Id);
                product.Name = viewModel.Name;
                product.Price = viewModel.Price;
                db.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Invalid model");
        }

        [HttpGet]//Get Delete Product
        public ActionResult CanDeleteProduct(int id)
        {
            var product = db.Products.Find(id);

            //var viewModel = new ProductViewModel
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Price = product.Price
            //};
            return Json(!product?.ProductSolds.Any(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]//Delete
        public ActionResult DeleteProduct(ProductViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var product = db.Products.Find(viewModel.Id);
                product.Name = viewModel.Name;
                product.Price = viewModel.Price;
                db.Products.Remove(product);
                db.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Invalid Model");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
    public class SalesController : Controller
    {
        private MVPDBEntities1 db = new MVPDBEntities1();

        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        ////Get All Sales
        public ActionResult GetSales()
        {
            var sales = db.ProductSolds;
            var allsales = new List<SalesViewModel>();

            foreach (var sal in sales)
            {
                allsales.Add(new SalesViewModel
                {
                    Id = sal.Id,
                    CustomerId = sal.CustomerId,
                    CustomerName = sal.Customer.Name,
                    ProductId = sal.ProductId,
                    ProductName = sal.Product.Name,
                    StoreId = sal.StoreId,
                    StoreName = sal.Store.Name,

                    DateSold = sal.DateSold.Value
                });
            }
            return Json(allsales, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]//Add Sales
        public ActionResult AddSales(SalesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var sales = new ProductSold
                {
                    CustomerId = viewModel.CustomerId,
                    ProductId = viewModel.ProductId,
                    StoreId = viewModel.StoreId,
                    DateSold = viewModel.DateSold
                };
                db.ProductSolds.Add(sales);
                db.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Invalid Model");
        }

        //Get Edit Store
        [HttpGet]
        public ActionResult EditSales(int id)
        {
            var sales = db.ProductSolds.Find(id);

            var viewModel = new SalesViewModel
            {
                Id = sales.Id,
                CustomerId = sales.CustomerId,
                CustomerName = sales.Customer.Name,
                ProductId = sales.ProductId,
                ProductName = sales.Product.Name,
                StoreId = sales.StoreId,
                StoreName = sales.Store.Name,

                DateSold = sales.DateSold.Value
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        //Post Edit Store
        [HttpPost]
        public ActionResult AddEditSales(SalesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var sales = db.ProductSolds.Find(viewModel.Id);
                sales.CustomerId = viewModel.CustomerId;
                sales.ProductId = viewModel.ProductId;
                sales.StoreId = viewModel.StoreId;
                sales.DateSold = viewModel.DateSold;
                db.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Invalid model");
        }

        [HttpGet]//Get Delete Sales
        public ActionResult DeleteSales(int id)
        {
            var sales = db.ProductSolds.Find(id);

            var viewModel = new SalesViewModel
            {
                Id = sales.Id,
                CustomerId = sales.CustomerId,
                CustomerName = sales.Customer.Name,
                ProductId = sales.ProductId,
                ProductName = sales.Product.Name,
                StoreId = sales.StoreId,
                StoreName = sales.Store.Name,

                DateSold = sales.DateSold.Value
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]//Sales
        public ActionResult DeleteSales(SalesViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var sales = db.ProductSolds.Find(viewModel.Id);
                sales.CustomerId = viewModel.CustomerId;
                sales.ProductId = viewModel.ProductId;
                sales.StoreId = viewModel.StoreId;
                sales.DateSold = viewModel.DateSold;

                db.ProductSolds.Remove(sales);
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

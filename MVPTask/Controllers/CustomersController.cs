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
    public class CustomersController : Controller
    {
        private MVPDBEntities1 db = new MVPDBEntities1();

        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }
        //Get All Cutomers
        public ActionResult GetCustomers()
        {
            var customer = db.Customers;
            var allcustomer = new List<CustomerViewModel>();

            foreach (var cust in customer)
            {
                allcustomer.Add(new CustomerViewModel
                {
                    Id = cust.Id,
                    Name = cust.Name,
                    Address = cust.Address
                });
            }
            return Json(allcustomer, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]//Add customer
        public ActionResult AddCustomer(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    Name = viewModel.Name,
                    Address = viewModel.Address
                };
                db.Customers.Add((customer));
                db.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Invalid Model");
        }

        [HttpGet]//Get edit Customer
        public ActionResult EditCustomer(int id)
        {
            var customer = db.Customers.Find(id);

            var viewmodel = new CustomerViewModel
            {

                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address
            };
            return Json(viewmodel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]//Add Edit Customer
        public ActionResult AddEditCustomer(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.Find(viewModel.Id);
                customer.Name = viewModel.Name;
                customer.Address= viewModel.Address;
                db.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Invalid model");
        }

        [HttpGet]
        public ActionResult CanDeleteCustomer(int Id)
        {
            var customer = db.Customers.Find(Id);

            return Json(!customer?.ProductSolds.Any(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost] //Delete Customer form database
        public ActionResult PostDeleteCustomer(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.Find(viewModel.Id);
                customer.Name = viewModel.Name;
                customer.Address = viewModel.Address;
                db.Customers.Remove(customer);
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

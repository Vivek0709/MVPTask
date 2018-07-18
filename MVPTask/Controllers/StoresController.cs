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
    public class StoresController : Controller
    {
        private MVPDBEntities1 db = new MVPDBEntities1();

        // GET: Stores
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost] //Add Stores
        public ActionResult AddStores(StoreViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var store = new Store
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Address = viewModel.Address
                };
                db.Stores.Add(store);
                db.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Invalid Model");
        }

        //Get All Stores
        public ActionResult GetAllStore()
        {
            var store = db.Stores;
            var allStore = new List<StoreViewModel>();

            foreach (var str in store )
            {
                allStore.Add(new StoreViewModel
                {
                    Id = str.Id,
                    Name = str.Name,
                    Address = str.Address
                });
            }

            return Json(allStore, JsonRequestBehavior.AllowGet);
        }
        //Get Edit Store
        [HttpGet]
        public ActionResult EditStore(int id)
        {
            var store = db.Stores.Find(id);

            var viewModel = new StoreViewModel
            {
                Id = store.Id,
                Name = store.Name,
                Address = store.Address
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
        //Post Edit Store
        [HttpPost]
        public ActionResult AddEditStore(StoreViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var store = db.Stores.Find(viewModel.Id);
                store.Name = viewModel.Name;
                store.Address = viewModel.Address;
                db.SaveChanges();

                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            throw new Exception("Invalid model");
        }

        [HttpGet]//Get Delete Store
        public ActionResult GetDeleteStore(int id)
        {
            var store = db.Stores.Find(id);

            //var viewModel = new StoreViewModel
            //{
            //    Id = store.Id,
            //    Name = store.Name,
            //    Address = store.Address
            //};
            return Json(!store?.ProductSolds.Any(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]//Post Delete
        public ActionResult PostDeleteStore(StoreViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var store = db.Stores.Find(viewModel.Id);
                store.Name = viewModel.Name;
                store.Address = viewModel.Address;
                db.Stores.Remove(store);
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

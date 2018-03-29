using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthwindMVC.Models;
using NorthwindMVC.ViewModels;

namespace NorthwindMVC.Controllers
{
    public class OrdersController : Controller
    {
        private NorthwindDataEntities db = new NorthwindDataEntities();

        // GET: Orders
        public ActionResult Index()
        {
            List<OrderViewModel> model = new List<OrderViewModel>();

            NorthwindDataEntities entities = new NorthwindDataEntities();

            try
            {
                List<Orders> orders = entities.Orders.OrderBy(Orders => Orders.OrderDate).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Orders order in orders)
                {
                    OrderViewModel view = new OrderViewModel();
                    view.OrderID = order.OrderID;         
                    view.OrderDate = order.OrderDate;
                    view.RequiredDate = order.RequiredDate;
                    view.ShippedDate = order.ShippedDate;
                    view.ShipVia = order.ShipVia;
                    view.Freight = order.Freight;
                    view.ShipName = order.ShipName;
                    view.ShipAddress = order.ShipAddress;
                    view.ShipCity = order.ShipCity;
                    view.ShipRegion = order.ShipRegion;
                    view.ShipPostalCode = order.ShipPostalCode;
                    view.ShipCountry = order.ShipCountry;

                    view.EmployeeID = order.EmployeeID;
                    view.LastName = order.Employees?.LastName;

                    view.CustomerID = order.CustomerID;
                    view.CompanyName = order.Customers?.CompanyName;

                    ViewBag.CompanyName = new SelectList((from c in db.Customers select new { CustomerID = c.CustomerID, CompanyName = c.CompanyName }), "CustomerID", "CompanyName", null);
                    ViewBag.LastName = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, LastName = e.LastName }), "EmployeeID", "LastName", null);
                    ViewBag.ShipVia = new SelectList((from o in db.Orders select new { OrderID = o.OrderID, ShipVia = o.ShipVia }), "OrderID ", "ShipVia", null);


                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//Index

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            OrderViewModel model = new OrderViewModel();

            NorthwindDataEntities entities = new NorthwindDataEntities();
            try
            {
                Orders order = entities.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta    
                OrderViewModel view = new OrderViewModel();
                view.OrderID = order.OrderID;       
                view.OrderDate = order.OrderDate;
                view.RequiredDate = order.RequiredDate;
                view.ShippedDate = order.ShippedDate;
                view.ShipVia = order.ShipVia;
                view.Freight = order.Freight;
                view.ShipName = order.ShipName;
                view.ShipAddress = order.ShipAddress;
                view.ShipCity = order.ShipCity;
                view.ShipRegion = order.ShipRegion;
                view.ShipPostalCode = order.ShipPostalCode;
                view.ShipCountry = order.ShipCountry;

                view.EmployeeID = order.EmployeeID;
                view.LastName = order.Employees?.LastName;

                view.CustomerID = order.CustomerID;
                view.CompanyName = order.Customers?.CompanyName;
        
                ViewBag.CompanyName = new SelectList((from c in db.Customers select new { CustomerID = c.CustomerID, CompanyName = c.CompanyName }), "CustomerID", "CompanyName", null);
                ViewBag.LastName = new SelectList((from e in db.Employees select new { EmployeeID= e.EmployeeID, LastName = e.LastName }), "EmployeeID", "LastName", null);
                ViewBag.ShipVia = new SelectList((from o in db.Orders select new { OrderID = o.OrderID, ShipVia = o.ShipVia }), "OrderID ", "ShipVia", null);

                model = view;
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }//details

        // GET: Orders/Create
        public ActionResult Create()
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();

            OrderViewModel model = new OrderViewModel();

            ViewBag.CompanyName = new SelectList((from c in db.Customers select new { CustomerID = c.CustomerID, CompanyName = c.CompanyName }), "CustomerID", "CompanyName", null);
            ViewBag.LastName = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, LastName = e.LastName }), "EmployeeID", "LastName", null);
            ViewBag.ShipVia = new SelectList((from o in db.Orders select new { OrderID = o.OrderID, ShipVia = o.ShipVia }), "OrderID ", "ShipVia", null);

            return View(model);
        }//create

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel model)
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();

            Orders view = new Orders();
            view.OrderDate = model.OrderDate;
            view.RequiredDate = model.RequiredDate;
            view.ShippedDate = model.ShippedDate;
            view.ShipVia = model.ShipVia;
            view.Freight =  model.Freight;
            view.ShipName = model.ShipName;
            view.ShipAddress = model.ShipAddress;
            view.ShipCity = model.ShipCity;
            view.ShipRegion = model.ShipRegion;
            view.ShipPostalCode = model.ShipPostalCode;
            view.ShipCountry = model.ShipCountry;

            view.EmployeeID = model.EmployeeID;
            //view.LastName = model.Employees?.LastName;

            view.CustomerID = model.CustomerID;
            //view.CompanyName = model.Customers?.CompanyName;

            ViewBag.CompanyName = new SelectList((from c in db.Customers select new { CustomerID = c.CustomerID, CompanyName = c.CompanyName }), "CustomerID", "CompanyName", null);
            ViewBag.LastName = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, LastName = e.LastName }), "EmployeeID", "LastName", null);
            ViewBag.ShipVia = new SelectList((from o in db.Orders select new { OrderID = o.OrderID, ShipVia = o.ShipVia }), "OrderID ", "ShipVia", null);

            db.Orders.Add(view);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//create

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            OrderViewModel view = new OrderViewModel();
            view.OrderID = order.OrderID;
            view.OrderDate = order.OrderDate;
            view.RequiredDate = order.RequiredDate;
            view.ShippedDate = order.ShippedDate;
            view.ShipVia = order.ShipVia;
            view.Freight = order.Freight;
            view.ShipName = order.ShipName;
            view.ShipAddress = order.ShipAddress;
            view.ShipCity = order.ShipCity;
            view.ShipRegion = order.ShipRegion;
            view.ShipPostalCode = order.ShipPostalCode;
            view.ShipCountry = order.ShipCountry;

            view.EmployeeID = order.EmployeeID;
            //view.LastName = model.Employees?.LastName;

            view.CustomerID = order.CustomerID;
            //view.CompanyName = model.Customers?.CompanyName;

            ViewBag.CompanyName = new SelectList((from c in db.Customers select new { CustomerID = c.CustomerID, CompanyName = c.CompanyName }), "CustomerID", "CompanyName", null);
            ViewBag.LastName = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, LastName = e.LastName }), "EmployeeID", "LastName", null);
            ViewBag.ShipVia = new SelectList((from o in db.Orders select new { OrderID = o.OrderID, ShipVia = o.ShipVia }), "OrderID ", "ShipVia", null);

            return View(view);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel model)
        {
            Orders view = new Orders();
            view.OrderDate = model.OrderDate;
            view.RequiredDate = model.RequiredDate;
            view.ShippedDate = model.ShippedDate;
            view.ShipVia = model.ShipVia;
            view.Freight = model.Freight;
            view.ShipName = model.ShipName;
            view.ShipAddress = model.ShipAddress;
            view.ShipCity = model.ShipCity;
            view.ShipRegion = model.ShipRegion;
            view.ShipPostalCode = model.ShipPostalCode;
            view.ShipCountry = model.ShipCountry;

            view.EmployeeID = model.EmployeeID;
            //view.LastName = model.Employees?.LastName;

            view.CustomerID = model.CustomerID;
            //view.CompanyName = model.Customers?.CompanyName;

            ViewBag.CompanyName = new SelectList((from c in db.Customers select new { CustomerID = c.CustomerID, CompanyName = c.CompanyName }), "CustomerID", "CompanyName", null);
            ViewBag.LastName = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, LastName = e.LastName }), "EmployeeID", "LastName", null);
            ViewBag.ShipVia = new SelectList((from o in db.Orders select new { OrderID = o.OrderID, ShipVia = o.ShipVia }), "OrderID ", "ShipVia", null);

            db.SaveChanges();
            return View("Index");
        }//edit

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            OrderViewModel view = new OrderViewModel();
            view.OrderID = order.OrderID;
        
            view.OrderDate = order.OrderDate;
            view.RequiredDate = order.RequiredDate;
            view.ShippedDate = order.ShippedDate;
            view.ShipVia = order.ShipVia;
            view.Freight = order.Freight;
            view.ShipName = order.ShipName;
            view.ShipAddress = order.ShipAddress;
            view.ShipCity = order.ShipCity;
            view.ShipRegion = order.ShipRegion;
            view.ShipPostalCode = order.ShipPostalCode;
            view.ShipCountry = order.ShipCountry;

            view.CustomerID = order.CustomerID;
            view.EmployeeID = order.EmployeeID;

            ViewBag.CompanyName = new SelectList((from c in db.Customers select new { CustomerID = c.CustomerID, CompanyName = c.CompanyName }), "CustomerID", "CompanyName", null);
            ViewBag.LastName = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, LastName = e.LastName }), "EmployeeID", "LastName", null);
            ViewBag.ShipVia = new SelectList((from o in db.Orders select new { OrderID = o.OrderID, ShipVia = o.ShipVia }), "OrderID ", "ShipVia", null);


            return View(view);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
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

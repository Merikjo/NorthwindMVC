﻿using System;
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
    public class CustomersController : Controller
    {
        private NorthwindDataEntities db = new NorthwindDataEntities();

        // GET: Customers
        public ActionResult Index()
        {
            List<CustomerViewModel> model = new List<CustomerViewModel>();

            NorthwindDataEntities entities = new NorthwindDataEntities();

            try
            {
                List<Customers> customers = entities.Customers.OrderBy(Customers => Customers.CompanyName).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Customers customer in customers)
                {
                    CustomerViewModel view = new CustomerViewModel();
                    view.CustomerID = customer.CustomerID;
                    view.CompanyName = customer.CompanyName;
                    view.ContactName = customer.ContactName;
                    view.ContactTitle = customer.ContactTitle;
                    view.Address = customer.Address;
                    view.City = customer.City;
                    view.Region = customer.Region;
                    view.PostalCode = customer.PostalCode;
                    view.Country = customer.Country;
                    view.Phone = customer.Phone;
                    view.Fax = customer.Fax;
            
                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        //Index
         //List<Customers> model = new List<Customers>();
         //try
         //{
         //    NorthwindDataEntities entities = new NorthwindDataEntities();
         //    model = entities.Customers.ToList();
         //    entities.Dispose();
         //}
         //catch (Exception ex)
         //{
         //    ViewBag.ErrorMessage = ex.GetType() + ": " + ex.Message;
         //}

        //return View(model);
    }


        public ActionResult GetOrders(string id)
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();

            List<Orders> orders = (from o in entities.Orders
                                   where o.CustomerID == id
                                   select o).ToList();


            entities.Dispose();

            List<SimpleOrderData> result = new List<SimpleOrderData>();
            foreach (Orders order in orders)
            {
                SimpleOrderData data = new SimpleOrderData();
                data.CustomerId = order.CustomerID;
                data.OrderId = order.OrderID;
                data.OrderDate = order.OrderDate.ToString();
                result.Add(data);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            CustomerViewModel model = new CustomerViewModel();

            NorthwindDataEntities entities = new NorthwindDataEntities();
            try
            {
               Customers customer = entities.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta    
                CustomerViewModel view = new CustomerViewModel();
                view.CustomerID = customer.CustomerID;
                view.CompanyName = customer.CompanyName;
                view.ContactName = customer.ContactName;
                view.ContactTitle = customer.ContactTitle;
                view.Address = customer.Address;
                view.City = customer.City;
                view.Region = customer.Region;
                view.PostalCode = customer.PostalCode;
                view.Country = customer.Country;
                view.Phone = customer.Phone;
                view.Fax = customer.Fax;

                //ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);

                model = view;
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }//details

        // GET: Customers/Create
        public ActionResult Create()
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();

            CustomerViewModel model = new CustomerViewModel();

            //ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);


            return View(model);
        }//create


        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();

            Customers view = new Customers();
            view.CompanyName = model.CompanyName;
            view.ContactName = model.ContactName;
            view.ContactTitle = model.ContactTitle;
            view.Address = model.Address;
            view.City = model.City;
            view.Region = model.Region;
            view.PostalCode = model.PostalCode;
            view.Country = model.Country;
            view.Phone = model.Phone;
            view.Fax = model.Fax;

            //ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);

            db.Customers.Add(view);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//create

        // GET: Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel view = new CustomerViewModel();
            view.CustomerID = customer.CustomerID;
            view.CompanyName = customer.CompanyName;
            view.ContactName = customer.ContactName;
            view.ContactTitle = customer.ContactTitle;
            view.Address = customer.Address;
            view.City = customer.City;
            view.Region = customer.Region;
            view.PostalCode = customer.PostalCode;
            view.Country = customer.Country;
            view.Phone = customer.Phone;
            view.Fax = customer.Fax;

            //ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);

            return View(view);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel model)
        {
            Customers view = new Customers();
            view.CompanyName = model.CompanyName;
            view.ContactName = model.ContactName;
            view.ContactTitle = model.ContactTitle;
            view.Address = model.Address;
            view.City = model.City;
            view.Region = model.Region;
            view.PostalCode = model.PostalCode;
            view.Country = model.Country;
            view.Phone = model.Phone;
            view.Fax = model.Fax;

            db.SaveChanges();

            return View("Index");
        }//edit

        // GET: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel view = new CustomerViewModel();
            view.CustomerID = customer.CustomerID;
            view.CompanyName = customer.CompanyName;
            view.ContactName = customer.ContactName;
            view.ContactTitle = customer.ContactTitle;
            view.Address = customer.Address;
            view.City = customer.City;
            view.Region = customer.Region;
            view.PostalCode = customer.PostalCode;
            view.Country = customer.Country;
            view.Phone = customer.Phone;
            view.Fax = customer.Fax;

            return View(view);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customers customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

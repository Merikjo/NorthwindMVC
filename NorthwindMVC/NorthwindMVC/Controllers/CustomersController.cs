using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
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
            //nimiavaruuden määrittäminen, lisää automaattisesti using-määreen tiedoston alkuun, jolloin voidaan hyödyntää entiteettimallia.
            NorthwindDataEntities entities = new NorthwindDataEntities();
            //haetaan lista asiakasolioista = tietokantakysely
            List<CustomerViewModel> model = new List<CustomerViewModel>();


            try
            {
                List<Customers> customer = entities.Customers.OrderBy(Customers => Customers.CustomerID).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Customers customers in customer)
                {
                    CustomerViewModel view = new CustomerViewModel();
                    view.CustomerID = customers.CustomerID;
                    view.CompanyName = customers.CompanyName;
                    view.ContactName = customers.ContactName;
                    view.ContactTitle = customers.ContactTitle;
                    view.Address = customers.Address;
                    view.City = customers.City;
                    view.Region = customers.Region;
                    view.PostalCode = customers.PostalCode;
                    view.Country = customers.Country;
                    view.Phone = customers.Phone;
                    view.Fax = customers.Fax;
            
                    model.Add(view); 
                }
            }
            finally
            {
                //Dispose metodi vapauttaa tietokantakyselyn, ei välttämätön, mutta hyvää ohjelmointitapaa
                entities.Dispose();
            }
            //malliolion välittäminen (=lista asiakkaita) näkymälle
            return View(model); //annetaan return lauseelle yksi parametri

            #region
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
            #endregion
        }

        CultureInfo fiFi = new CultureInfo("fi-FI");

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
            NorthwindDataEntities db = new NorthwindDataEntities();

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
            NorthwindDataEntities db = new NorthwindDataEntities();

            Customers view = new Customers();
            view.CustomerID = model.CustomerID;
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
            db.Customers.Add(view);

            //ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);

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
            Customers view = db.Customers.Find(model.CustomerID);
            view.CustomerID = model.CustomerID;
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


        // GET: CustomerDynamic
        public ActionResult IndexCustomer()
        {
            ViewBag.OmaTieto = "ABC123";

            NorthwindDataEntities entities = new NorthwindDataEntities();
            List<Customers> model = entities.Customers.ToList();
            entities.Dispose();

            return View(model);
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Index3()
        {
            return View();
        }

        public JsonResult GetList()
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();
            //List<Customer> model = entities.Customers.ToList();

            var model = (from c in entities.Customers
                         select new
                         {
                             CustomerID = c.CustomerID,
                             CompanyName = c.CompanyName,
                             Address = c.Address,
                             City = c.City
                         }).ToList();

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

            Response.Expires = -1;
            Response.CacheControl = "no-cache";

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSingleCustomer(string id)
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();
            var model = (from c in entities.Customers
                         where c.CustomerID == id
                         select new
                         {
                             CustomerID = c.CustomerID,
                             CompanyName = c.CompanyName,
                             Address = c.Address,
                             City = c.City
                         }).FirstOrDefault();

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update(Customers cust)
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();
            string id = cust.CustomerID;

            bool OK = false;

            // onko kyseessä muokkaus vai uuden lisääminen?
            if (id == "(uusi)")
            {
                // kyseessä on uuden asiakkaan lisääminen, kopioidaan kentät
                Customers dbItem = new Customers()
                {
                    CustomerID = cust.CompanyName.Substring(0, 5).Trim().ToUpper(), //luodaan CustomerID CompanyNamen 5:stä ensimmäisesta kirjaimesta isoilla kirjaimilla
                    CompanyName = cust.CompanyName,
                    Address = cust.Address,
                    City = cust.City
                };

                // tallennus tietokantaan
                entities.Customers.Add(dbItem);
                entities.SaveChanges();
                OK = true;
            }
            else
            {
                // muokkaus, haetaan id:n perusteella riviä tietokannasta
                Customers dbItem = (from c in entities.Customers
                                    where c.CustomerID == id
                                    select c).FirstOrDefault();
                if (dbItem != null)
                {
                    dbItem.CompanyName = cust.CompanyName;
                    dbItem.Address = cust.Address;
                    dbItem.City = cust.City;

                    // tallennus tietokantaan
                    entities.SaveChanges();
                    OK = true;
                }
            }
            entities.Dispose();

            return Json(OK, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCustomer(string id)
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();

            // etsitään id:n perusteella asiakasrivi kannasta
            bool OK = false;
            Customers dbItem = (from c in entities.Customers
                                where c.CustomerID == id
                                select c).FirstOrDefault();
            if (dbItem != null)
            {
                // tietokannasta poisto
                entities.Customers.Remove(dbItem);
                entities.SaveChanges();
                OK = true;
            }
            entities.Dispose();

            return Json(OK, JsonRequestBehavior.AllowGet);
        }
    }
}

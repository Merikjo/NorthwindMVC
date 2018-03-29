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
    public class EmployeesController : Controller
    {
        private NorthwindDataEntities db = new NorthwindDataEntities();

        // GET: Employees
        public ActionResult Index()
        {
            List<EmployeesViewModel> model = new List<EmployeesViewModel>();

            NorthwindDataEntities entities = new NorthwindDataEntities();

            try
            {
                List<Employees> employees = entities.Employees.OrderBy(Employees => Employees.LastName).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Employees employee in employees)
                {
                    EmployeesViewModel view = new EmployeesViewModel();
                    view.EmployeeID = employee.EmployeeID;
                    view.LastName = employee.LastName;
                    view.FirstName = employee.FirstName;
                    view.Title = employee.Title;
                    view.TitleOfCourtesy = employee.TitleOfCourtesy;
                    view.BirthDate = employee.BirthDate;
                    view.HireDate = employee.HireDate;
                    view.Address = employee.Address;
                    view.City = employee.City;
                    view.Region = employee.Region;
                    view.PostalCode = employee.PostalCode;
                    view.Country = employee.Country;
                    view.HomePhone = employee.HomePhone;
                    view.Extension = employee.Extension;
                    //view.Photo = employee.Photo;
                    view.Notes = employee.Notes;
                    view.ReportsTo = employee.ReportsTo;
                    //view.PhotoPath = employee.PhotoPath;

                    ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }//Index

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            EmployeesViewModel model = new EmployeesViewModel();

            NorthwindDataEntities entities = new NorthwindDataEntities();
            try
            {
                Employees employee = entities.Employees.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }

                // muodostetaan näkymämalli tietokannan rivien pohjalta    
                EmployeesViewModel view = new EmployeesViewModel();
                view.EmployeeID = employee.EmployeeID;
                view.LastName = employee.LastName;
                view.FirstName = employee.FirstName;
                view.Title = employee.Title;
                view.TitleOfCourtesy = employee.TitleOfCourtesy;
                view.BirthDate = employee.BirthDate;
                view.HireDate = employee.HireDate;
                view.Address = employee.Address;
                view.City = employee.City;
                view.Region = employee.Region;
                view.PostalCode = employee.PostalCode;
                view.Country = employee.Country;
                view.HomePhone = employee.HomePhone;
                view.Extension = employee.Extension;
                view.Photo = employee.Photo;
                view.Notes = employee.Notes;
                view.ReportsTo = employee.ReportsTo;
                view.PhotoPath = employee.PhotoPath;

                ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);

                model = view;
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }//details


        // GET: Employees/Create
        public ActionResult Create()
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();

            EmployeesViewModel model = new EmployeesViewModel();

            ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);


            return View(model);
        }//create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeesViewModel model)
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();

            Employees view = new Employees();
            view.LastName = model.LastName;
            view.FirstName = model.FirstName;
            view.Title = model.Title;
            view.TitleOfCourtesy = model.TitleOfCourtesy;
            view.BirthDate = model.BirthDate;
            view.HireDate = model.HireDate;
            view.Address = model.Address;
            view.City = model.City;
            view.Region = model.Region;
            view.PostalCode = model.PostalCode;
            view.Country = model.Country;
            view.HomePhone = model.HomePhone;
            view.Extension = model.Extension;
            //view.Photo = model.Photo;
            view.Notes = model.Notes;
            view.ReportsTo = model.ReportsTo;
            //view.PhotoPath = model.PhotoPath;

            ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);

            db.Employees.Add(view);

            try
            {
                db.SaveChanges();
            }

            catch (Exception ex)
            {
            }

            return RedirectToAction("Index");
        }//create

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            EmployeesViewModel view = new EmployeesViewModel();
            view.EmployeeID = employee.EmployeeID;
            view.LastName = employee.LastName;
            view.FirstName = employee.FirstName;
            view.Title = employee.Title;
            view.TitleOfCourtesy = employee.TitleOfCourtesy;
            view.BirthDate = employee.BirthDate;
            view.HireDate = employee.HireDate;
            view.Address = employee.Address;
            view.City = employee.City;
            view.Region = employee.Region;
            view.PostalCode = employee.PostalCode;
            view.Country = employee.Country;
            view.HomePhone = employee.HomePhone;
            view.Extension = employee.Extension;
            //view.Photo = employee.Photo;
            view.Notes = employee.Notes;
            view.ReportsTo = employee.ReportsTo;
            //view.PhotoPath = employee.PhotoPath;

            ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);

            return View(view);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeesViewModel model)
        {
            Employees view = new Employees();
            view.LastName = model.LastName;
            view.FirstName = model.FirstName;
            view.Title = model.Title;
            view.TitleOfCourtesy = model.TitleOfCourtesy;
            view.BirthDate = model.BirthDate;
            view.HireDate = model.HireDate;
            view.Address = model.Address;
            view.City = model.City;
            view.Region = model.Region;
            view.PostalCode = model.PostalCode;
            view.Country = model.Country;
            view.HomePhone = model.HomePhone;
            view.Extension = model.Extension;
            //view.Photo = model.Photo;
            view.Notes = model.Notes;
            view.ReportsTo = model.ReportsTo;
            //view.PhotoPath = model.PhotoPath;

            //if (cus.PinCodes == null)
            //{
            //    PinCodes pic = new PinCodes();
            //    pic.PinCode = model.PinCode;
            //    //usr.Password = "joku@joku.fi";
            //    pic.Customers = cus;

            //    db.PinCodes.Add(pic);
            //}
            //else
            //{
            //    PinCodes pic = cus.PinCodes.FirstOrDefault();
            //    if (pic != null)
            //    {
            //        pic.PinCode = model.PinCode;
            //    }
            //}

            ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);

            db.SaveChanges();
            return View("Index");
        }//edit

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            EmployeesViewModel view = new EmployeesViewModel();
            view.EmployeeID = employee.EmployeeID;
            view.LastName = employee.LastName;
            view.FirstName = employee.FirstName;
            view.Title = employee.Title;
            view.TitleOfCourtesy = employee.TitleOfCourtesy;
            view.BirthDate = employee.BirthDate;
            view.HireDate = employee.HireDate;
            view.Address = employee.Address;
            view.City = employee.City;
            view.Region = employee.Region;
            view.PostalCode = employee.PostalCode;
            view.Country = employee.Country;
            view.HomePhone = employee.HomePhone;
            view.Extension = employee.Extension;
            view.Photo = employee.Photo;
            view.Notes = employee.Notes;
            view.ReportsTo = employee.ReportsTo;
            view.PhotoPath = employee.PhotoPath;

            ViewBag.ReportsTo = new SelectList((from e in db.Employees select new { EmployeeID = e.EmployeeID, ReportsTo = e.ReportsTo }), "EmployeeID", "ReportsTo", null);
          
            return View(view);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

using NorthwindMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NorthwindMVC.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index()
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();
            try
            {
                List<Customers> model = entities.Customers.ToList();
                return View(model);
            }
            finally
            {
                entities.Dispose();
            }
        }

        public ActionResult GetOrderData(string id)
        {
            NorthwindDataEntities entities = new NorthwindDataEntities();
            try
            {
                List<Orders> orders = (from o in entities.Orders
                                       where o.CustomerID == id
                                       orderby o.OrderDate descending
                                       select o).ToList();


                StringBuilder html = new StringBuilder();
                html.AppendLine("<td colspan=\"5\">" +
                     "<table class=\"table table-striped\">");

                foreach (Orders order in orders)
                {
                    html.AppendLine("<tr><td>" +
                        order.OrderDate.Value.ToShortDateString() + "</td>" +
                        "<td>" + order.OrderID + "</td>" +
                        "<td>" + order.ShipCity + "</td>" +
                        "<td>" + order.RequiredDate.Value.ToShortDateString() + "</td></tr>");
                }
                   
                        html.AppendLine("</table></td>");
                
                //html.Append("Hello World");

                var jsonData = new { html = html.ToString() };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                entities.Dispose();
            }
        }
    }
}
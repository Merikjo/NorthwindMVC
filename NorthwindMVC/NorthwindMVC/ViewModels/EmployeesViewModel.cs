using NorthwindMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NorthwindMVC.ViewModels
{
    public class EmployeesViewModel
    {
        public EmployeesViewModel()
        {
            this.Employees = new HashSet<Employees>();
            this.Orders = new HashSet<Orders>();
            this.Territories = new HashSet<Territories>();
        }

        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public Nullable<int> ReportsTo { get; set; }
        public string PhotoPath { get; set; }


        public virtual ICollection<Employees> Employees { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }

        public virtual ICollection<Territories> Territories { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindMVC.Models
{
    public class SimpleOrderData
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string OrderDate { get; set; }
    }
}
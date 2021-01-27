using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace productassignment.Models
{
    public class productwithcategory
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public Product Product { get; set; }
       
    }
}
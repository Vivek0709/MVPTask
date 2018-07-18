using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVPTask.Models
{
    public class SalesViewModel
    {
        public int Id { get; set; }

        [Range(1, Int32.MaxValue)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        [Range(1, Int32.MaxValue)]
        public int CustomerId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string CustomerName { get; set; }
        public DateTime DateSold { get; set; }
    }
}
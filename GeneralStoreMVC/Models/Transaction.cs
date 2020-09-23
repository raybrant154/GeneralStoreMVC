using GeneralStore.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreMVC.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int AmountPurchased { get; set; }

        public DateTime TimeOfTransaction { get; set; }
    }
}
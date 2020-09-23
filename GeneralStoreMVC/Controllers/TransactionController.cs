using GeneralStore.MVC.Models;
using GeneralStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeneralStoreMVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Transaction
        public ActionResult Index()
        {
            List<Transaction> transactionList = _db.Transactions.ToList();
            List<Transaction> orderedList = transactionList.OrderBy(tran => tran.TransactionID).ToList();
            return View(orderedList);
        }
        // GET: Transaction
        public ActionResult Create()
        {
            var customers = new SelectList(_db.Customers.ToList(), "CustomerID", "FullName");
            ViewBag.Customers = customers;
            var products = new SelectList(_db.Products.ToList(), "ProductID", "Name");
            ViewBag.Products = products;
            return View();
        }
        // POST: Transaction
        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            Product product = _db.Products.Find(transaction.ProductID);
            if(product == null)
            {
                return HttpNotFound();
            }

            Customer customer = _db.Customers.Find(transaction.CustomerID);
            if (customer == null)
            {
                return HttpNotFound();
            }

            if (product.InventoryCount > 0 && product.InventoryCount > transaction.AmountPurchased && ModelState.IsValid)
            {
                
                _db.Transactions.Add(transaction);
                product.InventoryCount -= transaction.AmountPurchased;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }
    }
}
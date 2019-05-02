using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutomationP.ViewModels;

namespace AutomationP.Controllers
{
    public class CashController : Controller
    {

        private readonly ProductContext _context;

        public CashController(ProductContext context)
        {
            _context = context;
        }
        public RedirectToActionResult Buy()
        {
            CartClass cartClass = new CartClass("Cart", _context, HttpContext);
            var carts = cartClass.GetCart().Lines;
            foreach(var el in carts)
            {
                _context.Sales.Add(new Sales { ProductId = el.Product.Id, Quantity = el.Quantity, Price = el.Product.SellingPrice });
            }
            cartClass.Clear();
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Cash
        public ActionResult Index()
        {
            
            int id = int.Parse(User.Claims.ToList()[1].Value);
            var enter = _context.Enterprises.Find(id);
            var NameCategory = "baseCategory." + enter.Name;

            var categories = _context.Categories.Where(p => p.EnterpriseId == id && p.ParentCategory.Name == NameCategory).ToList();
            var products = _context.Products.Where(p => p.ParCategory.EnterpriseId == id && p.ParCategory.Name == NameCategory).ToList();
            ViewBag.categ = categories;
            ViewBag.prod = products;
            ViewBag.ParentCat = null;
            CartClass cartClass = new CartClass("Cart", _context, HttpContext);
            ViewBag.Cart = cartClass.GetCart().Lines ;
            return View();
        }
        public ActionResult SelectPoint()
        {
            int IdEnterprise = int.Parse(User.Claims.ToList()[1].Value);
            Enterprise Enterprise = _context.Enterprises.Find(IdEnterprise);
            User user= _context.Users.FirstOrDefault(s => s.Login == User.Identity.Name);
          //  _context.PointOfSales.Where(p=>p.EnterpriseId==IdEnterprise && user.Role.
            return View();
        }

        public ActionResult Category(int Id)
        {
            int IdEnterprise = int.Parse(User.Claims.ToList()[1].Value);
            Enterprise Enterprise = _context.Enterprises.Find(IdEnterprise);
            string NameCategory = "baseCategory." + Enterprise.Name;

            var cat1 = _context.Categories.Find(Id);
            if (cat1.Name == NameCategory)
               return RedirectToAction("Index","Cash");
            var categories = _context.Categories.Where(p => p.EnterpriseId == IdEnterprise && p.ParentCategory.Name == cat1.Name).ToList();
            var products = _context.Products.Where(p => p.ParCategory.EnterpriseId == IdEnterprise && p.ParCategory.Name == cat1.Name).ToList();
            ViewBag.categ = categories;
            ViewBag.prod = products;
            ViewBag.ParentCatName = cat1.Name;
            ViewBag.ParentCatId = _context.Categories.First(p=>p.Name==cat1.ParentCategory.Name).Id;
            CartClass cartClass = new CartClass("Cart", _context, HttpContext);
            ViewBag.Cart = cartClass.GetCart().Lines;
            return View("Index");
        }
       
        // GET: Cash/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cash/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cash/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cash/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cash/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cash/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cash/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
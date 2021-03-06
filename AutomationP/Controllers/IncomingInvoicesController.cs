﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using AutomationP.ViewModels;

namespace AutomationP.Controllers
{
    public class IncomingInvoicesController : Controller
    {
        private readonly ProductContext _context;

        public IncomingInvoicesController(ProductContext context)
        {
            _context = context;
        }

        public RedirectToActionResult CreateIncomingInvoice([Bind("Id,Date,StorageId")] IncomingInvoice incomingInvoice)
        {
            CartClassForInvoice cartClassForInvoice = new CartClassForInvoice("Product_in_InomingInvoice", _context, HttpContext);
            incomingInvoice.Date = DateTime.Now;
            incomingInvoice.UserId = _context.Users.FirstOrDefault(s => s.Login == User.Identity.Name).Id;
            _context.IncomingInvoices.Add(incomingInvoice);
            _context.SaveChanges();
            var e = cartClassForInvoice.GetCart().Lines;
            foreach (var el in e)
            {
                el.InvoiceId = incomingInvoice.Id;
                Invoice_Product newInvo = new Invoice_Product { InvoiceId = incomingInvoice.Id, ProductId = el.ProductId, Quantity = el.Quantity };
                _context.Invoice_Products.Add(newInvo);
            }
            _context.SaveChanges();
            cartClassForInvoice.Clear();
            
            return RedirectToAction("Index");
        }
        public ActionResult CreatingInvoice(int Id = -1)
        {
            int id = int.Parse(User.Claims.ToList()[1].Value);
            var enter = _context.Enterprises.Find(id);
            var NameCategory = "baseCategory." + enter.Name;
            Category cat1 = null;
            if (Id != -1)
            {
                cat1 = _context.Categories.Find(Id);
                if (NameCategory != cat1.Name)
                {
                    NameCategory = cat1.Name;
                }
                else
                {
                    cat1 = null;
                }
            }
            CartClassForInvoice cartClassForInvoice = new CartClassForInvoice("Product_in_InomingInvoice", _context, HttpContext);

            var e = cartClassForInvoice.GetCart().Lines;
            ViewBag.Products = cartClassForInvoice.GetCart().Lines;
            ViewBag.IncomingInvoice = new IncomingInvoice();
            ViewData["Storages"] = new SelectList(_context.Storages.Where(p => p.EnterpriseId == id), "Id", "Name");
            var categories = _context.Categories.Where(p => p.EnterpriseId == id && p.ParentCategory.Name == NameCategory).ToList();
            var products = _context.Products.Where(p => p.ParCategory.EnterpriseId == id && p.ParCategory.Name == NameCategory).ToList();
            ViewBag.categ = categories;
            ViewBag.prod = products;
            return View();
        }
        // GET: IncomingInvoices
        public async Task<IActionResult> Index(int Id=-1)
        {
            int id = int.Parse(User.Claims.ToList()[1].Value);
            var enter = _context.Enterprises.Find(id);
            var NameCategory = "baseCategory." + enter.Name;

            Category cat1 = null;
            if(Id != -1)
            {
                cat1 = _context.Categories.Find(Id);
                if(NameCategory != cat1.Name)
                {
                    NameCategory = cat1.Name;
                }
                else
                {
                    cat1 = null;
                }
            }

            var productContext = _context.IncomingInvoices.Where(p => p.Storage.EnterpriseId == id);
            ViewBag.IncomingInvoice = new IncomingInvoice();
            /* var e = new List<Product>();
             e.Add(new Product { Name = "sadsad" });*/
            CartClassForInvoice cartClassForInvoice = new CartClassForInvoice("Product_in_InomingInvoice", _context, HttpContext);

            var e = cartClassForInvoice.GetCart().Lines;
            ViewBag.Products = cartClassForInvoice.GetCart().Lines;
            var categories = _context.Categories.Where(p => p.EnterpriseId == id && p.ParentCategory.Name == NameCategory).ToList();
            var products = _context.Products.Where(p => p.ParCategory.EnterpriseId == id && p.ParCategory.Name == NameCategory).ToList();
            ViewBag.categ = categories;
            ViewBag.prod = products;
            ViewData["Storages"] = new SelectList(_context.Storages.Where(p => p.EnterpriseId == id), "Id", "Name");
            if (cat1 != null)
            {
                ViewBag.ParentCatName = cat1.Name;
                ViewBag.ParentCatId = _context.Categories.First(p => p.Name == cat1.ParentCategory.Name).Id;
            }
          
            return View(await productContext.ToListAsync());
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            CartClassForInvoice cartClassForInvoice = new CartClassForInvoice("Product_in_InomingInvoice", _context, HttpContext);
            cartClassForInvoice.AddToCart(id, returnUrl);
            return RedirectToAction("CreatingInvoice");
        }
        // GET: IncomingInvoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomingInvoice = await _context.IncomingInvoices
                .Include(i => i.Storage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomingInvoice == null)
            {
                return NotFound();
            }

            return View(incomingInvoice);
        }

        // GET: IncomingInvoices/Create
        public IActionResult Create()
        {
            int IdEnterprise = int.Parse(User.Claims.ToList()[1].Value);
            ViewData["StorageId"] = new SelectList(_context.Storages.Where(s=>s.EnterpriseId==IdEnterprise), "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products.Where(p => p.ParCategory.EnterpriseId == IdEnterprise));
            return View();
        }

        // POST: IncomingInvoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,StorageId")] IncomingInvoice incomingInvoice)
        {
             int IdEnterprise = int.Parse(User.Claims.ToList()[1].Value);
            if (ModelState.IsValid)
            {

                _context.Add(incomingInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StorageId"] = new SelectList(_context.Storages.Where(s => s.EnterpriseId == IdEnterprise), "Id", "Name", incomingInvoice.StorageId);
            return View(incomingInvoice);
        }

        // GET: IncomingInvoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomingInvoice = await _context.IncomingInvoices.FindAsync(id);
            if (incomingInvoice == null)
            {
                return NotFound();
            }
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Id", incomingInvoice.StorageId);
            return View(incomingInvoice);
        }

        // POST: IncomingInvoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,StorageId")] IncomingInvoice incomingInvoice)
        {
            if (id != incomingInvoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incomingInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomingInvoiceExists(incomingInvoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Id", incomingInvoice.StorageId);
            return View(incomingInvoice);
        }

        // GET: IncomingInvoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incomingInvoice = await _context.IncomingInvoices
                .Include(i => i.Storage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomingInvoice == null)
            {
                return NotFound();
            }

            return View(incomingInvoice);
        }

        // POST: IncomingInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incomingInvoice = await _context.IncomingInvoices.FindAsync(id);
            _context.IncomingInvoices.Remove(incomingInvoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomingInvoiceExists(int id)
        {
            return _context.IncomingInvoices.Any(e => e.Id == id);
        }
    }
}

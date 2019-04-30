using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace AutomationP.Controllers
{
    public class PointOfSalesController : Controller
    {
        private readonly ProductContext _context;

        public PointOfSalesController(ProductContext context)
        {
            _context = context;
        }

        // GET: PointOfSales
        public async Task<IActionResult> Index()
        {
            var productContext = _context.PointOfSales.Include(p => p.Enterprise);
            return View(await productContext.ToListAsync());
        }

        // GET: PointOfSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointOfSale = await _context.PointOfSales
                .Include(p => p.Enterprise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pointOfSale == null)
            {
                return NotFound();
            }

            return View(pointOfSale);
        }

        // GET: PointOfSales/Create
        public IActionResult Create()
        {
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprises, "Id", "Id");
            return View();
        }

        // POST: PointOfSales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,EnterpriseId")] PointOfSale pointOfSale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pointOfSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprises, "Id", "Id", pointOfSale.EnterpriseId);
            return View(pointOfSale);
        }

        // GET: PointOfSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointOfSale = await _context.PointOfSales.FindAsync(id);
            if (pointOfSale == null)
            {
                return NotFound();
            }
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprises, "Id", "Id", pointOfSale.EnterpriseId);
            return View(pointOfSale);
        }

        // POST: PointOfSales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EnterpriseId")] PointOfSale pointOfSale)
        {
            if (id != pointOfSale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointOfSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointOfSaleExists(pointOfSale.Id))
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
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprises, "Id", "Id", pointOfSale.EnterpriseId);
            return View(pointOfSale);
        }

        // GET: PointOfSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointOfSale = await _context.PointOfSales
                .Include(p => p.Enterprise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pointOfSale == null)
            {
                return NotFound();
            }

            return View(pointOfSale);
        }

        // POST: PointOfSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pointOfSale = await _context.PointOfSales.FindAsync(id);
            _context.PointOfSales.Remove(pointOfSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointOfSaleExists(int id)
        {
            return _context.PointOfSales.Any(e => e.Id == id);
        }
    }
}

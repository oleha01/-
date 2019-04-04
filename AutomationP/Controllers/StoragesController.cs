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
    public class StoragesController : Controller
    {
        private readonly ProductContext _context;

        public StoragesController(ProductContext context)
        {
            _context = context;
        }
       public class StructStoragesRemnants
        {
            public Product Item1;
           public int Item2;
           public StructStoragesRemnants(Product p,int c)
            {
                Item1 = p;
                Item2 = c;
            }
            public void ValuePl(int c)
            {
                Item2 += c;
            }
        }
        public async Task<IActionResult> Remnants()
        {
            int IdEnterprise = int.Parse(User.Claims.ToList()[1].Value);

          //  List<IncomingInvoice> listInvoice = await _context.IncomingInvoices.Where(p => p.Storage.EnterpriseId == IdEnterprise).ToListAsync();
            List<Storage> listStorages = await _context.Storages.Where(p => p.EnterpriseId == IdEnterprise).ToListAsync();
            //  List<(Product,Storage, int)> ll = new List<(Product,Storage, int)>();
            List<List<StructStoragesRemnants>> dict = new List<List<StructStoragesRemnants>>();
            foreach(var el in listStorages)
            {
                var el1 = el.IncomingInvoices;
                dict.Add(new List<StructStoragesRemnants>());
                foreach(var invoc in el1)
                {
                  foreach(var prod in invoc.Invoice_Products)
                    {
                        var product = dict.Last().Where(p => p.Item1.Name == prod.Product.Name && p.Item1.ParCategory.Name == prod.Product.ParCategory.Name).Take(1).ToList();
                        if(product.Count==0)
                        {
                            int capacity = prod.Capacity;
                            dict.Last().Add(new StructStoragesRemnants( product[0].Item1, capacity));
                        }
                        else
                        {
                            product[0].ValuePl(prod.Capacity);
                            
                        }
                    }
                }
            }
          //  await productContext.ToListAsync();
            return View();
        }
        // GET: Storages
        public async Task<IActionResult> Index()
        {
            int IdEnterprise = int.Parse(User.Claims.ToList()[1].Value);
            var productContext = _context.Storages.Include(s => s.Enterprise).Where(p=>p.EnterpriseId==IdEnterprise);
            return View(await productContext.ToListAsync());
        }

        // GET: Storages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages
                .Include(s => s.Enterprise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // GET: Storages/Create
        public IActionResult Create()
        {
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprises, "Id", "Id");
            return View();
        }

        // POST: Storages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MaxWeight,EnterpriseId")] Storage storage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprises, "Id", "Id", storage.EnterpriseId);
            return View(storage);
        }

        // GET: Storages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages.FindAsync(id);
            if (storage == null)
            {
                return NotFound();
            }
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprises, "Id", "Id", storage.EnterpriseId);
            return View(storage);
        }

        // POST: Storages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MaxWeight,EnterpriseId")] Storage storage)
        {
            if (id != storage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorageExists(storage.Id))
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
            ViewData["EnterpriseId"] = new SelectList(_context.Enterprises, "Id", "Id", storage.EnterpriseId);
            return View(storage);
        }

        // GET: Storages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages
                .Include(s => s.Enterprise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // POST: Storages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storage = await _context.Storages.FindAsync(id);
            _context.Storages.Remove(storage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorageExists(int id)
        {
            return _context.Storages.Any(e => e.Id == id);
        }
    }
}

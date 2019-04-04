using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Library.Models;
using Microsoft.AspNetCore.Http;

namespace AutomationP.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ProductContext _context;
        
        public CategoriesController(ProductContext context)
        {
            _context = context;

            
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            int IdEnterprise = int.Parse(User.Claims.ToList()[1].Value);
            Enterprise Enterprise = _context.Enterprises.Find(IdEnterprise);
            string NameCategory = "baseCategory." + Enterprise.Name;
            var productContext = _context.Categories.Where(p => p.EnterpriseId == IdEnterprise && p.Name != NameCategory);
            var list = await productContext.ToListAsync();
           var listik= list.Where(p => p.ParentCategory.Name == NameCategory);
            foreach(var el in listik)
            {
                el.ParentCategory.Name = "";
            }
            
            HttpContext.Session.SetString("cater","tty" );
            return View(list);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parCategory = await _context.Categories
                .Include(p => p.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parCategory == null)
            {
                return NotFound();
            }

            return View(parCategory);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            int IdEnterprise = int.Parse(User.Claims.ToList()[1].Value);
            Enterprise Enterprise = _context.Enterprises.Find(IdEnterprise);
            string NameCategory = "baseCategory." + Enterprise.Name;
            var list = new SelectList(_context.Categories.Where(p => p.EnterpriseId == IdEnterprise), "Id", "Name");
            list.First(p => p.Text == NameCategory).Text = "--Select--";
            ViewData["ParentCategoryId"] = list;
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentCategoryId")] Category parCategory)
        {
            if (ModelState.IsValid)
            {
                int IdEnterprise = int.Parse(User.Claims.ToList()[1].Value);
                Enterprise Enterprise = _context.Enterprises.Find(IdEnterprise);
                string NameCategory = "baseCategory." + Enterprise.Name;
                parCategory.EnterpriseId = IdEnterprise;
                if (parCategory.ParentCategoryId == 0)
                {
                    _context.Categories.First(p => p.Name == NameCategory);
                    parCategory.ParentCategoryId = _context.Categories.First(p => p.Name == NameCategory).Id;
                }
                _context.Add(parCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Id", parCategory.ParentCategoryId);
            return View(parCategory);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parCategory = await _context.Categories.FindAsync(id);
            if (parCategory == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Id", parCategory.ParentCategoryId);
            return View(parCategory);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ParentCategoryId")] Category parCategory)
        {
            if (id != parCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParCategoryExists(parCategory.Id))
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
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Id", parCategory.ParentCategoryId);
            return View(parCategory);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parCategory = await _context.Categories
                .Include(p => p.ParentCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parCategory == null)
            {
                return NotFound();
            }

            return View(parCategory);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parCategory = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(parCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParCategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}

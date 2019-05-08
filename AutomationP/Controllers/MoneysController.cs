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
    public class MoneysController : Controller
    {
        private readonly ProductContext _context;

        public MoneysController(ProductContext context)
        {
            _context = context;
        }

        // GET: Moneys
        public async Task<IActionResult> Index()
        {
            int IdEnterprise = int.Parse(User.Claims.ToList()[1].Value);
            Enterprise Enterprise = _context.Enterprises.Find(IdEnterprise);
            string NameCategory = "baseCategory." + Enterprise.Name;
            var productContext = _context.Money.Include(m => m.Point).Include(m => m.User);
            List<MoneyAndSales> money = new List<MoneyAndSales>();
            foreach(var el in productContext)
            {
           
                money.Add(new MoneyAndSales { Date=el.Date,User=el.User,PointOfSale=el.Point,Price=el.Price,Coment=el.Coment,Prychyna="Інше"});
            }
            var Sales = _context.Sales.Where(p => p.PointOfSale.EnterpriseId == IdEnterprise);
            foreach(var el in Sales)
            {
                var Prod =_context.Sales_Products.Where(p => p.SaleId == el.Id);
                int summ = 0;
                foreach(var el1 in Prod)
                {
                    summ += el1.Price * el1.Count;
                }
                money.Add(new MoneyAndSales { Date = el.Date, User = el.User, PointOfSale = el.PointOfSale, Price = summ, Coment = "", Prychyna = "Продажа" });
            }
            money.Sort((p1,p2) => { if (p1.Date > p2.Date) return -1; if (p1.Date < p2.Date) return 1; return 0; });
         
            ViewBag.PointSelect =new SelectList( _context.PointOfSales.Where(p=>p.EnterpriseId==IdEnterprise),"Id","Name");
            return View(money);
        }

        // GET: Moneys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var money = await _context.Money
                .Include(m => m.Point)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (money == null)
            {
                return NotFound();
            }

            return View(money);
        }

        // GET: Moneys/Create
        public IActionResult Create()
        {
            ViewData["PointId"] = new SelectList(_context.PointOfSales, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Moneys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PointId,Data,UserId,Price,Coment")] Money money)
        {
            if (ModelState.IsValid)
            {
                money.Date = DateTime.Now;
                money.UserId = _context.Users.First(p => p.Login == User.Identity.Name).Id;
                _context.Add(money);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            /* ViewData["PointId"] = new SelectList(_context.PointOfSales, "Id", "Id", money.PointId);
             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", money.UserId);
             return View(money);*/
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CreateR([Bind("Id,PointId,Data,UserId,Price,Coment")] Money money)
        {
            if (ModelState.IsValid)
            {
                money.Date = DateTime.Now;
                money.UserId = _context.Users.First(p => p.Login == User.Identity.Name).Id;
                money.Price *= -1;
                _context.Add(money);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            /* ViewData["PointId"] = new SelectList(_context.PointOfSales, "Id", "Id", money.PointId);
             ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", money.UserId);
             return View(money);*/
            return RedirectToAction(nameof(Index));
        }

        // GET: Moneys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var money = await _context.Money.FindAsync(id);
            if (money == null)
            {
                return NotFound();
            }
            ViewData["PointId"] = new SelectList(_context.PointOfSales, "Id", "Id", money.PointId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", money.UserId);
            return View(money);
        }

        // POST: Moneys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PointId,Data,UserId,Price,Coment")] Money money)
        {
            if (id != money.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(money);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoneyExists(money.Id))
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
            ViewData["PointId"] = new SelectList(_context.PointOfSales, "Id", "Id", money.PointId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", money.UserId);
            return View(money);
        }

        // GET: Moneys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var money = await _context.Money
                .Include(m => m.Point)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (money == null)
            {
                return NotFound();
            }

            return View(money);
        }

        // POST: Moneys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var money = await _context.Money.FindAsync(id);
            _context.Money.Remove(money);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoneyExists(int id)
        {
            return _context.Money.Any(e => e.Id == id);
        }
    }
}

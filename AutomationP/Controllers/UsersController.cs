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
    public class UsersController : Controller
    {
        private readonly ProductContext _context;

        public UsersController(ProductContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            int id = int.Parse(User.Claims.ToList()[1].Value);
            var productContext = _context.Users.Where(p=>p.Role.EnterpriseId==id);
            var ttt= _context.Roles.Where(p => p.EnterpriseId == id);
            var uu = ttt.ToList().Capacity;
            ViewBag.Role = ttt.ToList();
            ViewBag.Point = _context.PointOfSales.Where(p => p.EnterpriseId == id).ToList();
            ViewBag.Storage = _context.Storages.Where(p => p.EnterpriseId == id).ToList();
            ViewBag.EnterpriseName = _context.Enterprises.Find(id).Name;
            return View(await productContext.ToListAsync());
        }

        // GET: Users/Details/5
      /*  public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }*/
        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LastName,Patronymic,Name,Login,Email,Password")] User user, string textboxStorageForCheck, string textboxRoleForCheck, string textboxPointForCheck)
        {
            if (ModelState.IsValid)
            {
                int id = int.Parse(User.Claims.ToList()[1].Value);
                var enter = _context.Enterprises.Find(id);

                var Roles = _context.Roles.Where(p => p.EnterpriseId == id).ToList();
                var Storages = _context.Storages.Where(p => p.EnterpriseId == id).ToList();
                var Points = _context.PointOfSales.Where(p => p.EnterpriseId == id).ToList();

                List<Role> UsingRole = new List<Role>();
                List<PointOfSale> UsingPoint= new List<PointOfSale>();
                List<Storage> UsingStorage = new List<Storage>();

                for (int i = 0; i < textboxStorageForCheck.Length; i++)
                {
                    if (textboxStorageForCheck[i] == '1')
                    {
                        _context.User_Storages.Add(new User_Storage
                        {
                            UserId = user.Id,
                            StorageId = Storages[i].Id
                        });
                    }
                }

                for (int i = 0; i < textboxRoleForCheck.Length; i++)
                {
                    if (textboxRoleForCheck[i] == '1')
                    {
                        _context.User_Points.Add(new User_Point
                        {
                            UserId = user.Id,
                            PointId = Points[i].Id
                        });
                    }
                }


                _context.Add(user);
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Login,Email,Password,RoleId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}

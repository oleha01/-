using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AuthApp.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace AuthApp.Controllers
{
    public class AccountController : Controller
    {
        private ProductContext db;
        public AccountController(ProductContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Login,user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    Enterprise newEnter = new Enterprise
                    {
                        Name = model.EnterpriseName
                    };
                    db.Enterprises.Add(newEnter);
                   db.SaveChanges();
                    Storage baseStorage = new Storage
                    {
                        Name = "Головний",
                        EnterpriseId = newEnter.Id
                    };
                    Role newRole = new Role
                    {
                        Name = "admin",
                        EnterpriseId = newEnter.Id
                    };
                    db.Roles.Add(newRole);
                    db.Storages.Add(baseStorage);
                  db.SaveChanges();
                    User regUser = new User
                    {
                        Email = model.Email,
                        Password = model.Password,
                        RoleId = newRole.Id,
                        Login = model.EnterpriseName + ".admin"
                    };

                    db.Users.Add(regUser);
                    Category baseCategory = new Category
                    {
                        Name = "baseCategory." + model.EnterpriseName,
                        EnterpriseId = newEnter.Id
                    };
                    db.Categories.Add(baseCategory);
                    await db.SaveChangesAsync();

                    await Authenticate(regUser.Login, regUser); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName,User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,user.Role.EnterpriseId.ToString())
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
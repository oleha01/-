using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Library.Models;
using Microsoft.AspNetCore.Http;
using AutomationP.ViewModels;
namespace AutomationP.Controllers
{
    public class CartController : Controller
    {
        private ProductContext repository;
        public CartController(ProductContext repo)
        {
            repository = repo;
           
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            CartClass cartClass = new CartClass("Cart", repository, HttpContext);
            /* Product product = repository.Products
                 .FirstOrDefault(g => g.Id == id);

             if (product != null)
             {    Cart cart = GetCart();
                 cart.AddItem(product, 1);
                 HttpContext.Session.Set("Cart", cart);
             }*/
            cartClass.AddToCart(id, returnUrl);
            return RedirectToAction("Index","Cash");
        }

        public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
        {
            CartClass cartClass = new CartClass("Cart", repository, HttpContext);
            /* Product product = repository.Products
                 .FirstOrDefault(g => g.Id == id);

             if (product != null)
             {
                 GetCart().RemoveLine(product);
             }*/
            cartClass.RemoveFromCart(id, returnUrl);
            return RedirectToAction("Index", "Cash");
        }
    }
}

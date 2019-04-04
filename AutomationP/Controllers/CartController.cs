using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Library.Models;
using Microsoft.AspNetCore.Http;
namespace AutomationP.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductContext _context;
        public CartController(ProductContext context)
        {
            _context = context;
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        // public string AddToCart(int id, string returnUrl)
        {
            Product product = _context.Products
                .FirstOrDefault(p => p.Id == id);
            Cart tt = null;
            if (product != null)
            {
                tt = GetCart();
                tt.AddItem(product, 1);
            }
            //var s = returnUrl.Split('/');
            
            HttpContext.Session.Remove("Cart");
          
            HttpContext.Session.Set("Cart", tt);
            tt = GetCart();
            return RedirectToAction("Index", "Cash", new { a = 1 });
           // return JsonConvert.SerializeObject(tt).ToString() ;
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _context.Products
               .FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Categories", "Home", new { a = 10, h = 12 });
        }

        public Cart GetCart()
        {
            
            Cart cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Session.Set("Cart", cart);
            }
            return cart;
        }
    }
}

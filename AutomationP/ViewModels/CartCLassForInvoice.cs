using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Library.Models;
using Microsoft.AspNetCore.Http;

namespace AutomationP.ViewModels
{
    public class CartClassForInvoice
    {
        ProductContext repository;
        HttpContext httpContext;
        string session;
        public CartClassForInvoice(string _session, ProductContext _repository, HttpContext _httpContext)
        {
            repository = _repository;
            httpContext = _httpContext;
            session = _session;
        }
        public void AddToCart(int id, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(g => g.Id == id);

            if (product != null)
            {
                CartForInvoice cart = GetCart();
                cart.AddItem(product, 1);
                httpContext.Session.Set(session, cart);
            }
        }

        public void RemoveFromCart(int id, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(g => g.Id == id);

            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
        }

        public CartForInvoice GetCart()
        {
            CartForInvoice cart = httpContext.Session.Get<CartForInvoice>(session);
            if (cart == null)
            {
                cart = new CartForInvoice();
                httpContext.Session.Set(session, cart);

            }
            return cart;
        }
    }
}

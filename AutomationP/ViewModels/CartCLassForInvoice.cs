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
        public void Clear()
        {
            httpContext.Response.Cookies.Delete(session);
        }
        public void AddToCart(int id, string returnUrl)
        {
            Product product = repository.Products
               .FirstOrDefault(p => p.Id == id);
            CartForInvoice tt = null;
            if (product != null)
            {
                tt = GetCart();
                tt.AddItem(product, 1);
            }
            //var s = returnUrl.Split('/');
            string seriz = JsonConvert.SerializeObject(tt);
            httpContext.Response.Cookies.Delete(session);
            httpContext.Response.Cookies.Append(session, seriz);
            tt = GetCart();
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
            string cart1 = httpContext.Request.Cookies[session];
            CartForInvoice cart;
            if (cart1 == null)
            {
                cart = new CartForInvoice();
                httpContext.Response.Cookies.Append(session, JsonConvert.SerializeObject(cart));
            }
            else
            {
                cart = JsonConvert.DeserializeObject<CartForInvoice>(cart1);
            }
            return cart;

        }
    }
}

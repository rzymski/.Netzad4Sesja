using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zad4.Models;
using System.Text.Json;
using zad4.DAL;
using System.Reflection;


namespace zad4.Pages.Basket
{
    public class IndexModel : MyPageModel
    {
        public BasketModel koszyk = null;
        public List<Product> productList;
        public void OnGet()
        {
            var koszykJson = Request.Cookies["Koszyk"];
            if (!string.IsNullOrEmpty(koszykJson))
                koszyk = JsonSerializer.Deserialize<BasketModel>(koszykJson, ProductDictionaryKeyConverter.jsonSerializeConventorOptions);

            LoadDB();
            productList = productDB.List();
        }

        public IActionResult OnGetAddToBasket(int id) 
        {
            LoadDB();
            Product product = productDB.getProduct(id);
            koszyk = new BasketModel();
            var koszykJson = Request.Cookies["Koszyk"];
            if (!string.IsNullOrEmpty(koszykJson))
                koszyk = JsonSerializer.Deserialize<BasketModel>(koszykJson, ProductDictionaryKeyConverter.jsonSerializeConventorOptions);
            Product existedProduct = null;
            foreach (var item in koszyk.Products)
                if (item.Key.name == product.name && item.Key.price == product.price && item.Key.id == product.id)
                    existedProduct = item.Key;
            if (existedProduct != null)
                koszyk.Products[existedProduct] = koszyk.Products[existedProduct] + 1;
            else
                koszyk.Products.Add(product, 1);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1)
            };
            Response.Cookies.Append("Koszyk", JsonSerializer.Serialize(koszyk, ProductDictionaryKeyConverter.jsonSerializeConventorOptions), cookieOptions);
            return RedirectToPage();
        }

        public IActionResult OnGetAddToBasketReturnToPreviousPage(int id)
        {
            LoadDB();
            Product product = productDB.getProduct(id);
            koszyk = new BasketModel();
            var koszykJson = Request.Cookies["Koszyk"];
            if (!string.IsNullOrEmpty(koszykJson))
                koszyk = JsonSerializer.Deserialize<BasketModel>(koszykJson, ProductDictionaryKeyConverter.jsonSerializeConventorOptions);
            Product existedProduct = null;
            foreach (var item in koszyk.Products)
                if (item.Key.name == product.name && item.Key.price == product.price && item.Key.id == product.id)
                    existedProduct = item.Key;
            if (existedProduct != null)
                koszyk.Products[existedProduct] = koszyk.Products[existedProduct] + 1;
            else
                koszyk.Products.Add(product, 1);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1)
            };
            Response.Cookies.Append("Koszyk", JsonSerializer.Serialize(koszyk, ProductDictionaryKeyConverter.jsonSerializeConventorOptions), cookieOptions);
            string url = Request.Headers["Referer"].ToString();
            return Redirect(url);
        }

        public IActionResult OnPostClearBasket()
        {
            var koszyk = new BasketModel();
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1)
            };
            Response.Cookies.Append("Koszyk", JsonSerializer.Serialize(koszyk, ProductDictionaryKeyConverter.jsonSerializeConventorOptions), cookieOptions);
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteBasket()
        {
            var koszyk = new BasketModel();
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(-1)
            };
            Response.Cookies.Append("Koszyk", JsonSerializer.Serialize(koszyk, ProductDictionaryKeyConverter.jsonSerializeConventorOptions), cookieOptions);
            return RedirectToPage();
        }

        public IActionResult OnPostDeleteNotAvailableProducts(string basketJSON, string productsJSON) 
        {
            productList = JsonSerializer.Deserialize<List<Product>>(productsJSON, ProductDictionaryKeyConverter.jsonSerializeConventorOptions);
            koszyk = JsonSerializer.Deserialize<BasketModel>(basketJSON, ProductDictionaryKeyConverter.jsonSerializeConventorOptions);
            if (koszyk is null) return RedirectToPage();

            foreach (var item in koszyk.Products)
            {
                if (!productList.Any(p => (p.name == item.Key.name && p.price == item.Key.price)))
                {
                    Console.WriteLine("POWINNO USUNAC: "+item.Key);
                    koszyk.Products.Remove(item.Key);
                }
            }
            Console.WriteLine(koszyk);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1)
            };
            Response.Cookies.Append("Koszyk", JsonSerializer.Serialize(koszyk, ProductDictionaryKeyConverter.jsonSerializeConventorOptions), cookieOptions);
            return RedirectToPage();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using zad4.DAL;
using zad4.Models;

namespace zad4.Pages.Products
{
    public class EditModel : MyPageModel
    {
        [BindProperty]
        public Product newProduct { get; set; }
        public List<Product> productList;
        public void OnGet(int id)
        {
            LoadDB();
            productList = productDB.List();
            //SaveDB();
            foreach (var product in productList)
            {
                if (product.id == id)
                {
                    newProduct = product;
                    break;
                }
            }
            TempData["oldProductName"] = newProduct.name;
        }
        public IActionResult OnPost()
        {
            string oldProductName = TempData["oldProductName"] as string;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                LoadDB();
                if (productDB.getProducts().Any(p => p.name == newProduct.name && newProduct.name != oldProductName))
                {
                    ModelState.AddModelError("newProduct.name", "Nazwa jest ju¿ zajêta przez inny produkt");
                    TempData["oldProductName"] = oldProductName;
                    return Page();
                }
                productDB.Edit(newProduct);
                SaveDB();
                return RedirectToPage("List");
            }
        }
    }
}

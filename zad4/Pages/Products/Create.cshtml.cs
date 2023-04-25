using Microsoft.AspNetCore.Mvc;
using zad4.Models;

namespace zad4.Pages.Products
{
    public class CreateModel : MyPageModel
    {
        [BindProperty]
        public Product newProduct { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                LoadDB();
                if (productDB.getProducts().Any(p => p.name == newProduct.name))
                {
                    ModelState.AddModelError("newProduct.name", "Nazwa jest ju¿ zajêta przez inny produkt");
                    return Page();
                }
                productDB.Create(newProduct);
                SaveDB();
                return RedirectToPage("List");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using zad4.Models;

namespace zad4.Pages.Products
{
    public class DeleteModel : MyPageModel
    {
        [BindProperty]
        public Product newProduct { get; set; }
        public void OnGet(int id)
        {
            LoadDB();
            newProduct = productDB.getProduct(id);
        }
        public IActionResult OnPost()
        {
            LoadDB();
            productDB.Delete(newProduct.id);
            SaveDB();
            return RedirectToPage("List");
        }
    }
}

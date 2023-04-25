using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using zad4.DAL;
using zad4.Models;

namespace zad4.Pages.Products
{
    public class DetailsModel : MyPageModel
    {
        public Product newProduct { get; set; }
        public void OnGet(int id)
        {
            LoadDB();
            newProduct = productDB.getProduct(id);
            TempData["idProductu"] = id.ToString();
        }

        public IActionResult OnPost()
        {
            int idProductu = int.Parse(TempData["idProductu"] as string);
            return RedirectToPage("./../Basket/Index", "AddToBasket", new {id = idProductu});
        }
    }
}

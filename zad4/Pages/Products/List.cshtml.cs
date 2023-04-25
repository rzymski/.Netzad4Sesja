using System.Collections.Generic;
using zad4.Models;

namespace zad4.Pages.Products
{
    public class ListModel : MyPageModel
    {
        public List<Product> productList;
        public void OnGet()
        {
            LoadDB();
            productList = productDB.List();
            SaveDB();
        }
    }
}
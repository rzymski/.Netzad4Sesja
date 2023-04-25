using System;
using System.Collections.Generic;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using zad4.Models;

namespace zad4.DAL
{
    public class ProductDB
    {
        private List<Product> products;
        public void Load(string jsonProducts)
        {
            if (jsonProducts == null)
            {
                products = Product.GetProducts();
            }
            else
            {
                products = JsonSerializer.Deserialize<List<Product>>(jsonProducts);
            }
        }
        //private int GetNextId()
        //{
        //    int lastID;
        //    if(products.Count > 0)
        //        lastID = products[products.Count - 1].id;
        //    else
        //        lastID = 0;
        //    int newID = lastID+1;
        //    return newID;
        //}
        public void Create(Product p)
        {
            //p.id = GetNextId();
            p.id = products.Max(x => x.id) + 1;
            products.Add(p);
        }
        public string Save()
        {
            return JsonSerializer.Serialize(products);
        }
        public List<Product> List()
        {
            return products;
        }

        public void Edit(Product p)
        {
            //products[p.id-1] = p;
            int indx = products.IndexOf(getProduct(p.id));
            Console.WriteLine(indx);
            products[indx] = p;
        }

        //public void Delete(int id) 
        //{
        //    products.RemoveAt(id-1);
        //    foreach (Product p in products)
        //        if (p.id > id)
        //            p.id = p.id - 1;
        //}

        public void Delete(int id)
        {
            products.Remove(getProduct(id));
        }

        public Product getProduct(int id) 
        {
            return products.Single(p => p.id == id);
        }

        public List<Product> getProducts()
        {
            return products.ToList();
        }

    }
}

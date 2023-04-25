using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace zad4.Models
{
    public class Product
    {
        [Display(Name = "Id")]
        public int id { get; set; }
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        public string name { get; set; }
        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Cena jest wymagana.")]
        [Range(0.01, double.MaxValue, ErrorMessage ="Cena musi być większa od 0")]
        public decimal price { get; set; }
        public Product() {}
        public Product(int id, string name, decimal price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }
        public static List<Product> GetProducts()
        {
            Product pilka = new Product { id = 1, name = "Piłka nożna", price = 25.30M };
            Product narty = new Product { id = 2, name = "Narty", price = 150.39M };
            Product rakieta = new Product { id = 3, name = "Rakieta ", price = 111.10M };

            return new List<Product> { pilka, narty, rakieta };
        }
        public override string ToString() 
        {
            return "Id: "+id.ToString()+" Nazwa: "+name.ToString()+" Cena: "+price.ToString();
        }
    }

}

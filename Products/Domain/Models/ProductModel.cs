using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Domain.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Amount { get; set; }
        public float? Price { get; set; }
        public string? Barcode { get; set; }
        public string? Code { get; set; }


        public ProductModel (){}
        public ProductModel (string name, double? amount, float? price, string? barcode, string? code){
            Name = name;
            Amount = amount;
            Price = price;
            Code = code;
            
        }


    }
}
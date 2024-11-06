using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Application.DTOs
{
    public class CreateProductDTO
    {
        public string? Name { get; set; }
        public float? Price { get; set;}
        public double? Amount { get; set;}

        public CreateProductDTO(){}
        public CreateProductDTO(string? name, float? price, double? amount){
            Name = name;
            Price = price;
            Amount = amount;
        }
    }
}
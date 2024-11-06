using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Application.DTOs
{
    public class ViewProductDTO
    {
         public string? Name { get; set; }
        public float? Price { get; set;}
        public double? Amount { get; set;}
        public string? Barcode { get; set; }
        public string? Code { get; set; }


        public ViewProductDTO(){}
        public ViewProductDTO(string? name, float? price, double? amount, string? barcode, string? code){
            Name = name;
            Price = price;
            Amount = amount;
            Code = code;
            Barcode = barcode;
        }

    }
}
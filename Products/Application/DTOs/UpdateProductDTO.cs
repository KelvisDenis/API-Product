using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Application.DTOs
{
    public class UpdateProductDTO
    {
        public int Id { get; set;}
        public string? Name { get; set; }
        public float? Price { get; set;}
        public double? Amount { get; set;}
         public string? Barcode { get; set; }
        public string? Code { get; set; }

        public UpdateProductDTO(){}
        public UpdateProductDTO(int id, string? name, float? price, double? amount, string? barcode, string? code){
            Id = id;
            Name = name;
            Price = price;
            Amount = amount;
            Code = code;
            Barcode = barcode;
        }
    }
}
using System;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
	public class ProductResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public ProductResponse() { }
        public ProductResponse(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Unit = product.Unit;
        }
    }
}


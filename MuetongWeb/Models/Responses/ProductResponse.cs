using System;
using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
	public class ProductResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Unit { get; set; } = string.Empty;
        public bool CanDelete { get; set; } = true;
        public ProductResponse() { }
        public ProductResponse(Product product)
        {
            Id = product.Id;
            Name = string.IsNullOrWhiteSpace(product.Name) ? string.Empty : product.Name;
            Unit = string.IsNullOrWhiteSpace(product.Unit) ? string.Empty : product.Unit;
            if ((product.PrDetails != null && product.PrDetails.Any()) || (product.PoDetails != null && product.PoDetails.Any()))
                CanDelete = false;
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace startProject.Model
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Aantal")]
        [Range(1, int.MaxValue, ErrorMessage = "'{0}' moet groter dan of gelijk zijn aan {1} .")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "'{0}' is verplicht.")]
        public int Quantity { get; set; } = 1;

        [DisplayName("ProductName")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "'{0}' is verplicht.")]
        public string ProductName { get; set; }

        public static List<OrderLine> OrderLinesList { get; set; } = new List<OrderLine>();

        // constructors
        public OrderLine()
        {
        }

        public OrderLine(string productName, int orderQuantity)
        {
            this.ProductName = productName;
            this.Quantity = orderQuantity;
        }
    }
}
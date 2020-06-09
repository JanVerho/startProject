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
        [Required(AllowEmptyStrings = false, ErrorMessage = "'{0}' is verplicht.")]
        [Range(1, int.MaxValue, ErrorMessage = "'{0}' moet een geheel getal zijn .")]
        public int Quantity { get; set; } = 1;

        [DisplayName("ProductNaam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "'{0}' is verplicht.")]
        public string ProductName { get; set; }

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
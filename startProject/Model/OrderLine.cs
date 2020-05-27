using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace startProject.Model
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Aantal")]
        [Range(1, int.MaxValue, ErrorMessage = "'{0}' moet groter of gelijk zijn aan {1} .")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "'{0}' is verplicht.")]
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        [Range(0, 10, ErrorMessage = "'{0' waarde is niet in lijst")]
        public Product Product { get; set; }

        public List<OrderLine> OrderLinesList { get; set; }

        // constructors
        public OrderLine()
        {
        }

        public OrderLine(int quantityOrdered)
        {
            this.Quantity = quantityOrdered;
        }

        public OrderLine(Product product, int orderQuantity)
        {
            this.Product = product;
            this.Quantity = orderQuantity;
        }
    }
}
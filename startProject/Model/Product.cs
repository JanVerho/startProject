using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace startProject.Model
{
    public class Product
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "'{0}' is verplicht.")]
        public int Id { get; set; }

        [DisplayName("Naam")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "'{0}' is verplicht.")]
        public string Name { get; set; }

        [DisplayName("Bloei vanaf")]
        [Range(1, 52, ErrorMessage = "'{0}' moet min {1} en max {2} zijn .")]
        public int WeekNrFlowerStart { get; set; }

        [DisplayName("Bloei tot")]
        [Range(1, 52, ErrorMessage = "'{0}' moet min {1} en max {2} zijn .")]
        public int WeekNrFlowerEnd { get; set; }

        public Product()
        {
        }

        public Product(int id, string name, int weekNrFlowerStart, int weekNrFlowerEnd)
        {
            this.Id = id;
            this.Name = name;
            this.WeekNrFlowerStart = weekNrFlowerStart;
            this.WeekNrFlowerEnd = weekNrFlowerEnd;
        }
    }
}
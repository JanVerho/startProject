using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace startProject.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Naam")]
        public string Name { get; set; }

        [DisplayName("Bloei vanaf")]
        public int WeekNrFlowerStart { get; set; }

        [DisplayName("Bloei tot")]
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
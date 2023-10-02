using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Image { get; set; }
        public float Prize { get; set; }

        public Pizza() { }
        public Pizza(string name, string description, string image ) 
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
        }
    }
}

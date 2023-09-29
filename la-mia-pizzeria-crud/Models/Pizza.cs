namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public float Prize { get; set; }


        public Pizza(string name, string description, string image ) 
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
        }
    }
}

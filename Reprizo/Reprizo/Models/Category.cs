namespace Reprizo.Models
{
    public class Category :  BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Product> Products { get; set; }
    }
}

using Reprizo.Models;

namespace Reprizo.Areas.Admin.ViewModels.Product
{
	public class ProductDetailVM
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public List<ProductImage> Images { get; set; }
		public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateDate { get; set; }
	}
}

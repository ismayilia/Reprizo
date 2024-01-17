using System.ComponentModel.DataAnnotations;

namespace Reprizo.Areas.Admin.ViewModels.Essence
{
	public class EssenceEditVM
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		public IFormFile Photo { get; set; }
		public string Image { get; set; }
	}
}

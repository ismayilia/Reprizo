using System.ComponentModel.DataAnnotations;

namespace Reprizo.Areas.Admin.ViewModels.Feature
{
	public class FeatureEditVM
	{
		public int Id { get; set; }
		public string Image { get; set; }
		[Required]
		public string TitleLeft { get; set; }
		[Required]
		public string DescriptionLeft { get; set; }
		[Required]
		public string TitleRight { get; set; }
		[Required]
		public string DescriptionRight { get; set; }
		public IFormFile Photo { get; set; }
	}
}

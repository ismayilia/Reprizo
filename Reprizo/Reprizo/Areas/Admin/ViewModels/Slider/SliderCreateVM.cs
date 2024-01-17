using System.ComponentModel.DataAnnotations;

namespace Reprizo.Areas.Admin.ViewModels.Slider
{
	public class SliderCreateVM
	{
		
		[Required]
		public string Title { get; set; }
		[Required]
		public IFormFile Photo { get; set; }
	}
}

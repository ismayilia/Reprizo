using System.ComponentModel.DataAnnotations;

namespace Reprizo.Areas.Admin.ViewModels.Team
{
	public class TeamEditVM
	{
		public int Id { get; set; }
		[Required]
		public string FullName { get; set; }
		[Required]
		public string Position { get; set; }
		public IFormFile Photo { get; set; }
		public string Image { get; set; }
	}
}

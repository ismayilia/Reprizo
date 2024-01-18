using System.ComponentModel.DataAnnotations;

namespace Reprizo.Areas.Admin.ViewModels.BestWorker
{
	public class BestWorkerEditVM
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
	}
}

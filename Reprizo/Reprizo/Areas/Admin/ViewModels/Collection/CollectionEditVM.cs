using System.ComponentModel.DataAnnotations;

namespace Reprizo.Areas.Admin.ViewModels.Collection
{
	public class CollectionEditVM
	{
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

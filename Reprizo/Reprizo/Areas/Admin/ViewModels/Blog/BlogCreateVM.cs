using System.ComponentModel.DataAnnotations;

namespace Reprizo.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Writer { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}

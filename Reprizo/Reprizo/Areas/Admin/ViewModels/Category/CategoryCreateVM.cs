using System.ComponentModel.DataAnnotations;

namespace Reprizo.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}

namespace Reprizo.Areas.Admin.ViewModels.Blog
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Writer { get; set; }
        public string Image { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

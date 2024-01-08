namespace Reprizo.Models
{
    public class Feature : BaseEntity
    {
        public string Image { get; set; }
        public string TitleLeft { get; set; }
        public string DescriptionLeft { get; set; }
        public string TitleRight { get; set; }
        public string DescriptionRight { get; set; }
    }
}

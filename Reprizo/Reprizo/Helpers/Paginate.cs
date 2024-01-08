namespace Reprizo.Helpers
{
    public class Paginate<T>
    {
        public List<T> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public string BlogBanner { get; set; }

        public Paginate(List<T> datas, int currentPage, int totalPage, string blogBanner)
        {
            Datas = datas;
            CurrentPage = currentPage;
            TotalPage = totalPage;
            BlogBanner = blogBanner;
        }

        public bool HasPrevios
        {
            get
            {
                return CurrentPage > 1;
            }
        }

        public bool HasNext
        {
            get
            {
                return CurrentPage < TotalPage;
            }
        }
    }
}

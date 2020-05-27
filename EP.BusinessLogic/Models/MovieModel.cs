namespace EP.BusinessLogic.Models
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }

    public class MovieSeason
    {
        public int Season { get; set; }
        public bool IsSelected { get; set; }
    }
}

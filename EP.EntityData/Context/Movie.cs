namespace EP.EntityData.Context
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Movie")]
    public partial class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public int Season { get; set; }
    }
}

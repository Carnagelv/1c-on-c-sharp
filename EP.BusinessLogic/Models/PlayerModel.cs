using EP.EntityData.Helpers;

namespace EP.BusinessLogic.Models
{
    public class PlayersViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool HasOwner { get; set; }
        public DisciplineEnum Discipline { get; set; }
    }

    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthYear { get; set; }
        public bool HasOwner { get; set; }
        public string FullName { get; set; }
        public int? UserId { get; set; }
        public string Photo { get; set; }
    }

    public class Bombardier
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string TeamLogo { get; set; }
        public int Goals { get; set; }
    }

    public class Roster
    {
        public string FullName { get; set; }
    }
}

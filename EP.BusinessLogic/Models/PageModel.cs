using EP.EntityData.Helpers;

namespace EP.BusinessLogic.Models
{
    public class PageModel
    {
        public int ID { get; set; }
        public bool IsOwner { get; set; }
        public bool IsActive { get; set; }
        public FriendPageTypeEnum ActionType { get; set; }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Default
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

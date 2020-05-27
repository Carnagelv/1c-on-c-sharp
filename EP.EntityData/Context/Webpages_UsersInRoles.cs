namespace EP.EntityData.Context
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("webpages_UsersInRoles")]
    public partial class Webpages_UsersInRoles
    {
        public int ID { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public virtual UserProfile User { get; set; }
        public virtual Webpages_Roles Role { get; set; }
    }
}

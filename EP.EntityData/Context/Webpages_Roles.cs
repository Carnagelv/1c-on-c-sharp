namespace EP.EntityData.Context
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("webpages_Roles")]
    public partial class Webpages_Roles
    {
        public Webpages_Roles()
        {
            UsersInRoles = new List<Webpages_UsersInRoles>();
        }

        [Key]
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public virtual ICollection<Webpages_UsersInRoles> UsersInRoles { get; set; }
    }
}

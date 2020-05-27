namespace EP.EntityData.Context
{
    using EP.EntityData.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserProfile")]
    public partial class UserProfile
    {
        public UserProfile()
        {
            Roles = new List<Webpages_UsersInRoles>();
            Friends = new List<Friend>();
            RequestToFriends = new List<RequestToFriend>();
            RequestFromFriends = new List<RequestToFriend>();
            NewsCommentaries = new List<NewsCommentary>();
            NewsLikes = new List<NewsLike>();
            Teams = new List<Team>();
        }

        [Key]
        public int UserId { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastActivityDate { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DisciplineEnum? Discipline { get; set; }

        public PositionEnum? Position { get; set; }

        public bool IsActive { get; set; }

        public string Photo { get; set; }

        public virtual ICollection<Webpages_UsersInRoles> Roles { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<RequestToFriend> RequestToFriends { get; set; }
        public virtual ICollection<RequestToFriend> RequestFromFriends { get; set; }
        public virtual ICollection<NewsCommentary> NewsCommentaries { get; set; }
        public virtual ICollection<NewsLike> NewsLikes { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}

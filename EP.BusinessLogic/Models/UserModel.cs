using EP.EntityData.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EP.BusinessLogic.Models
{
    public static class UserRoles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string SportsMan = "SportsMan";
        public const string CyberSportsMan = "CyberSportsMan"; 
    }

    public class UserModel
    {
        [Required, MaxLength(30)]
        public string FirstName { get; set; }
        [Required, MaxLength(30)]
        public string LastName { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        public string CreateDate { get; set; }
    }

    public class RegisterModel: UserModel
    {
        public string Password { get; set; }
        public string RePassword { get; set; }
    }

    public class LoginModel
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        [Required, MaxLength(30)]
        public string Password { get; set; }
    }

    public class UserViewModel: UserModel
    {
        public PositionEnum Position { get; set; }
        public DisciplineEnum Discipline { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsFriend { get; set; }
        public bool IsRequested { get; set; }
        public bool IsAsked { get; set; }
        public string Photo { get; set; }
    }

    public class InformerData
    {
        public List<TeamsViewModel> LastTeams { get; set; } = new List<TeamsViewModel>();
        public int Requests { get; set; }
        public ActiveTournamentsView Tournament { get; set; }
    }

    public class OtherUserViewModel
    {
        public string LastLogIn { get; set; }
        public int FriendsCount { get; set; }
        public int UserDialogId { get; set; }
        public List<UserRandomFriend> UserFriends { get; set; } = new List<UserRandomFriend>();
    }

    public class UserRandomFriend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
    }
}

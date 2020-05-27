using System;

namespace EP.BusinessLogic.Models
{
    public class FriendsModel
    {
        public int UserId { get; set; }
        public string Photo { get; set; }
        public string FullName { get; set; }
        public string LastActivyDate { get; set; }
    }

    public class DialogView
    {
        public int DialogId { get; set; }
        public string Recipient { get; set; }
        public string RecipentImg { get; set; }
        public bool IsRead { get; set; }
        public string LastMessage { get; set; }
        public string LastSenderImg { get; set; }
    }

    public class MessageView
    {
        public string Sender { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
        public bool IsRight { get; set; }
    }

    public class UserForDialog
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class JsonMessage
    {
        public int Id { get; set; }
        public string Ms { get; set; }
        public DateTime Dt { get; set; }
    }

    public class DialogParticipants
    {
        public int SenderId { get; set; }
        public string Sender { get; set; }
        public string Photo { get; set; }
    }
}

namespace EP.EntityData.Context
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Message")]
    public partial class Message
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public virtual UserProfile Author { get; set; }

        public int RecipientId { get; set; }
        public virtual UserProfile Recipient { get; set; }

        public string MessageText { get; set; }

        public string LastMessage { get; set; }

        public int LastSenderId { get; set; }
        public virtual UserProfile LastSender { get; set; }

        public bool IsNewMessage { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

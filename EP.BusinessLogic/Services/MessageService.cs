using EP.BusinessLogic.Models;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using Jil;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EP.BusinessLogic.Services
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        public MessageService(IDataContext dataContext) : base(dataContext)
        {
        }

        public IQueryable<Message> GetDialogs(int userId)
        {
            return Dbset.Include(i => i.Author).Where(w => w.AuthorId == userId || w.RecipientId == userId).OrderByDescending(o => o.UpdatedDate).AsQueryable();
        }

        public List<UserForDialog> GetUserForDialog(int userId)
        {
            var friends = DataContext.Friends.Where(w => w.WithID == userId).Select(s => new UserForDialog
            {
                Id = s.WhoID,
                Name = s.Who.FirstName + " " + s.Who.LastName
            }).ToList();

            var userDialogsId = Dbset.Where(w => w.AuthorId == userId).Select(s => s.RecipientId).ToList();
            userDialogsId.AddRange(Dbset.Where(w => w.RecipientId == userId).Select(s => s.AuthorId).ToList());

            var result = new List<UserForDialog>();
            foreach (var friend in friends)
            {
                if (!userDialogsId.Contains(friend.Id))
                    result.Add(friend);
            }

            return result;
        }

        public int CreateDialog(int userId, int recipientId)
        {
            var dialog = new Message();
            if (!Dbset.Any(a => a.AuthorId == userId && a.RecipientId == recipientId))
            {
                dialog = new Message
                {
                    AuthorId = userId,
                    LastSenderId = userId,
                    MessageText = Constants.EMPTY_JSON,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    RecipientId = recipientId
                };

                Add(dialog);
            }

            return dialog.Id;
        }

        public List<JsonMessage> SaveMessage(int userId, int dialogId, string message)
        {
            var entity = Get(g => g.Id == dialogId && (g.AuthorId == userId || g.RecipientId == userId));

            if (entity != null)
            {
                var model = new JsonMessage
                {
                    Dt = DateTime.Now,
                    Id = userId,
                    Ms = message
                };

                var messages = new List<JsonMessage>();
                if (entity.MessageText != Constants.EMPTY_JSON)
                    messages = JSON.Deserialize<List<JsonMessage>>(entity.MessageText);

                messages.Add(model);

                entity.IsNewMessage = true;
                entity.LastMessage = message;
                entity.LastSenderId = userId;
                entity.UpdatedDate = DateTime.Now;
                entity.MessageText = JSON.Serialize(messages);

                Update(entity);

                return messages;
            }

            return new List<JsonMessage>();
        }

        public List<DialogParticipants> GetDialogParticipants(int dialogId)
        {
            var dialog = Get(g => g.Id == dialogId);

            if (dialog != null)
            {
                var senders = new List<DialogParticipants>
                {
                    new DialogParticipants
                    {
                        SenderId = dialog.RecipientId,
                        Photo = dialog.Recipient.Photo,
                        Sender = dialog.Recipient.FirstName + " " + dialog.Recipient.LastName
                    },
                    new DialogParticipants
                    {
                        SenderId = dialog.AuthorId,
                        Photo = dialog.Author.Photo,
                        Sender = dialog.Author.FirstName + " " + dialog.Author.LastName
                    }
                };

                return senders;
            }

            return new List<DialogParticipants>();
        }

        public bool IsExistNewMessage(int userId, int dialogId)
        {
            return Dbset.Any(a => a.Id == dialogId && a.IsNewMessage && a.LastSenderId != userId);
        }
    }

    public interface IMessageService : IService<Message>
    {
        int CreateDialog(int userId, int recipientId);
        List<DialogParticipants> GetDialogParticipants(int dialogId);
        IQueryable<Message> GetDialogs(int userId);
        List<UserForDialog> GetUserForDialog(int userId);
        bool IsExistNewMessage(int userId, int dialogId);
        List<JsonMessage> SaveMessage(int userId, int dialogId, string message);
    }
}

using EP.BusinessLogic.Models;
using EP.BusinessLogic.Services;
using EP.EntityData.Context;
using EP.EntityData.Helpers;
using Jil;
using System.Collections.Generic;
using System.Linq;

namespace EP.BusinessLogic.Managers
{
    public class MessageManager : IMessageManager
    {
        private readonly IMessageService _messageService;

        public MessageManager(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public int CreateDialog(int userId, int recipientId)
        {
            return _messageService.CreateDialog(userId, recipientId);
        }

        public List<MessageView> GetDialog(int userId, int dialogId)
        {
            var dialogsItems = _messageService.Get(g => g.Id == dialogId && (g.AuthorId == userId || g.RecipientId == userId));

            if (dialogsItems != null && dialogsItems.MessageText != Constants.EMPTY_JSON)
            {
                dialogsItems.IsNewMessage = false;
                _messageService.Update(dialogsItems);

                var items = JSON.Deserialize<List<JsonMessage>>(dialogsItems.MessageText);
                var participants = _messageService.GetDialogParticipants(dialogId);

                var outPutMessages = new List<MessageView>();
                foreach (var msg in items)
                {
                    var sender = participants.FirstOrDefault(f => f.SenderId == msg.Id);
                    if (sender != null)
                        outPutMessages.Add(new MessageView
                        {
                            Text = msg.Ms,
                            IsRight = msg.Id == userId,
                            Photo = sender.Photo,
                            Sender = sender.Sender
                        });
                }

                return outPutMessages;
            }

            return new List<MessageView>();
        }

        public List<DialogView> GetDialogs(int userId)
        {
            var dialogs = _messageService.GetDialogs(userId);

            foreach (var dlg in dialogs)
            {
                if (dlg.IsNewMessage)
                    dlg.IsNewMessage = dlg.LastSenderId != userId;

                if (dlg.RecipientId == userId)
                    dlg.Recipient = dlg.Author;
            }

            var mapper = Mappings.GetMapper();

            return mapper.Map<List<Message>, List<DialogView>>(dialogs.ToList());
        }

        public List<UserForDialog> GetUsersForDialog(int userId)
        {
            return _messageService.GetUserForDialog(userId);
        }

        public bool IsExistNewMessage(int userId, int dialogId)
        {
            return _messageService.IsExistNewMessage(userId, dialogId);
        }

        public List<MessageView> SaveMessage(int userId, int dialogId, string message)
        {
            var messages = _messageService.SaveMessage(userId, dialogId, message);
            var participants = _messageService.GetDialogParticipants(dialogId);

            var outPutMessages = new List<MessageView>();
            foreach (var msg in messages)
            {
                var sender = participants.FirstOrDefault(f => f.SenderId == msg.Id);

                if (sender != null) 
                    outPutMessages.Add(new MessageView
                    {
                        Text = msg.Ms,
                        IsRight = msg.Id == userId,
                        Photo = sender.Photo,
                        Sender = sender.Sender
                    });
            }

            return outPutMessages;
        }
    }

    public interface IMessageManager
    {
        List<DialogView> GetDialogs(int userId);
        List<MessageView> GetDialog(int userId, int dialogId);
        bool IsExistNewMessage(int userId, int dialogId);
        List<MessageView> SaveMessage(int userId, int dialogId, string message);
        int CreateDialog(int userId, int recipientId);
        List<UserForDialog> GetUsersForDialog(int userId);
    }
}

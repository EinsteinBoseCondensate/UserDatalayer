using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataLayer.Extensions;
using UserDataLayer.Models.UI.CUD;
using UserDataLayer.Repositories;

namespace UserDataLayer.Services
{
    public class MessageService
    {
        private UsersRepository UsersRepository { get; set; }
        private MessageRepository MessageRepository { get; set; }
        private GroupUserRepository GroupUserRepository { get; set; }
        private ILogger Logger { get; set; }

        public MessageService(UsersRepository usersRepository, MessageRepository messageRepository, GroupUserRepository groupUserRepository, ILoggerProvider provider)
        {
            UsersRepository = usersRepository;
            MessageRepository = messageRepository;
            GroupUserRepository = groupUserRepository;
            Logger = provider.CreateLogger(nameof(MessageService));
        }
        public async Task InsertMessage(MessageSubmitModel arg)
        {
            try
            {
                GroupUserRepository.DisableLazyLoading();
                var messageGroupUsers = (await GroupUserRepository.GetManyBy(gu => arg.GroupId.Equals(gu.Group.Id) && !gu.Id.Equals(arg.CreatorGroupUserId))).Select(gu => new Models.MessageGroupUser
                {
                    GroupUser = gu,
                    AlreadyReceived = true,//Add logic with logged userIds at signalR chat
                    CreatorName = arg.MessageUI.CreatorName,
                    IsCreator = arg.CreatorGroupUserId == gu.Id,
                    Id = Guid.NewGuid()
                }).ToList();

                Message message = arg.MessageUI.ToActualMessage(messageGroupUsers);

                MessageRepository.InsertEntry(message);

                await MessageRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError("Exception at InsertMessage", ex);
            }            
        }


    }
}

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserDataLayer.Extensions;

namespace UserDataLayer.Services
{
    public class GroupService
    {
        private GroupsRepository GroupsRepository { get; }
        private UsersRepository UsersRepository { get; }

        private string HashSalt { get; set; }
        private ILogger Logger;
        public GroupService(GroupsRepository GroupsRepository, ILoggerProvider loggerProvider, UsersRepository usersRepository)
        {
            this.GroupsRepository = GroupsRepository;
            UsersRepository = usersRepository;
            Logger = loggerProvider.CreateLogger(nameof(GroupService));
        }
        public async Task<(bool, Guid)> InsertGroup(Models.UI.CUD.FirstMessageModel model)
        {
            try
            {
                UsersRepository.DisableLazyLoading();
                Group actualG = model.group.ToActualGroup();
                GroupsRepository.InsertEntry(actualG);
                var users = await UsersRepository.GetManyBy(u => model.group.GroupUsers.Select(gu => gu.Name).Contains(u.UserName));
                actualG.Users = users.CreateGroupUsers(actualG, model.group.MessageUsers.First().RealMsg, model.currentId);
                await GroupsRepository.SaveChangesAsync();
                return (true, actualG.Id);
            }
            catch (Exception e)
            {
                Logger.LogError("Exception at InsertGroup", e);
                return (false, Guid.NewGuid());
            }
        }

        public void Dispose(bool disposeContext)
        {
            GroupsRepository.Dispose(disposeContext);
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}

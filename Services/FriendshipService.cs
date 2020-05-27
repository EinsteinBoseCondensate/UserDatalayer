using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataLayer.Models.UI.CUD;
using UserDataLayer.Repositories;

namespace UserDataLayer.Services
{
    public class FriendshipService
    {
        private bool CreatedGroupOk { get; set; }
        public FriendshipRepository FrienshipRepository { get; set; }
        public UsersRepository UsersRepository { get; set; }
        public ILogger Logger { get; set; }
        public FriendshipService(FriendshipRepository friendshipRepository,
            UsersRepository usersRepository,
            ILoggerProvider LoggerProvider)
        {
            FrienshipRepository = friendshipRepository;
            UsersRepository = usersRepository;
            Logger = LoggerProvider.CreateLogger(nameof(FriendshipService));
        }
        public async Task<bool> CreateFrienship(FrienshipRequestModel model)
        {
            try
            {
                UsersRepository.DisableLazyLoading();
                var guids = new List<Guid> { model.RequestedId, model.RequesterId };
                var users = await UsersRepository.GetManyBy(u => guids.Contains(u.Id));
                FrienshipRepository.InsertEntry(new Models.Friendship
                {
                    Id = Guid.NewGuid(),
                    IsPending = true,
                    RequestedFriend = users.First(u => u.Id == model.RequestedId),
                    RequesterFriend = users.First(u => u.Id == model.RequesterId)
                });
                await FrienshipRepository.SaveChangesAsync();
                CreatedGroupOk = true;
            }
            catch (Exception ex)
            {
                CreatedGroupOk = false;
                Logger.LogError("Exception at CreateFrienship", ex);
            }
            finally
            {
                FrienshipRepository.Dispose();
                UsersRepository.Dispose();                
            }
            return CreatedGroupOk;
        }
    }
}

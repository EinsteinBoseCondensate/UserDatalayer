using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UserDataLayer.Models;
using UserDataLayer.Models.UI.CUD;

namespace UserDataLayer.Repositories
{
    public class FriendshipRepository : ChatsAppRepository<Friendship>
    {
        public FriendshipRepository(UserChatContext context, ILoggerProvider loggerProvider, IConfiguration configuration) : base(context, loggerProvider, configuration)
        {
        }
        //public async Task CreateFriendship(FrienshipRequestModel model)
        //{
        //    await 
        //}
    }
}

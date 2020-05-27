using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserDataLayer.Models;

namespace UserDataLayer.Repositories
{
    public class GroupUserRepository : ChatsAppRepository<GroupUser>
    {
        public GroupUserRepository(UserChatContext context, ILoggerProvider loggerProvider, IConfiguration configuration) : base(context, loggerProvider, configuration)
        {

        }
       
    }
}

using CrossCuttingExtensions.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UserDataLayer.Repositories
{
    public class ChatsAppRepository<T> : GenericRepo<UserChatContext, T> where T : class
    {
        public ChatsAppRepository(UserChatContext context, ILoggerProvider loggerProvider, IConfiguration configuration) : base(context, loggerProvider, configuration)
        {
        }
    }
}

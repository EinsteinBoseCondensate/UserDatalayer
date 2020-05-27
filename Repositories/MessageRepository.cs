using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace UserDataLayer.Repositories
{
    public class MessageRepository : ChatsAppRepository<Message>
    {
        public MessageRepository(UserChatContext context, ILoggerProvider loggerProvider, IConfiguration configuration) : base(context, loggerProvider, configuration)
        {

        }
    }
}

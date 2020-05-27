using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserDataLayer.Repositories;

namespace UserDataLayer
{
    public class GroupsRepository : ChatsAppRepository<Group>
    {
        public GroupsRepository(UserChatContext context, ILoggerProvider loggerProvider, IConfiguration configuration) : base(context, loggerProvider, configuration)
        {
        }

    }
}

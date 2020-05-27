using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using UserDataLayer.Models.DTO;
using UserDataLayer.Repositories;

namespace UserDataLayer
{
    public class UsersRepository : ChatsAppRepository<User>
    {
        public UsersRepository(UserChatContext context, ILoggerProvider loggerProvider, IConfiguration configuration) : base(context, loggerProvider, configuration)
        {
            
        }
        
        public async Task<bool> IsUserUniqueAsync(UserLoginDTO dto) => !await AnyAsync(user => user.Email.ToUpper().Equals(dto.Email.ToUpper()) || user.UserName.ToUpper().Equals(dto.UserName.ToUpper()));
        //public async Task<User> GetUserWithMessagesAndGroups(UserLoginDTO dto)
        //{
        //    User u = (await GetEntitiesByWith(new string[][] { new string[] { "GroupsBridge" }, new string[] { "" } }, e => e.Id == dto.Id, 0, 1)).FirstOrDefault();
        //    return u;
        //}
    }
}

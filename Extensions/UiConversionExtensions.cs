using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDataLayer.Models;

namespace UserDataLayer.Extensions
{
    public static class UiConversionExtensions
    {
        public static ICollection<Models.GroupUser> CreateGroupUsers(this ICollection<User> users, Group group, Models.UI.Message message, Guid currentId)
        {
            User currentU = users.First(u => u.Id == currentId);
            return users.Select(u => {
                Models.GroupUser groupUser = new Models.GroupUser
                {
                    Group = group,
                    Id = Guid.NewGuid(),
                    IsAdmin = false,
                    Member = u,

                };
                var aux = new List<MessageGroupUser>() {new MessageGroupUser
                {
                    Id = Guid.NewGuid(),
                    GroupUser = groupUser,
                    AlreadyReceived = false,
                    CreatorName = currentU.UserName,
                    IsCreator = u.Id == currentId

                } };
                var actualM = message.ToActualMessage(aux);
                aux.ForEach(aux => aux.Message = actualM);
                groupUser.Messages = aux;
                return groupUser;
            }).ToList(); 
        }
       public static Message ToActualMessage(this Models.UI.Message message, ICollection<MessageGroupUser> gus)
        {
            return new Message
            {
                Id = Guid.NewGuid(),
                Content = message.Content,
                Created = DateTime.Now,
                GroupUsers = gus
            };
        }
    }
}

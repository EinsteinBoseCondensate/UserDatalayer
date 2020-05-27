using CrossCuttingExtensions.Configuration;
using CryptographyLayer.HashOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using UserDataLayer.Models.UI;
using UserDataLayer.Repositories;
using UserDataLayer.Services;

namespace UserDataLayer.Extensions
{
    public static class UserExtensions
    {
        public static IServiceCollection ConfigureUserDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserChatContext>(option => option.UseLazyLoadingProxies().UseSqlServer(configuration.Get<ConnectionConfig>().ConnectionStrings.LocalDB));
            services.AddScoped(typeof(UsersRepository));
            services.AddScoped(typeof(UserService));
            services.AddScoped(typeof(GroupsRepository));
            services.AddScoped(typeof(GroupService));
            services.AddScoped(typeof(FriendshipRepository));
            services.AddScoped(typeof(FriendshipService));
            services.AddScoped(typeof(GroupUserRepository));
            services.AddScoped(typeof(MessageRepository));
            services.AddScoped(typeof(MessageService));
            return services;
        }
        public static bool IsPaswordAuthorized(this User user, string password, string hashsalt) => !string.IsNullOrEmpty(password.Trim()) && password.Hash(hashsalt).Equals(user.HashedPassword);
        public static InitialLoad ToInitialLoad(this User user) => new InitialLoad()
        {
            Frienships = user.ExternalFrienships?.Select(ef => new Friendship
            {
                Id = ef.RequesterFriend.Id,
                External = true,
                FriendName = ef.RequesterFriend.UserName,
                Pending = ef.IsPending
            }).ToList()
                .Concat(user.RequestedFrienships?.Select(ef => new Friendship
                {
                    Id = ef.RequestedFriend.Id,
                    External = false,
                    FriendName = ef.RequestedFriend.UserName,
                    Pending = ef.IsPending
                })).ToList(),
            Groups = user.GroupsBridge.Select(gb => gb.Group).Select(g => new Models.UI.Group
            {
                Name = g.Users.Count == 2 ? g.Users.First(u => !u.Member.UserName.Equals(user.UserName)).Member.UserName : g.GroupName,
                Id = g.Id,
                GroupUsers = g.Users.Select(gu => new Models.UI.User { Name = gu.Member.UserName, Id = gu.Member.Id }).ToList(),
                MessageUsers = g.Users.SelectMany(gu => gu.Messages).OrderByDescending(mu => mu.Message.Created).Select(m => new MessageUser
                {
                    RealMsg = new Models.UI.Message
                    {
                        Content = m.Message.Content,
                        Created = m.Message.Created.ToString("HH:mm"),
                        IsMine = m.IsCreator,
                        CreatorName = m.CreatorName

                    },
                    User = new Models.UI.User { Name = m.GroupUser.Member.UserName }
                }).ToList()


            }).ToList()
        };

    }
}



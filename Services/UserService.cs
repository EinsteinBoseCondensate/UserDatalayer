using CrossCuttingExtensions.Extensions;
using CryptographyLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using UserDataLayer.Extensions;
using UserDataLayer.Models.DTO;
using UserDataLayer.Models.UI;

namespace UserDataLayer.Services
{
    public class UserService
    {
        private UsersRepository UserRepository { get; }
        private string HashSalt { get; set; }
        private ILogger Logger;
        private IConfiguration Configuration;
        private readonly int usersChunkSize = 25;
        public UserService(UsersRepository UserRepository, IConfiguration configuration, ILoggerProvider loggerProvider)
        {
            this.UserRepository = UserRepository;
            Configuration = configuration;
            HashSalt = Configuration.Get<CryptoConfig>().CryptographySection.HashSalt;
            Logger = loggerProvider.CreateLogger(nameof(UserService));
        }
        public async Task<string> InsertUser(UserLoginDTO dto)
        {
            try
            {
                if (await UserRepository.IsUserUniqueAsync(dto))
                {
                    UserRepository.InsertEntry(dto.ToNewUser(HashSalt));
                    await UserRepository.SaveChangesAsync();
                }
                else
                {
                    return "ko";
                }
            }
            catch (Exception e)
            {
                Logger.LogError($"Exception at {nameof(UserService)}, method {MethodBase.GetCurrentMethod().Name}", e.Message);
                return "ko";
            }
            return "ok";
        }
        public async Task<(string, InitialLoad)> TryAuthenticateUser(UserLoginDTO dto)
        {
            try
            {
                var realUser = await UserRepository.GetFirstOrDefaultBy(u => u.UserName.Equals(dto.UserName));
                if (realUser == null || !realUser.IsPaswordAuthorized(dto.Password, HashSalt))
                    return ("", null);
                return (realUser.Id.ToString().GenerateToken(Configuration, realUser.UserName), realUser.ToInitialLoad());

            }
            catch (Exception ex)
            {
                Logger.LogError($"Error at {nameof(TryAuthenticateUser)}\n {ex.InnerException.Message}");
                return ("exception", null);
            }
        }
        public async Task<ICollection<UserLoginDTO>> FetchUsers(UsersSearchPaginationModel model)
        {

            var res = await UserRepository.GetManyAndConvertBy(u => u.UserName.Contains(model.NameFragment) && !model.OwnIds.Contains(u.Id) ,
                                                               UserLoginDtoExtensions.FromUserNameAndId(),
                                                               model.SkippingResults,
                                                               usersChunkSize);
            return res;
        }

        public async Task<ICollection<User>> getLos()
        {
            return await UserRepository.GetEntitiesByWith(u => true, 0, 0, 
                u => u.GroupsBridge,
                u => u.ExternalFrienships);

        }
        public void Dispose(bool disposeContext)
        {
            UserRepository.Dispose(disposeContext);
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}

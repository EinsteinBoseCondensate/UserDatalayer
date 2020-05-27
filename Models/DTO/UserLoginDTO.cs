using CryptographyLayer.HashOperations;
using System;

namespace UserDataLayer.Models.DTO
{
    public class UserLoginDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }   
    public static class UserLoginDtoExtensions
    {
        /// <summary>
        /// Method to convert UserLoginDTO to User entity when register event occurs
        /// </summary>
        /// <param name="dto">Basic user info</param>
        /// <param name="hashSalt">Hash salt for HMACSHA512</param>
        /// <returns>New user entity without email confirmed</returns>
        public static User ToNewUser(this UserLoginDTO dto, string hashSalt)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                EmailConfirmed = false,
                HashedPassword = dto.Password.Hash(hashSalt),
                UserName = dto.UserName
            };
        }
        public static System.Linq.Expressions.Expression<Func<User, UserLoginDTO>> FromUserNameAndId()
        {
            return u => new UserLoginDTO
            {
                Id = u.Id,
                UserName = u.UserName
            };
        }
    }
}

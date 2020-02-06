using Microsoft.EntityFrameworkCore;
using OAuthServer.Application;
using OAuthServer.Application.Repository;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OAuthServer.Persistence.Reopsitories
{
    public class UserRepository : IUserRepository
    {
        private readonly OAuthContext _context;

        public UserRepository(OAuthContext context)
        {
            _context = context;
        }

        public async Task<User> LogInUser(string username, string password)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> RegisterUser(User user, string password)
        {
            if (await UserExists(user.Username))
                return null;

            byte[] passwordHash;
            byte[] passwordSalt;

            ComputePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<User> GetUserById(string user_id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.User_Id == user_id);
        }

        private void ComputePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                PasswordSalt = hmac.Key;
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                if (hash.Length != passwordHash.Length)
                    return false;

                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }
    }
}

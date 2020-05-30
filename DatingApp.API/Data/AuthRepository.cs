using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            this._context = context;            
        }


        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null)
            {
                //returning null here, will help controller to return the response type accordingly 401(Unauthorized)
                return null;
            }

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                //returning null here, will help controller to return the response type accordingly 401(Unauthorized)
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                //passwordSalt = hmac.Key;
                //passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var computedhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedhash.Length; i++)
                {
                    if(password[i] != computedhash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordhash, passworldsalt;
            CreatePasswordHash(password, out passwordhash, out passworldsalt);

            user.PasswordHash = passwordhash;
            user.PasswordSalt = passworldsalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordhash, out byte[] passworldsalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passworldsalt = hmac.Key;
                passwordhash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));                
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
           if(await  _context.Users.AnyAsync(x => x.Username == username))
           {
               return true;
           }else
           {
               return false;
           }
        }
    }
}
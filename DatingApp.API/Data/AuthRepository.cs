using System;
using System.Threading.Tasks;
using DatingApp.API.Modules;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
         public DataContext _context { get; }
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            var user  = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if( user == null)
                return null;
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            // now we want to give the key so it can compute the hash.
            using( var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) {
                var computedHash =  hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // we want to check if all elements of byte array matches
                for( int i=0; i < computedHash.Length;i++) {
                    if(computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // gives us a random generated key.
            // we can use this key to unlock our hashed password
            // it is an IDisposible method.
            using( var hmac = new System.Security.Cryptography.HMACSHA512()) {
                passwordSalt = hmac.Key;
                // computerHash needs a byte value.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if( await _context.Users.AnyAsync( x => x.Username == username ) ) {
                return true;
            }

            return false;
        }
    }
}
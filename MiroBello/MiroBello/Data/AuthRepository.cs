using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MiroBello.Models;

namespace MiroBello.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
     

        public async Task<ClientAccount> Login(string email, string password)
        {
            var emailAdress =  _context.ClientAccounts.FirstOrDefault(x => x.Email==email);

            if (emailAdress == null)
                return null;

            if (!VerifyPasswordHash(password, emailAdress.PasswordHash, emailAdress.PasswordSalt))
                return null;

            //auth successful
            return emailAdress;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }


        public async Task<ClientAccount> Register(string firstname, string lastname, string address, string phonenumber, ClientAccount email, string password)
        {
            byte[] passwordHash = new byte[] { };
            byte[]passwordSalt = new byte[] { };
            CreatePasswordHash(password, passwordHash, passwordSalt);

            email.PasswordHash = passwordHash;
            email.PasswordSalt = passwordSalt;

            await _context.ClientAccounts.AddAsync(email);
            await _context.SaveChangesAsync();

            return email;
        }

        private void CreatePasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.ClientAccounts.AnyAsync(x => x.Email == email))
                return true;
            return false;
        }

        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiroBello.Models;

namespace MiroBello.Data
{
    public interface IAuthRepository
    {
        Task<ClientAccount> Register(string firstname, string lastname, string address,string phonenumber, ClientAccount email, string password);

        Task<ClientAccount> Login(string emailAddress, string password);

        Task<bool> UserExists(string emailAddress);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiroBello.Data;
using MiroBello.Models;

namespace MiroBello.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string firstname, string lastname, string address, string phonenumber, string email,string password)
        {
            //validate request

            email=email.ToLower();

            if (await _repo.UserExists(email))
                return BadRequest(("Client is already existing!"));

            var clientToCreate = new ClientAccount
            {
                Email = email
            };
            var createMail = await _repo.Register(firstname,lastname,address,phonenumber,clientToCreate, password);


            return StatusCode(201);
        }

    }
}
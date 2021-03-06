﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiroBello.Data;
using MiroBello.Dtos;
using MiroBello.Models;

namespace MiroBello.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]ClientForRegisterDto clientForRegisterDto)
        {
            clientForRegisterDto.Email = clientForRegisterDto.Email.ToLower();

            if (await _repo.UserExists(clientForRegisterDto.Email))
                ModelState.AddModelError("Email","Client is already existing!");

            //validate request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            var clientToCreate = new ClientAccount
            {
                Email = clientForRegisterDto.Email
            };
            var createMail = await _repo.Register(clientForRegisterDto.FirstName, clientForRegisterDto.LastName, clientForRegisterDto.Address, clientForRegisterDto.PhoneNumber, clientToCreate, clientForRegisterDto.Password);


            return StatusCode(201);
        }
        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]ClientForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Email.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            // generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFromRepo.Email)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString });
        }

    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EacademyApp.API.Data;
using EacademyApp.API.Dtos;
using EacademyApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EacademyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthStudentRepository _studentRepo;
        private readonly IConfiguration _config;
        private readonly DataContext _context;

        public AuthController(IAuthStudentRepository studentRepo, IConfiguration config, DataContext context)
        {
            _config = config;
            _studentRepo = studentRepo;
            _context = context;
        }

        [HttpPost("studentRegister")]
        public async Task<IActionResult> StudentRegister(StudentForRegisterDto userForRegisterDto)
        {
            // validate request

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _studentRepo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");

            Random r = new Random();
            var randomPhotoNumber = r.Next(1, 100);

            var userToCreate = new Student
            {
                Username = userForRegisterDto.Username,
                Name = userForRegisterDto.Name,
                Surname = userForRegisterDto.Surname,
                PhotoURL = "https://randomuser.me/api/portraits/men/" + randomPhotoNumber + ".jpg",
                Created = DateTime.Now,
                EnrollmentDate = DateTime.Now
            };

            var createdUser = await _studentRepo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("studentLogin")]
        public async Task<IActionResult> StudentLogin(StudentForLoginDto userForLoginDto)
        {
            var userFromRepo = await _studentRepo.Login(userForLoginDto.Username, userForLoginDto.Password);

            if(userFromRepo == null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var roles = await _context.Roles.Include(r => r.StudentRoles).Where(r => r.StudentRoles.Any(sr => sr.StudentId == userFromRepo.Id)).ToListAsync();
            
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
        
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
            
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

//Token Specific
using System;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;



//project specific
using DatingApp.API.Data;
using DatingApp.API.Models;
using DatingApp.API.DTOs;



namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        private readonly IConfiguration _configuration;
        public AuthController(IAuthRepository repo, IConfiguration configuration)
        {
            this._repo = repo;
            this._configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            //DONE: Validation the request is now been taken care by the DTOs classes, created in the DTO folder
            
            userForRegisterDTO.Username = userForRegisterDTO.Username.ToLower();

            //check if user exists
            if(await _repo.UserExists(userForRegisterDTO.Username))
            {
                return BadRequest("Username already exist");
            }

            var userToCreate = new User
            {
                Username = userForRegisterDTO.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDTO.Password);

            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            userForLoginDTO.Username = userForLoginDTO.Username.ToLower();

            var userFromRepo = await _repo.Login(userForLoginDTO.Username, userForLoginDTO.Password);

            if(userFromRepo == null)
            {
                return Unauthorized();
            }

            //1. We will create a new claim first, in order to generate a token
            var claim = new []{
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            //2. Since we have a claim and it's properties, we will now try to create a key for it
            //Token's first part will be taken from the appsettings.json property
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));//it will take a key part from configuration file appsettings.json

            //3. Now we have both, claim and key, so we can create a credential holder
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //4. Since we have all the properties required, we can now create a security token descriptor which will hold the information about token expiray and other information
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds                
            };


            //5. Finally, we can now have a token handler part, this handler(JWT) will be responsible to run all the validations
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new{
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}

//Test users
//John Test@123
//James Hello@123
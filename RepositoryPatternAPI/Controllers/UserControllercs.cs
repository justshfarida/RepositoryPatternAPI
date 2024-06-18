    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryPatternApi.Application.DTOs;
    using RepositoryPatternApi.Domain.Entities;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    namespace RepositoryPatternAPI.API.Controllers
    {
        [ApiController]
        [Route("[controller]")]
        public class UserController:ControllerBase
        {
            private readonly IConfiguration _config;
            private readonly UserManager<User> _userManager;

            public UserController(IConfiguration configuration, UserManager<User> userManager) 
            { 
                _config = configuration;
                _userManager = userManager;
            }
            [HttpPost("Login")]
            public async Task<List<string>> Login(UserLoginDTO user)        {
                List<string> result = new List<string>();
                var userExist = await _userManager.FindByEmailAsync(user.Email);//email is unique
                if (userExist != null && await _userManager.CheckPasswordAsync(userExist, user.Password))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, userExist.UserName),
                            new Claim(ClaimTypes.Email, userExist.Email),
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    result.Add(tokenString);

                    return result;
                }
                result.Add("No such user");
                return result;
            }
            [HttpPost("Register")]
            public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
            {
                var userExist = await _userManager.FindByEmailAsync(user.Email);//email is unique
                if (userExist != null)
                {
                    return StatusCode(403);//Forbidden action
                }
                var newUser = new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Id = Guid.NewGuid().ToString() // Example: Generate a new GUID string for Id
                };

                var result = await _userManager.CreateAsync(newUser, user.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok("User registered successfully.");
            }
        }
    }

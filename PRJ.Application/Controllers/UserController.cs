using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PRJ.Common.Entities;
using PRJ.Domain.Entities;
using PRJ.Domain.Interfaces.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PRJ.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, IUserService userService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }


        // T O K E N   

        [HttpPost, AllowAnonymous]
        [Route("Token")]
        public async Task<ApiResult> CreateToken(string email, string password)
        {
            var user = await _userService.AuthenticateAsync(email, password);

            if (user != null)
            {
                var tokenString = BuildToken(user);

                return new ApiResult { 
                    Success = true,
                    Message = "OK",
                    Value = tokenString,
                    StatusCode = HttpStatusCode.OK
                };
            }

            return new ApiResult
            {
                Success = false,
                Message = "Unauthorized",
                Value = null,
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        private string BuildToken(UserEntity user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Name),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate,"1991-05-01"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                expires: expires,
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // T O K E N   



        [HttpGet, Authorize]
        public async Task<ApiResult> GetAll()
        {
            try
            {
                var entities = await _userService.GetAllAsync();

                return new ApiResult
                {
                    Success = true,
                    Message = "OK",
                    Value = entities,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (ArgumentException e)
            {
                return new ApiResult
                {
                    Success = false,
                    Message = e.Message,
                    Value = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpGet, Authorize]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ApiResult> Get(int id)
        {
            try
            {
                var entity = await _userService.GetAsync(id);

                return new ApiResult
                {
                    Success = true,
                    Message = "OK",
                    Value = entity,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (ArgumentException e)
            {
                return new ApiResult
                {
                    Success = false,
                    Message = e.Message,
                    Value = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPost, Authorize]
        public async Task<ApiResult> Create([FromBody] UserEntity user)
        {
            try
            {
                var result = await _userService.CreateAsync(user);
                if (result != null)
                {
                    // return Created(new Uri(Url.Link("GetWithId", new { id = result.Id })), result);
                    return new ApiResult
                    {
                        Success = true,
                        Message = new Uri(Url.Link("GetWithId", new { id = result.Id })).ToString(),
                        Value = result,
                        StatusCode = HttpStatusCode.OK
                    };
                }
                else
                {
                    return new ApiResult
                    {
                        Success = false,
                        Message = "BadRequest",
                        Value = null,
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }
            }
            catch (ArgumentException e)
            {
                return new ApiResult
                {
                    Success = false,
                    Message = e.Message,
                    Value = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpPut, Authorize]
        public async Task<ApiResult> Update([FromBody] UserEntity user)
        {
            try
            {
                var result = await _userService.UpdateAsync(user);
                if (result != null)
                {
                    return new ApiResult
                    {
                        Success = true,
                        Message = "OK",
                        Value = result,
                        StatusCode = HttpStatusCode.OK
                    };
                }
                else
                {
                    return new ApiResult
                    {
                        Success = false,
                        Message = "BadRequest",
                        Value = null,
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }
            }
            catch (ArgumentException e)
            {
                return new ApiResult
                {
                    Success = false,
                    Message = e.Message,
                    Value = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ApiResult> Remove(int id)
        {
            try
            {
                var result =  await _userService.RemoveAsync(id);
                return new ApiResult
                {
                    Success = true,
                    Message = "OK",
                    Value = result,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (ArgumentException e)
            {
                return new ApiResult
                {
                    Success = false,
                    Message = e.Message,
                    Value = null,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}

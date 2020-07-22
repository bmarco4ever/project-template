using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PRJ.Common.Entities;
using PRJ.Domain.Entities;
using PRJ.Domain.Interfaces.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PRJ.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, IUserService userService, ITokenService tokenService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _tokenService = tokenService;
            _configuration = configuration;
        }


        // T O K E N   

        [HttpPost, AllowAnonymous]
        [Route("Token")]
        public async Task<IActionResult> CreateToken(string email, string password)
        {
            var user = await _userService.AuthenticateAsync(email, password);

            if (user != null)
            {
                var token = _tokenService.CreateToken(user);

                return StatusCode((int)HttpStatusCode.OK, new ApiResult
                {
                    Success = true,
                    Message = "OK",
                    Value = token,
                });
            }

            return Unauthorized(new ApiResult
            {
                Success = false,
                Message = "Unauthorized",
                Value = null,
            });
        }

        // T O K E N   



        [HttpGet, Authorize]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var entities = await _userService.GetAllAsync();

                return StatusCode((int)HttpStatusCode.OK, new ApiResult
                {
                    Success = true,
                    Message = "OK",
                    Value = entities,
                });
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResult
                {
                    Success = false,
                    Message = e.Message,
                    Value = null,
                });
            }
        }

        [HttpGet, Authorize]
        [Route("GetId/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var entity = await _userService.GetAsync(id);
                if (entity==null)
                {
                    return StatusCode((int)HttpStatusCode.NotFound, new ApiResult
                    {
                        Success = false,
                        Message = "Not Fount",
                        Value = null,
                    });
                }

                return StatusCode((int)HttpStatusCode.OK, new ApiResult
                {
                    Success = true,
                    Message = "OK",
                    Value = entity,
                });
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResult
                {
                    Success = false,
                    Message = e.Message,
                    Value = null,
                });
            }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create([FromBody] UserEntity user)
        {
            try
            {
                var result = await _userService.CreateAsync(user);
                if (result != null)
                {
                    return StatusCode((int)HttpStatusCode.OK, new ApiResult
                    {
                        Success = true,
                        Message = new Uri(Url.Link("GetId", new { id = result.Id })).ToString(),
                        Value = result,
                    });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ApiResult
                    {
                        Success = false,
                        Message = "BadRequest",
                        Value = null,
                    });
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResult
                {
                    Success = false,
                    Message = e.Message,
                    Value = null,
                });
            }
        }

        [HttpPut, Authorize]
        public async Task<IActionResult> Update([FromBody] UserEntity user)
        {
            try
            {
                var result = await _userService.UpdateAsync(user);
                if (result != null)
                {
                    return StatusCode((int)HttpStatusCode.OK, new ApiResult
                    {
                        Success = true,
                        Message = "OK",
                        Value = result,
                    });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ApiResult
                    {
                        Success = false,
                        Message = "BadRequest",
                        Value = null,
                    });
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResult
                {
                    Success = false,
                    Message = e.Message,
                    Value = null,
                });
            }
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _userService.RemoveAsync(id);
                return StatusCode((int)HttpStatusCode.OK, new ApiResult
                {
                    Success = true,
                    Message = "OK",
                    Value = result,
                });
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResult
                {
                    Success = false,
                    Message = e.Message,
                    Value = null,
                });
            }
        }
    }
}

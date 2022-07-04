using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TesteLuizaLabs.Application.DTO.DTO.Auth;
using TesteLuizaLabs.Application.Exceptions;
using TesteLuizaLabs.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TesteLuizaLabs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IApplicationServiceAuth _applicationServiceAuth;

        public AuthController(IApplicationServiceAuth applicationServiceAuth)
        {
            this._applicationServiceAuth = applicationServiceAuth;
        }
        // POST api/<AuthController>
        [HttpPost]
        public IActionResult Post([FromBody] AuthDTO value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.UserName) || string.IsNullOrEmpty(value.Password))
                    throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.IncompleteCredentials);

                var result = this._applicationServiceAuth.Authenticate(value);

                return StatusCode(result.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.Unauthorized, result);
            }
            catch (Exception ex)
            {
                if (ex is TesteLuizaLabsExceptions e)
                {
                    return StatusCode(e.statusCode, new { type = e.type, message = e.Message });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError,
                        new { type = TesteLuizaLabsExceptions.Type.Error, message = new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.Error).Message });
                }
            }
        }
    }
}

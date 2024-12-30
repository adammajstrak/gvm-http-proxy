using GvmHttpProxy.Extensions;
using GvmHttpProxy.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace GvmHttpProxy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GvmController : ControllerBase
    {
        private readonly GvmService _gvmService;
        private readonly PasswordService _passwordService;

        public GvmController(GvmService gvmService, PasswordService passwordService)
        {
            _gvmService = gvmService;
            _passwordService = passwordService;
        }

        [HttpPost]
        [Authorize]
        [Consumes("application/xml")]
        public IResult ExecuteGvmCommand([FromBody] XElement xmlRequest)
        {
            try
            {
                var authorizationHeader = Request.Headers["Authorization"].ToString();
                var credentials = _passwordService.GetCredentials(authorizationHeader);

                string body = xmlRequest.Serialize();

                var result = _gvmService.ExecuteGvmCommand(credentials.Username!, credentials.Password!, body);

                if (!string.IsNullOrWhiteSpace(result.Error))
                {
                    return Results.Content(result.Error, "application/xml", statusCode: 500);
                }

                return Results.Content(result.Response, "application/xml");
            }
            catch(UnauthorizedAccessException)
            {
                return Results.Content("Token expired", "application/xml", statusCode: 401);
            }
        }
    }
}

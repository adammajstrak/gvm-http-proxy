using GvmHttpProxy.Models;
using GvmHttpProxy.Service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    [Consumes("application/xml")]
    public IActionResult Login([FromBody] Credentials credentials)
    {
        try
        {
            var token = _tokenService.GetToken(credentials);

            return Ok(new Token()
            {
                Bearer = token
            });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}

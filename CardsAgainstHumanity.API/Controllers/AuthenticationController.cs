using CardsAgainstHumanity.API.DataTransferObjects.UserDto;
using CardsAgainstHumanity.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardsAgainstHumanity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase {
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService) {
        _authenticationService = authenticationService;
    }

    [HttpPost("Register")]
    public ActionResult Register([FromBody] CreateUserDto par) {
        _authenticationService.Register(par);
        return Ok();
    }

    [HttpGet("Login")]
    public ActionResult Login([FromQuery] string username, [FromQuery] string password) {
        var token = _authenticationService.Login(username, password);
        return Ok(token);
    }
}

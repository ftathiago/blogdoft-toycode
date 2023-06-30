using GitLeaks.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitLeaks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly AuthSettings _authSettings;

    public LoginController(IConfiguration configuration)
    {
        _authSettings = configuration.GetSection("AuthSettings").Get<AuthSettings>();
    }

    [HttpGet]
    public IActionResult GetLogin(string user, string password)
    {
        var logged = _authSettings.UserName.Equals(user)
            && _authSettings.Password.Equals(password);

        return Ok(new { logged });
    }
}

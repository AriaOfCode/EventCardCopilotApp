using Microsoft.AspNetCore.Mvc;  
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using EventCardCopilotApp.Services;
[Route("auth")]
public class AuthController : Controller
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] string username)
    {
        var user = _userService.GetAllUsers().FirstOrDefault(u => u.Username == username);
        if (user == null)
        {
            TempData["LoginError"] = "Username non valido.";
            return Redirect("/login");
        }

        var principal = _userService.CreateClaims(username);
        await HttpContext.SignInAsync("MyCookieAuth", principal);
        return Redirect("/");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        return Redirect("/login");
    }
}

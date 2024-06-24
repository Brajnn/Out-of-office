using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Out_of_Office.Domain.Interfaces;
using Out_of_Office.Application.Models.Requests;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


[Route("auth")]
public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUserService userService, ILogger<AuthController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet("login")]
    [AllowAnonymous] 
    public IActionResult Login(string returnUrl = null)
    {
        _logger.LogInformation("Accessing Login page");
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost("login")]
    [AllowAnonymous] 
    public async Task<IActionResult> Login(LoginRequest request, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Login model state is invalid");
            return View(request);
        }

        var user = await _userService.AuthenticateAsync(request.Username, request.Password);
        if (user == null)
        {
            _logger.LogWarning("Invalid login attempt for user {Username}", request.Username);
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(request);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        _logger.LogInformation("User {Username} logged in successfully", request.Username);

        if (string.IsNullOrEmpty(returnUrl))
        {
            return RedirectToAction("Index", "Home");
        }

        return LocalRedirect(returnUrl);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        _logger.LogInformation("User {Username} logging out", User.Identity.Name);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}




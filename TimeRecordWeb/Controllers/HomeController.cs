using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using TimeRecordWeb.Helpers;
using TimeRecordWeb.Models;

namespace TimeRecordWeb.Controllers
{
    public class HomeController : Controller
    {
        private IUserAPIClient _userAPIClient;

        public HomeController(IUserAPIClient userAPIClient)
        {
            _userAPIClient = userAPIClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                var usertoken = _userAPIClient.GetToken(user);
                if (!string.IsNullOrWhiteSpace(usertoken))
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("AcessToken", usertoken)
                    };

                    var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                    await HttpContext.SignInAsync(userPrincipal);

                    return RedirectToAction("Index", "TimeRecord");
                }

                ModelState.AddModelError(string.Empty, "Username or password is incorrect.");
            }

            return View("Index", user);
        }

        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

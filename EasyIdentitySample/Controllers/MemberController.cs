using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using EasyIdentitySample.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyIdentitySample.Controllers
{
    public class MemberController : Controller
    {
        private readonly IHttpContextAccessor _accessor;

        public MemberController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MemberViewModel request,string returnUrl)
        {
            if (request.Account == "Test" && request.Password == "123")
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, request.Account),
                    new Claim(ClaimTypes.Role, "Admin"),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await _accessor.HttpContext.SignInAsync(claimsPrincipal);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await _accessor.HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
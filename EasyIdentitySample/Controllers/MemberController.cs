using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using EasyIdentitySample.ViewModel;
using Microsoft.AspNetCore.Authentication;
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
        public async Task<IActionResult> Index(MemberViewModel request)
        {
            if (request.Account == "Test" && request.Password == "123")
            {
                var claimsIdentity = new GenericIdentity(ClaimTypes.Name,request.Account);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await _accessor.HttpContext.SignInAsync(claimsPrincipal);
                
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
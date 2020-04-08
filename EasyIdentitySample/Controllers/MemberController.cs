using EasyIdentitySample.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EasyIdentitySample.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MemberViewModel request)
        {
            if (request.Account == "Test" && request.Password == "123")
            {
                // TODO SignIn
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult SignOut()
        {
            // TODO SignOut
            return RedirectToAction("Index");
        }
    }
}
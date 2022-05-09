using core.GoogleCaptchaV2.Models;
using core.GoogleCaptchaV2.Utility;
using Microsoft.AspNetCore.Mvc;

namespace core.GoogleCaptchaV2.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly ReCaptcha _captcha;

        public AuthenticateController(ReCaptcha captcha)
        {
            _captcha = captcha;
        }

        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (!Request.Form.ContainsKey("g-recaptcha-response"))
            {
                return View(model);
            }
            var captcha = Request.Form["g-recaptcha-response"].ToString();
            if (!await _captcha.IsValid(captcha))
            {
                return View();
            }
            ViewBag.message = "login successfull!";
            return View();
        }
    }
}

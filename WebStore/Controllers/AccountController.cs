using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Identity;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        #region Registration

        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model) {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User
            {
                UserName = model.UserName,
            };

            var registration_result = await _userManager.CreateAsync(user, model.Password);

            if (registration_result.Succeeded) {
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in registration_result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        #endregion

        #region Login

        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel() { ReturnUrl = ReturnUrl});

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel) {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var login_result = await _signInManager.PasswordSignInAsync(
                    loginViewModel.UserName,
                    loginViewModel.Password,
                    loginViewModel.RememberMe,
#if DEBUG
                    false
#else
                    true
#endif
                );

            if (login_result.Succeeded) {
                if (Url.IsLocalUrl(loginViewModel.ReturnUrl))
                    return Redirect(loginViewModel.ReturnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");

            return View(loginViewModel);
        }

        #endregion

        #region Logout
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        public IActionResult AccessDenied() => View();
    }
}

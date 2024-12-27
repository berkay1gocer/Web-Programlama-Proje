using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using odev3.Data;
using odev3.Models;
using System.Threading.Tasks;

namespace odev3.Controllers
{
    public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		// Giriş Sayfası (GET)
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		// Giriş Sayfası (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.Username);
				if (user != null)
				{
					var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}
				}

				ModelState.AddModelError(string.Empty, "Geçersiz giriş.");
			}

			return View(model);
		}

		// Kayıt Sayfası (GET)
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		// Kayıt Sayfası (POST)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToAction("Index", "Home");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}

		// Çıkış Yapma
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}

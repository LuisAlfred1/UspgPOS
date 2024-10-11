using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UspgPOS.Data;
using UspgPOS.Models;

namespace UspgPOS.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            AppDbContext context)
        {

            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                ViewData["Layout"] = "_LayoutLogged";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Layout"] = "_Layout";
            }


            ViewBag.Sucursales = _context.Sucursales.ToList();
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.Usuario);
				if (user != null)
				{
					var result = await _signInManager.PasswordSignInAsync(model.Usuario!, model.Password!, false, false);
					if (result.Succeeded)
					{
						HttpContext.Session.SetString("SucursalId", model.SucursalId.ToString());
						ViewData["Layout"] = "_LayoutLogged";
						return RedirectToAction("Index", "Home");
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Contraseña incorrecta.");
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "El usuario no existe.");
				}
			}

			ViewBag.Sucursales = _context.Sucursales.ToList();
			return View(model);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = model.Usuario,
					Email = model.Email,
					Nombre = model.Nombre,
					Rol = model.Rol // Asegúrate de que el modelo contenga esta propiedad
				};

				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, model.Rol);

					return RedirectToAction("Login", "Auth");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Auth");
		}
	}
}

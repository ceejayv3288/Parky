using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkyWeb.Models;
using ParkyWeb.Models.ViewModels;
using ParkyWeb.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly ITrailRepository _trailRepository;
        private readonly IAccountRepository _accountRepository;

        public HomeController(ILogger<HomeController> logger, INationalParkRepository nationalParkRepository, ITrailRepository trailRepository, IAccountRepository accountRepository)
        {
            _logger = logger;
            _nationalParkRepository = nationalParkRepository;
            _trailRepository = trailRepository;
            _accountRepository = accountRepository;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel listOfParksAndTrails = new IndexViewModel()
            {
                NationalParkList = await _nationalParkRepository.GetAllAsync(StaticDetails.NationalParkAPIPath, HttpContext.Session.GetString("JWToken")),
                TrailList = await _trailRepository.GetAllAsync(StaticDetails.TrailAPIPath, HttpContext.Session.GetString("JWToken"))
            };
            return View(listOfParksAndTrails);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            UserModel obj = new UserModel();
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(UserModel obj)
        {
            UserModel userObj = await _accountRepository.LoginAsync(StaticDetails.AccountAPIPath + "authenticate/", obj);
            if (userObj.Token == null)
                return View();

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, userObj.Username));
            identity.AddClaim(new Claim(ClaimTypes.Role, userObj.Role));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            HttpContext.Session.SetString("JWToken", userObj.Token);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(UserModel obj)
        {
            bool result = await _accountRepository.RegisterAsync(StaticDetails.AccountAPIPath + "register/", obj);
            if (!result)
                return View();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> LogoutAsync(UserModel obj)
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("JWToken", string.Empty);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

using AutoMapper;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Domain.Contracts.Models;
using UserIdentity.Domain.Contracts.Services;
using UserIdentity.Web.Models;

namespace UserIdentity.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService,
            IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            returnUrl = "https://github.com/";
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var model = _mapper.Map<SignInUserModel>(viewModel);

            var isUserExisted = await _authService.SingInAsync(model);

            return isUserExisted ? Redirect(viewModel.ReturnUrl) : View(viewModel);

            //if (!ModelState.IsValid)
            //{
            //    return View(viewModel);
            //}

            //var user = await _userManager.FindByEmailAsync(viewModel.Email);

            //if (user == null)
            //{
            //    ModelState.AddModelError(string.Empty, "User not found");
            //    return View(viewModel);
            //}

            //var result = await _signInManager.PasswordSignInAsync(viewModel.Email,
            //    viewModel.Password, false, false);

            //if (result.Succeeded)
            //{
            //    return Redirect(viewModel.ReturnUrl);
            //}

            //ViewBag.AlertMessage = "Invalid data. Please try again.";

            //return View(viewModel);
        }

        //[HttpGet]
        //public IActionResult Register(string returnUrl)
        //{
        //    var viewModel = new RegisterViewModel
        //    {
        //        ReturnUrl = returnUrl
        //    };
        //    return View(viewModel);

        //}
        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel viewModel)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(viewModel);
        //    }

        //    var user = new IdentityUser
        //    {
        //        UserName = viewModel.Email,
        //        Email = viewModel.Email
        //    };
        //    var result = await _userManager.CreateAsync(user, viewModel.Password);

        //    if (result.Succeeded)
        //    {
        //        if (!await _roleManager.RoleExistsAsync("User"))
        //        {
        //            var role = new IdentityRole("User");
        //            await _roleManager.CreateAsync(role);
        //        }

        //        await _userManager.AddToRoleAsync(user, "User");

        //        await _signInManager.SignInAsync(user, false);

        //        return Redirect(viewModel.ReturnUrl);
        //    }

        //    ViewBag.AlertMessage = "Invalid data. Please try again.";

        //    return View(viewModel);
        //}
        //[HttpGet]
        //public async Task<IActionResult> Logout(string logoutId)
        //{
        //    await _signInManager.SignOutAsync();
        //    var logoutRequest = await _interaction.GetLogoutContextAsync(logoutId);
        //    return Redirect(logoutRequest.PostLogoutRedirectUri);
        //}
    }
}

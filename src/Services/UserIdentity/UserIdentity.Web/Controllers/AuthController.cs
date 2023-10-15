using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UserIdentity.Infrastructure.Settings;
using UserIdentity.Web.Models;
using UserIdentity.Domain.Contracts.Models;
using UserIdentity.Domain.Contracts.Services;

namespace UserIdentity.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ReturnUrlsSettings _returnUrlsSettings;

        public AuthController(
            IAuthService authService,
            IMapper mapper,
            IOptions<ReturnUrlsSettings> returnUrlsSettings)
        {
            _authService = authService;
            _mapper = mapper;
            _returnUrlsSettings = returnUrlsSettings.Value;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = _returnUrlsSettings.WebUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var model = _mapper.Map<SignInUserModel>(viewModel);

            var result = await _authService.SignInAsync(model);

            _mapper.Map(result.Data, viewModel);
            result.ValidationResult.AddToModelState(ModelState);
            
            return result.IsValid
                ? Redirect(viewModel.ReturnUrl)
                : View(viewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = _returnUrlsSettings.LoginPageUrl
            };
            
            return View(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            var model = _mapper.Map<SignUpUserModel>(viewModel);

            var result = await _authService.SignUpAsync(model);
            
            _mapper.Map(result.Data, viewModel);
            result.ValidationResult.AddToModelState(ModelState);
            
            return result.IsValid
                ? Redirect(viewModel.ReturnUrl)
                : View(viewModel);
        }
    }
}

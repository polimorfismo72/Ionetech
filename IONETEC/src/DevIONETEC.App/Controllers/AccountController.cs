using AutoMapper;
using DevIONETEC.App.ViewModels;
using DevIONETEC.Business.Intefaces;
using DevIONETEC.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevIONETEC.App.Controllers
{
   public class AccountController : Controller
   {
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager;
        //public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}

        #region VARIAVEIS
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly IUserStore<IdentityUser> _userStore;
        //private readonly IUserEmailStore<IdentityUser> _emailStore;
        //private readonly ILogger<RegisterViewModel> _logger;
        //private readonly IEmailSender _emailSender;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<IdentityUser> userManager,
            //IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            //ILogger<RegisterViewModel> logger,
            //IEmailSender emailSender,
            IVendedorRepository vendedorRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            //_userStore = userStore;
            //_emailStore = GetEmailStore();
            _signInManager = signInManager;
            //_logger = logger;
            //_emailSender = emailSender;
            _vendedorRepository = vendedorRepository;
            _mapper = mapper;
        }
        #endregion

        #region MÉTODO PARA REGISTAR
        
        [Route("novo-registo")]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-registo")]
        public async Task<IActionResult> Registro(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            VendedorViewModel vendedorViewModel = new()
            {
                Nome = registerViewModel.Email,
                Email = registerViewModel.Email,
                Ativo = false
            };

            returnUrl ??= Url.Content("~/");
            registerViewModel.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    // Inserir o Vendedor na Base de Dados
                    var vendedor = _mapper.Map<Vendedor>(vendedorViewModel);
                    await _vendedorRepository.Adicionar(vendedor);

                    //return LocalRedirect(returnUrl);
                    return RedirectToAction("Index", "Home", returnUrl);
               
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerViewModel);
        }
        
        #endregion

        #region MÉTODO PARA LOGIN

        [Route("entrar")]
        public async Task<IActionResult> Login(string returnUrl = null)
         {
             LoginViewModel loginViewModel = new();
             returnUrl ??= Url.Content("~/");
             loginViewModel.ReturnUrl = returnUrl;
               await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
             return View();
         }

         [HttpPost]
         [Route("entrar")]
         public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
         {
             returnUrl ??= Url.Content("~/");
             if (ModelState.IsValid)
             {

                 var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false);
                 if (result.Succeeded)
                 {
                     //return RedirectToAction("Index", "Home");
                     return LocalRedirect(returnUrl);
                 }
                 ModelState.AddModelError(string.Empty, "Login Inválido");
             }

             return View(loginViewModel);
         }
         
        #endregion
        
        /*
        public async Task Login2(string returnUrl = null)
        {
            LoginViewModel loginViewModel = new();
            if (!string.IsNullOrEmpty(loginViewModel.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, loginViewModel.ErrorMessage);
            }
            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            loginViewModel.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            loginViewModel.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> Login2(LoginViewModel loginViewModel,string returnUrl = null)
        {
            //LoginViewModel loginViewModel = new();
            returnUrl ??= Url.Content("~/");

            loginViewModel.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, loginViewModel.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
        */
       
        #region MÉTODO PARA LOGOUT
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}

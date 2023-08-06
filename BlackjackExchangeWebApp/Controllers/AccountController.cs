using BlackjackExchangeWebApp.Interfaces;
using BlackjackExchangeWebApp.Models;
using BlackjackExchangeWebApp.Models.AccountModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlackjackExchangeWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDatabaseService _dbService;

        public AccountController(ILogger logger, IDatabaseService dbService)
        { _logger = logger; _dbService = dbService; }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        public IActionResult LoginRequest(LoginModel loginModel)
        {
            LoginUserModel userCredentials = loginModel.UserModel;

            _logger.Log(LogLevel.Information, $"Logging: {userCredentials}");

            loginModel.Response = "No Such Username";
            bool usernameExists = _dbService.UsernameExists(userCredentials.Username);
            _logger.Log(LogLevel.Debug, $"Checking UsernameExists... {usernameExists}");
            if (!usernameExists)
                return View("Login", loginModel);

            loginModel.Response = "Incorrect Password";
            bool usernamePasswordPairExists = _dbService.UsernamePasswordPairExists(userCredentials.Username, userCredentials.Password);
            _logger.Log(LogLevel.Debug, $"Checking UsernamePasswordPairExists... {usernamePasswordPairExists}");
            if (!usernamePasswordPairExists)
                return View("Login", loginModel);

            return View("Index");
        }

        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        public IActionResult RegisterRequest(RegisterModel registerModel)
        {
            RegisterUserModel userCredentials = registerModel.UserModel;

            _logger.Log(LogLevel.Information, $"Registering: {userCredentials}");

            registerModel.Response = "Username Already Exists";
            bool usernameExists = _dbService.UsernameExists(userCredentials.Username);
            _logger.Log(LogLevel.Debug, $"Checking UsernameExists... {usernameExists}");
            if (usernameExists)
                return View("Register", registerModel);

            registerModel.Response = "Email Already Exists";
            bool emailExists = _dbService.EmailExists(userCredentials.Email);
            _logger.Log(LogLevel.Debug, $"Checking EmailExists... {emailExists}");
            if (emailExists)
                return View("Register", registerModel);

            _dbService.InsertUser(userCredentials.Username, userCredentials.Password, userCredentials.Email);
            return View("Login", new LoginModel() { Response = "Successfully Registered!"});
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
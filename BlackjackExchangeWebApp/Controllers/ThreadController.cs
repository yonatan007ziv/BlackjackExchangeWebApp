using BlackjackExchangeWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlackjackExchangeWebApp.Controllers
{
    public class ThreadController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDatabaseService _dbService;

        public ThreadController(ILogger logger, IDatabaseService dbService)
        { _logger = logger; _dbService = dbService; }

        public IActionResult Index()
        {
            return View(_dbService.GetThreadsList());
        }

        public IActionResult GetThread()
        {
            _logger.LogInformation($"Method: {Request.Method}\nPath: {Request.Path}");
            return View();
        }
    }
}

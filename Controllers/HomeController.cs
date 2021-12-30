using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmotionController.Models;

namespace EmotionController.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [Route("/api/calc/emotion")]
        public IActionResult CalculateEmotion(EmotionData emotions) {
            for (int i = 0; i < emotions.Emotions.Count; i++)
                Emotion emot = emotions.Emotions[i];
                Console.WriteLine($"angry {emot.angry}");
                Console.WriteLine($"sad {emot.sad}");
                Console.WriteLine($"happy {emot.happy}");
                Console.WriteLine($"neutral {emot.neutral}");
            }
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

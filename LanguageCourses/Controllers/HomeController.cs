using LanguageCourses.Helpers;
using LanguageCourses.Interfaces;
using LanguageCourses.Models;
using LanguageCourses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace LanguageCourses.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseRepository _courseRepository;
        private readonly IOptions<IpInfoSettings> _options;

        public HomeController(ILogger<HomeController> logger, ICourseRepository courseRepository, IOptions<IpInfoSettings> options)
        {
            _logger = logger;
            _courseRepository = courseRepository;
            _options = options;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IpInfo();
            var homeViewModel = new HomeViewModel();

            try
            {
                IpInfoSettings settings = new IpInfoSettings
                {
                    Token = _options.Value.Token
                };
                string url = settings.Token;
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo regInfo = new RegionInfo(ipInfo.Country);
                ipInfo.Country = regInfo.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region;

                if (homeViewModel != null)
                {
                    homeViewModel.Courses = await _courseRepository.GetCourseByCity(homeViewModel.City);
                }
                else
                {
                    homeViewModel.Courses = null;
                }
                return View(homeViewModel);
            }
            catch (Exception ex)
            {
                homeViewModel.Courses = null;
            }
            return View(homeViewModel);
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
    }
}
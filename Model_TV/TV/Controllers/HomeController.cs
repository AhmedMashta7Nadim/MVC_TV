using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model_TV.Models;
using Model_TV.VM;
using System.Diagnostics;
using TV.Data;
using TV.Models;
using TV.Repositry.Serves;

namespace TV.Controllers
{
    [Authorize(Roles ="User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositryAllModel<TV_Show, TV_ShowSummary> repositry;
        private readonly IStringLocalizer<HomeController> localizer;

        public HomeController(ILogger<HomeController> logger,
            IRepositryAllModel<TV_Show,TV_ShowSummary> repositry,
            IStringLocalizer<HomeController> localizer
            )
        {
            _logger = logger;

            this.repositry = repositry;
            this.localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }
        public void languagesh()
        {
            ViewBag.alert = localizer["alert"];
            ViewBag.tvwibsite = localizer["tvwibsite"];
            ViewBag.title = localizer["Title"];
            ViewBag.ReleaseDate = localizer["ReleaseDate"];
            ViewBag.Rating = localizer["Rating"];
            ViewBag.VisitShow = localizer["VisitShow"];
            ViewBag.TV = localizer["TV"];
        }

        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Show(int p=1)
        {
            languagesh();
            return View(await repositry.GetAllAsyncP(p));//GetAllAsyncP Â«œ Œ«’ ··»Ã‰Ì‘‰
        }
        
    }
}

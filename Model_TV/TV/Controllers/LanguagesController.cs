using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Model_TV.Models;
using Model_TV.VM;
using TV.Data;
using TV.Repositry.Serves;

namespace TV.Controllers
{
    [Authorize(Roles = "User")]
    public class LanguagesController : Controller
    {
        private readonly IStringLocalizer<LanguagesController> stringLocalizer;
        private readonly IRepositryAllModel<Languages, LanguagesSummary> repositry;

        public LanguagesController(
            IStringLocalizer<LanguagesController> localizer,
            IRepositryAllModel<Languages, LanguagesSummary> repositry
            )
        {
            this.stringLocalizer = localizer;
            this.repositry = repositry;
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.languages = await repositry.GetAllTAsync();
            return View(await repositry.GetAllTAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languages = await repositry.DetailsAsync(id);
            if (languages == null)
            {
                return NotFound();
            }

            return View(languages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] Languages languages)
        {
            if (ModelState.IsValid)
            {
               await repositry.Add(languages);
                return RedirectToAction(nameof(Index));
            }
            return View(languages);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languages = await repositry.GetById((Guid)id);

            if (languages == null)
            {
                return NotFound();
            }
            return View(languages);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] Languages languages)
        {
            if (id != languages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
               var put=   await  repositry.Puts(id,languages);
                if (put == null)
                {
                    return View();
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(languages);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var languages = await repositry.GetByIdT((Guid)id);
            if (languages == null)
            {
                return NotFound();
            }

            return View(languages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var languages = await repositry.DeleteAsync(id);
            if (languages == null)
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }


       
    }
}

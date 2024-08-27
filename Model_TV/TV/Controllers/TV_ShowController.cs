using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model_TV.Models;
using Model_TV.VM;
using NuGet.Protocol.Core.Types;
using TV.Repositry.RepoModels;
using TV.Repositry.Serves;

namespace TV.Controllers
{
    [Authorize(Roles = "User")]
    public class TV_ShowController : Controller
    {
        private readonly IRepositryAllModel<TV_Show, TV_ShowSummary> repositry;
        private readonly IRepositryAllModel<Model_TV.Models.Attachment, AttachmentSummary> attachmentrepositry;
        private readonly IRepositryAllModel<Languages, LanguagesSummary> languagesrepositry;
        private readonly IRepositryAllModel<TV_ShowLanguages, TV_ShowLanguagesSummary> tv_Languash;
        private readonly IMapper mapper;

        public TV_ShowController(
            IRepositryAllModel<TV_Show, TV_ShowSummary> repositry,
            IRepositryAllModel<Model_TV.Models.Attachment, AttachmentSummary> Attachmentrepositry,
            IRepositryAllModel<Model_TV.Models.Languages, LanguagesSummary> Languagesrepositry,
            IRepositryAllModel<Model_TV.Models.TV_ShowLanguages, TV_ShowLanguagesSummary> tv_languash,

            IMapper mapper
            )

        {
            this.repositry = repositry;
            attachmentrepositry = Attachmentrepositry;
            languagesrepositry = Languagesrepositry;
            tv_Languash = tv_languash;
            this.mapper = mapper;
        }

        // GET: TV_Show
        public async Task<IActionResult> Index()
        {
            var result = await repositry.GetAllAsync();
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> IndexAll()
        {
            
            var result = await repositry
                              .GetAllTAsync();

            return View(result);
        }

        // GET: TV_Show/Details/7
        public async Task<IActionResult> Details(Guid? id)
        {
            var details = await repositry.DetailsAsync(id);
            if (details == null)
            {
                return NotFound();
            }

            return View(details);
        }
        private void unloadeAsync()
        {
            var attachments = attachmentrepositry.GetAllAsync().Result;
            //var x = languagesrepositry.GetAllAsync().Result;

            if (attachments == null)
            {
                attachments = new List<AttachmentSummary>();
                //x = new List<LanguagesSummary>();
            }
            //هنا يتم ارسال جميع ال attacments
            //الموجودة لدي على قاعدة البيانات الى صفحة ال view
            //الخاصة بالعرض على شكل ليستا ويتم اظاهار الاسم وند ارسال الطلب يتم ارساال ال guid 
            //الى الكونترولر


            ViewBag.lst = new SelectList(attachments, "Id", "Name");
            //ViewBag.languesh = new SelectList(x, "Id", "Name");
        }

        // GET: TV_Show/Create
        public async Task<IActionResult> Create()
        {

            unloadeAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TV_Show tv_Show, List<Guid> selectlanguash)
        {
            if (ModelState.IsValid)
            {
                //ترجيع البيانات شات اللغات كلا 
                //وبعد هيك من طريق دالة 
                //select 
                //برجع بس الايديات 
                var exist = 
                    (await languagesrepositry
                    .GetAllTAsync())
                    .Select(l => l.Id)
                    .ToList();

                // بهاد الكود بشوف شو البيانات الموجودة بقاعدة البيانات وبرجع اللي ما موجود 
                //بحيث ما يرجعلي قيم متشابهة ويرمي اكسبشن
                var invalidLanguageIds = selectlanguash.Except(exist).ToList();

                if (invalidLanguageIds.Any())
                {
                
                    ModelState.AddModelError("", "the languagesh No Is Data Base");
                    return View(tv_Show);
                }

                var addedShow = await repositry.Add(tv_Show);

                if (selectlanguash != null && selectlanguash.Any())//any يعني انو المصفوفة فاضية وبترجع bool
                {
                    foreach (var ee in selectlanguash)
                    {
                        // هون عنا اوبجكت من جدول كسر العلاقة بنعبي بقلبو قيمتين ايدي العرض , وايدي اللغة
                        var tvShowLanguage = new TV_ShowLanguages
                        {
                            TV_ShowId = addedShow.Id,
                            LanguagesId = ee
                        };
                        await tv_Languash.Add(tvShowLanguage); 
                    }
                }

                return RedirectToAction("Show", "Home");
            }
            return View(tv_Show);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            unloadeAsync();

            var tV_Show = await repositry.GetById((Guid)id);
            if (tV_Show == null)
            {
                return NotFound();
            }
            var mapping = mapper.Map<TV_Show>(tV_Show);
            return View(mapping);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,ReleaseDate,Rating,URL,Id,AttachmentId")] TV_Show tV_Show)
        {
            if (id != tV_Show.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var puts = await repositry.Puts(id, tV_Show);


                return RedirectToAction(nameof(Index));
            }
            return View(tV_Show);
        }

        // GET: TV_Show/Delete/7
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tV_Show = await repositry.GetById((Guid)id);

            if (tV_Show == null)
            {
                return NotFound();
            }

            return View(tV_Show);
        }

        // POST: TV_Show/Delete/7
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tV_Show = await repositry.DeleteAsync(id);

            return RedirectToAction("Show", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var get = await repositry.DeletedSoft(id);
            if (get == null)
            {
                return NotFound();
            }
            return RedirectToAction("Show", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {


            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(Guid id)
        {

            var x = await repositry.GetByIdT(id);
            if (x == null)
            {
                return NotFound();
            }
            return View();
        }




   





    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model_TV.Creation;
using Model_TV.Models;
using Model_TV.VM;
using Newtonsoft.Json.Linq;
using TV.Data;
using TV.Repositry.RepoModels;
using TV.Repositry.Serves;

namespace TV.Controllers
{
    [Authorize(Roles ="User")]
    public class AttachmentsController : Controller
    {
        private readonly IMapper mapper;
        private readonly IRepositryAllModel<Attachment, AttachmentSummary> repositry;
        private readonly RepoAttachment Repoattachment;

        public AttachmentsController(
            IRepositryAllModel<Attachment, AttachmentSummary> repositry,
            RepoAttachment Repoattachment,
            IMapper mapper
            )
        {
            this.mapper = mapper;
            this.repositry = repositry;
            this.Repoattachment = Repoattachment;
        }

        // GET: Attachments
        public async Task<IActionResult> Index()
        {
            return View(await repositry.GetAllAsync());
        }

        // GET: Attachments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            var details = await repositry.DetailsAsync(id);
            if (details == null)
            {
                return NotFound();
            }
            return View(details);
        }

        // GET: Attachments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,File,Id")] AttachmentCreation attachment)
        {
            var mappings = mapper.Map<Attachment>(attachment);
            if (ModelState.IsValid)
            {
                var added = await Repoattachment.Add(mappings);
                if (added == null)
                {
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(attachment);
        }

        // GET: Attachments/Edit/7
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await repositry.GetById((Guid)id);
            if (attachment == null)
            {
                return NotFound();
            }
            var mapping = mapper.Map<AttachmentCreation>(attachment);
            return View(mapping);
        }

        // POST: Attachments/Edit/7
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,File,Id")] AttachmentCreation attachment)
        {
     
            if (ModelState.IsValid)
            {
                var mapping = mapper.Map<Attachment>(attachment);
                
              var x= await Repoattachment.Puts(id, mapping);

                return RedirectToAction(nameof(Index));
            }
            return View(attachment);
        }

        // GET: Attachments/Delete/7
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachment = await repositry.DetailsAsync(id);

            if (attachment == null)
            {
                return NotFound();
            }

            return View(attachment);
        }

        // POST: Attachments/Delete/7
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var attachment = await repositry.DeleteAsync(id);
            if (attachment == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

   
    }
}

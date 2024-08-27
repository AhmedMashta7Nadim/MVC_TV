using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualBasic.FileIO;
using Model_TV.Creation;
using Model_TV.Models;
using Model_TV.VM;
using System.Linq;
using TV.Data;

namespace TV.Repositry.RepoModels
{
    public class RepoAttachment : RepositryAllModel<Attachment, AttachmentSummary>
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RepoAttachment(IMapper mapper, ApplicationDbContext context,IWebHostEnvironment webHostEnvironment
            ) : base(mapper, context)
        {
            this.mapper = mapper;
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public override async Task<Attachment> Add(Attachment value)
        {
            if (value.File != null && value.File.Length > 0)
            {
                var fileName = Path.GetFileName(value.File.FileName);
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads\\Attachment", fileName);
                if (!AccessFile(fileName))
                {
                    return null;
                }
                if (!Directory.Exists(Path.Combine(webHostEnvironment.WebRootPath, "uploads\\Attachment")))
                {
                    Directory.CreateDirectory(Path.Combine(webHostEnvironment.WebRootPath, "uploads\\Attachment"));
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    value.File.CopyToAsync(stream);
                }

                value.Path_File = $"uploads/Attachment/{fileName}";
                value.FileType = Path.GetExtension(fileName).TrimStart('.');
            }
            context.Attachment.Add(value);
            context.SaveChanges();
            return value;
        }

        public override async Task<Attachment> Puts(Guid id, Attachment value)
        {
            var get = context.Attachment.FirstOrDefault(x => x.Id == id);
            if (value.File != null && value.File.Length > 0)
            {
                var fileName = Path.GetFileName(value.File.FileName);
                var filePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads\\Attachment", fileName);

                if (!Directory.Exists(Path.Combine(webHostEnvironment.WebRootPath, "uploads\\Attachment")))
                {
                    Directory.CreateDirectory(Path.Combine(webHostEnvironment.WebRootPath, "uploads\\Attachment"));
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    value.File.CopyToAsync(stream);
                }

                get.Path_File = $"uploads/Attachment/{fileName}";
                get.FileType = Path.GetExtension(fileName).TrimStart('.');
            }
            get.Name = value.Name;
        

            context.Attachment.Update(get);
            await context.SaveChangesAsync();

            return value;
        }


        public bool AccessFile(string file)
        {
            var split=file.Split('.');
            string[] arr = { "png" ,"jpg" };
            var exist = arr.FirstOrDefault(x => x.Contains(split[1]));
            if (exist != null)
            {
                return true;
            }
            return false;
        }
    }
}

using Models_TV.Model.Entity_programing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_TV.Models
{
    public class TV_Show : Entity_Id
    {
        public required string Title { get; set; } = string.Empty;
        public required DateTime ReleaseDate { get; set; }
        public required string Rating { get; set; } = string.Empty;//التصنيف
        public required string URL { get; set; } = string.Empty;//رابط البرنامج 
        public Guid AttachmentId { get; set; }
        public Attachment? Attachment { get; set; }
        public List<Languages> languages { get; set; } = new List<Languages>();
        public List<TV_ShowLanguages> tv_languages { get; set; } = new List<TV_ShowLanguages>();
    }
    public class TV_ShowLanguagesViewModel
    {
        public Guid TV_ShowId { get; set; }
        public List<Guid> selectlanguash { get; set; } = new List<Guid>();
    }
}

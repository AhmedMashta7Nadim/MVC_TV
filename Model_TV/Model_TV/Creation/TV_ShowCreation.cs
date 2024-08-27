using Model_TV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_TV.Creation
{
    public class TV_ShowCreation
    {
        public required string Title { get; set; }
        public required DateTime ReleaseDate { get; set; }
        public required string Rating { get; set; }  //التصنيف
        public required string URL { get; set; }//رابط البرنامج 
        public Guid AttachmentId { get; set; }
        public Attachment? Attachment { get; set; }

    }
}

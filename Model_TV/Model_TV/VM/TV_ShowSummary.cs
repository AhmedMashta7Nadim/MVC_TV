using Model_TV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_TV.VM
{
    public class TV_ShowSummary
    {
        public Guid Id { get; set; }
        public  string Title { get; set; }
        public  DateTime ReleaseDate { get; set; }
        public  string Rating { get; set; }//التصنيف
        public  string URL { get; set; } //رابط البرنامج 
        public Guid AttachmentId { get; set; }
        public bool IsActive { get; set; }


    }
}

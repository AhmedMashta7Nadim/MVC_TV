using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_TV.VM
{
    public class AttachmentSummary
    {
        public Guid Id { get; set; }
        public  string Name { get; set; }
        public  string Path_File { get; set; }
        public  string FileType { get; set; }
        public bool IsActive { get; set; }
    }
}

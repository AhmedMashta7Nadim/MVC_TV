using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_TV.Creation
{
    public class AttachmentCreation
    {
        public required string Name { get; set; } = string.Empty;
        public IFormFile File { get; set; }
    }
}

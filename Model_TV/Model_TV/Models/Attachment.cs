using Microsoft.AspNetCore.Http;
using Models_TV.Model.Entity_programing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_TV.Models
{
    public class Attachment : Entity_Id
    {
        [MaxLength(100)]
        public required string Name { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile File { get; set; }
        public required string Path_File { get; set; } = string.Empty;
        public required string FileType { get; set; } = string.Empty;
        //public bool IsActive { get; set; } = true;
    }
 }
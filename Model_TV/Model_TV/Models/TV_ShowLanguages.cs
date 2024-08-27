using Models_TV.Model.Entity_programing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_TV.Models
{
    public class TV_ShowLanguages:Entity_Id
    {
        public Guid TV_ShowId { get; set; }
        [ForeignKey(nameof(TV_ShowId))]
        public TV_Show? TV_Show { get; set; }
        public Guid LanguagesId { get; set; }
        [ForeignKey(nameof(LanguagesId))]
        public Languages? Languages { get; set; }
        public DateTime CreatedAt { get; set; } =DateTime.UtcNow;

    }
}


using Models_TV.Model.Entity_programing;

namespace Model_TV.Models
{
    public class Languages : Entity_Id
    {
        public required string Name { get; set; } = string.Empty;
        public bool IsAccess { get; set; }
        public List<TV_Show> tV_Shows { get; set; } = new List<TV_Show>();
        public List<TV_ShowLanguages> tv_languages { get; set; } = new List<TV_ShowLanguages>();

    }




}

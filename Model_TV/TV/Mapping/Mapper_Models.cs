using AutoMapper;
using Model_TV.Creation;
using Model_TV.Models;
using Model_TV.VM;

namespace TV.Mapping
{
    public class Mapper_Models:Profile
    {

        public Mapper_Models()
        {
            CreateMap<TV_Show, TV_ShowSummary>().ReverseMap(); 
            CreateMap<TV_Show, TV_ShowCreation>().ReverseMap(); 

            CreateMap<Attachment, AttachmentSummary>().ReverseMap(); 
            CreateMap<Attachment, AttachmentCreation>().ReverseMap(); 
            CreateMap<AttachmentSummary, AttachmentCreation>().ReverseMap();

            CreateMap<Languages, LanguagesSummary>().ReverseMap();

        }

    }
}

using AutoMapper;
using Model_TV.Models;
using Model_TV.VM;
using TV.Data;
using TV.Repositry.Serves;

namespace TV.Repositry.RepoModels
{
    public class RepoTV_ShowLanguages : RepositryAllModel<TV_ShowLanguages, TV_ShowLanguagesSummary>
    {
        public RepoTV_ShowLanguages(IMapper mapper, ApplicationDbContext context) : base(mapper, context)
        {
        }
    }
}

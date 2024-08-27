using AutoMapper;
using Model_TV.Models;
using Model_TV.VM;
using TV.Data;

namespace TV.Repositry.RepoModels
{
    public class RepoLanguages : RepositryAllModel<Languages, LanguagesSummary>
    {
        public RepoLanguages(IMapper mapper, ApplicationDbContext context) : base(mapper, context)
        {
        }
    }
}

using AutoMapper;
using Model_TV.Models;
using Model_TV.VM;
using TV.Data;

namespace TV.Repositry.RepoModels
{
    public class RepoTV_Show : RepositryAllModel<TV_Show, TV_ShowSummary>
    {
        public RepoTV_Show(IMapper mapper, ApplicationDbContext context) : base(mapper, context)
        {
        }
    }
}

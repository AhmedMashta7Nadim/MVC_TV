using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model_TV.Models;
using Model_TV.VM;
using NuGet.Protocol.Core.Types;
using TV.Data;
using TV.Repositry.Serves;

namespace TV.Components
{
    public class TV_ShowListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext context;
        private readonly IRepositryAllModel<TV_Show, TV_ShowSummary> repositry;
        private readonly IStringLocalizer<TV_ShowListViewComponent> localizer;

        public TV_ShowListViewComponent(ApplicationDbContext context,
            IRepositryAllModel<TV_Show,TV_ShowSummary>repositry,
            IStringLocalizer<TV_ShowListViewComponent> localizer
            )
        {
            this.context = context;
            this.repositry = repositry;
            this.localizer = localizer;
        }

        public async Task<IViewComponentResult> InvokeAsync(int x)
        {

            var tvshow =await repositry.GetAllAsyncP(x);
            return View(tvshow);
            //return Task.FromResult<IViewComponentResult>(
            //    View(tvshow)
            //    );
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using NopBrasil.Plugin.Widgets.Polls.Service;

namespace NopBrasil.Plugin.Widgets.ContactData.Component
{
    [ViewComponent(Name = "WidgetsPolls")]
    public class WidgetsPollsViewComponent : NopViewComponent
    {
        private readonly IWidgetPollsService _widgetPollsService;

        public WidgetsPollsViewComponent(IWidgetPollsService widgetPollsService)
        {
            this._widgetPollsService = widgetPollsService;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData) => View("~/Plugins/Widgets.Polls/Views/PublicInfo.cshtml", _widgetPollsService.GetModel());
    }
}

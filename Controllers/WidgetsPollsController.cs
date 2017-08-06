using System.Web.Mvc;
using NopBrasil.Plugin.Widgets.Polls.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Controllers;
using Nop.Web.Controllers;
using NopBrasil.Plugin.Widgets.Polls.Service;

namespace NopBrasil.Plugin.Widgets.Polls.Controllers
{
    public class WidgetsPollsController : BasePublicController
    {
        private readonly ISettingService _settingService;
        private readonly PollsSettings _PollsSettings;
        private readonly IWidgetPollsService _widgetPollsService;

        public WidgetsPollsController(ISettingService settingService,
            PollsSettings PollsSettings, IWidgetPollsService widgetPollsService)
        {
            this._settingService = settingService;
            this._PollsSettings = PollsSettings;
            this._widgetPollsService = widgetPollsService;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            var model = new ConfigurationModel()
            {
                WidgetZone = _PollsSettings.WidgetZone,
                QtdPolls = _PollsSettings.QtdPolls
            };
            return View("~/Plugins/Widgets.Polls/Views/WidgetsPolls/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
            {
                return Configure();
            }
            _PollsSettings.QtdPolls = model.QtdPolls;
            _PollsSettings.WidgetZone = model.WidgetZone;
            _settingService.SaveSetting(_PollsSettings);
            return Configure();
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
             return View("~/Plugins/Widgets.Polls/Views/WidgetsPolls/PublicInfo.cshtml", _widgetPollsService.GetModel());
        }
    }
}
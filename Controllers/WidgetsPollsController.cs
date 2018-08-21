using NopBrasil.Plugin.Widgets.Polls.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Localization;

namespace NopBrasil.Plugin.Widgets.Polls.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsPollsController : BasePluginController
    {
        private readonly ISettingService _settingService;
        private readonly PollsSettings _pollsSettings;
        private readonly ILocalizationService _localizationService;

        public WidgetsPollsController(ISettingService settingService, PollsSettings pollsSettings, ILocalizationService localizationService)
        {
            this._settingService = settingService;
            this._pollsSettings = pollsSettings;
            this._localizationService = localizationService;
        }

        public ActionResult Configure()
        {
            var model = new ConfigurationModel()
            {
                WidgetZone = _pollsSettings.WidgetZone,
                QtdPolls = _pollsSettings.QtdPolls
            };
            return View("~/Plugins/Widgets.Polls/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            _pollsSettings.QtdPolls = model.QtdPolls;
            _pollsSettings.WidgetZone = model.WidgetZone;
            _settingService.SaveSetting(_pollsSettings);
            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}
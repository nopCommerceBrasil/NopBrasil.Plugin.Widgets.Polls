using NopBrasil.Plugin.Widgets.Polls.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Messages;

namespace NopBrasil.Plugin.Widgets.Polls.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsPollsController : BasePluginController
    {
        private readonly ISettingService _settingService;
        private readonly PollsSettings _pollsSettings;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;

        public WidgetsPollsController(ISettingService settingService, PollsSettings pollsSettings, ILocalizationService localizationService,
            INotificationService notificationService, IPermissionService permissionService)
        {
            this._settingService = settingService;
            this._pollsSettings = pollsSettings;
            this._localizationService = localizationService;
            this._notificationService = notificationService;
            this._permissionService = permissionService;
        }

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            var model = new ConfigurationModel()
            {
                WidgetZone = _pollsSettings.WidgetZone,
                QtdPolls = _pollsSettings.QtdPolls
            };
            return View("~/Plugins/Widgets.Polls/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            _pollsSettings.QtdPolls = model.QtdPolls;
            _pollsSettings.WidgetZone = model.WidgetZone;
            _settingService.SaveSetting(_pollsSettings);
            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}
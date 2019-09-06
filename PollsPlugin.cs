using System.Collections.Generic;
using Nop.Core;
using Nop.Services.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;

namespace NopBrasil.Plugin.Widgets.Polls
{
    public class PollsPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;
        private readonly PollsSettings _pollsSettings;
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;

        public PollsPlugin(ISettingService settingService, PollsSettings pollsSettings, IWebHelper webHelper, ILocalizationService localizationService)
        {
            this._settingService = settingService;
            this._pollsSettings = pollsSettings;
            this._webHelper = webHelper;
            this._localizationService = localizationService;
        }

        public IList<string> GetWidgetZones() => new List<string> { _pollsSettings.WidgetZone };

        public override string GetConfigurationPageUrl() => _webHelper.GetStoreLocation() + "Admin/WidgetsPolls/Configure";

        public override void Install()
        {
            var settings = new PollsSettings
            {
                QtdPolls= 1,
                WidgetZone = "home_page_before_poll"
            };
            _settingService.SaveSetting(settings);

            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Polls.Fields.WidgetZone", "WidgetZone name");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Polls.Fields.WidgetZone.Hint", "Enter the WidgetZone name that will display the HTML code.");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Polls.Fields.QtdPolls", "Number of Polls items");
            _localizationService.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Polls.Fields.QtdPolls.Hint", "Enter the number of Polls items that will be displayed in view.");

            base.Install();
        }

        public override void Uninstall()
        {
            _settingService.DeleteSetting<PollsSettings>();

            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.WidgetZone");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.WidgetZone.Hint");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.QtdPolls");
            _localizationService.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.QtdPolls.Hint");

            base.Uninstall();
        }

        public string GetWidgetViewComponentName(string widgetZone) => "WidgetsPolls";

        public bool HideInWidgetList => false;
    }
}

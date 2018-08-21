using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Plugins;
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

        public PollsPlugin(ISettingService settingService, PollsSettings pollsSettings, IWebHelper webHelper)
        {
            this._settingService = settingService;
            this._pollsSettings = pollsSettings;
            this._webHelper = webHelper;
        }

        public IList<string> GetWidgetZones() => new List<string> { _pollsSettings.WidgetZone };

        public override string GetConfigurationPageUrl() => _webHelper.GetStoreLocation() + "Admin/WidgetsPolls/Configure";

        public void GetPublicViewComponent(string widgetZone, out string viewComponentName) => viewComponentName = "WidgetsPolls";

        public override void Install()
        {
            var settings = new PollsSettings
            {
                QtdPolls= 1,
                WidgetZone = "home_page_before_poll"
            };
            _settingService.SaveSetting(settings);

            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Polls.Fields.WidgetZone", "WidgetZone name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Polls.Fields.WidgetZone.Hint", "Enter the WidgetZone name that will display the HTML code.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Polls.Fields.QtdPolls", "Number of Polls items");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.Polls.Fields.QtdPolls.Hint", "Enter the number of Polls items that will be displayed in view.");

            base.Install();
        }

        public override void Uninstall()
        {
            _settingService.DeleteSetting<PollsSettings>();

            this.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.WidgetZone");
            this.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.WidgetZone.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.QtdPolls");
            this.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.QtdPolls.Hint");

            base.Uninstall();
        }
    }
}

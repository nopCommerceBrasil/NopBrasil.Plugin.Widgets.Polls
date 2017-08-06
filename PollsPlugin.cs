using System.Collections.Generic;
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;

namespace NopBrasil.Plugin.Widgets.Polls
{
    public class PollsPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;
        private readonly PollsSettings _pollsSettings;

        public PollsPlugin(IPictureService pictureService,
            ISettingService settingService, PollsSettings pollsSettings)
        {
            this._settingService = settingService;
            this._pollsSettings = pollsSettings;
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string> { _pollsSettings.WidgetZone };
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WidgetsPolls";
            routeValues = new RouteValueDictionary { { "Namespaces", "NopBrasil.Plugin.Widgets.Polls.Controllers" }, { "area", null } };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "WidgetsPolls";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "NopBrasil.Plugin.Widgets.Polls.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

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
            //settings
            _settingService.DeleteSetting<PollsSettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.WidgetZone");
            this.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.WidgetZone.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.QtdPolls");
            this.DeletePluginLocaleResource("Plugins.Widgets.Polls.Fields.QtdPolls.Hint");

            base.Uninstall();
        }
    }
}

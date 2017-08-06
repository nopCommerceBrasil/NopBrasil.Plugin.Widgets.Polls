using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace NopBrasil.Plugin.Widgets.Polls.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Polls.Fields.WidgetZone")]
        [AllowHtml]
        public string WidgetZone { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Polls.Fields.QtdPolls")]
        [AllowHtml]
        public int QtdPolls { get; set; }
    }
}
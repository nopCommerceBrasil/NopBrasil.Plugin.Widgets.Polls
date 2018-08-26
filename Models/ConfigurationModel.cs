using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace NopBrasil.Plugin.Widgets.Polls.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Polls.Fields.WidgetZone")]
        public string WidgetZone { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.Polls.Fields.QtdPolls")]
        public int QtdPolls { get; set; }
    }
}
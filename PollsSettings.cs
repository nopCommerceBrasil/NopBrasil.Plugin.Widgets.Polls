using Nop.Core.Configuration;

namespace NopBrasil.Plugin.Widgets.Polls
{
    public class PollsSettings : ISettings
    {
        public string WidgetZone { get; set; }
        public int QtdPolls { get; set; }
    }
}
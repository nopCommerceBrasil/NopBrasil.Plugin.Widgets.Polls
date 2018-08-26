using Nop.Web.Framework.Models;

namespace NopBrasil.Plugin.Widgets.Polls.Models
{
    public class PollAnswerModel : BaseNopEntityModel
    {
        public string Name { get; set; }

        public int NumberOfVotes { get; set; }

        public double PercentOfTotalVotes { get; set; }
    }
}

using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;

namespace NopBrasil.Plugin.Widgets.Polls.Models
{
    public class PollModel : BaseNopEntityModel
    {
        public PollModel()
        {
            Answers = new List<PollAnswerModel>();
        }

        public string Name { get; set; }

        public bool AlreadyVoted { get; set; }

        public int TotalVotes { get; set; }

        public IList<PollAnswerModel> Answers { get; set; }
    }
}

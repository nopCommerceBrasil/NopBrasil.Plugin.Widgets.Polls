using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Polls;
using Nop.Services.Polls;
using NopBrasil.Plugin.Widgets.Polls.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NopBrasil.Plugin.Widgets.Polls.Service
{
    public class WidgetPollsService : IWidgetPollsService
    {
        private readonly IPollService _pollService;
        private readonly PollsSettings _pollsSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;

        public WidgetPollsService(IPollService pollService, PollsSettings pollsSettings, IStaticCacheManager cacheManager, IWorkContext workContext)
        {
            this._pollService = pollService;
            this._pollsSettings = pollsSettings;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
        }

        private IList<PollModel> GetAllPolls()
        {
            string cacheKey = $"nopBrasil:polls:language:{_workContext.WorkingLanguage.Id}:qtd:{_pollsSettings.QtdPolls}";
            return _cacheManager.Get<IList<PollModel>>(cacheKey, () => _pollService.GetPolls(_workContext.WorkingLanguage.Id, false, pageIndex: 0, pageSize: _pollsSettings.QtdPolls).Select(p => PreparePollModel(p)).ToList());
        }

        public PublicInfoModel GetModel() => new PublicInfoModel { Polls = GetAllPolls() };
        
        private PollModel PreparePollModel(Poll poll)
        {
            if (poll == null)
                throw new ArgumentNullException("poll");

            var model = new PollModel
            {
                Id = poll.Id,
                AlreadyVoted = _pollService.AlreadyVoted(poll.Id, _workContext.CurrentCustomer.Id),
                Name = poll.Name
            };
            var answers = poll.PollAnswers.OrderBy(x => x.DisplayOrder);
            foreach (var answer in answers)
                model.TotalVotes += answer.NumberOfVotes;
            foreach (var pa in answers)
            {
                model.Answers.Add(new PollAnswerModel
                {
                    Id = pa.Id,
                    Name = pa.Name,
                    NumberOfVotes = pa.NumberOfVotes,
                    PercentOfTotalVotes = model.TotalVotes > 0 ? ((Convert.ToDouble(pa.NumberOfVotes) / Convert.ToDouble(model.TotalVotes)) * Convert.ToDouble(100)) : 0
                });
            }

            return model;
        }
    }
}

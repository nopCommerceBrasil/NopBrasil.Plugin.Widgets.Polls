﻿using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;

namespace NopBrasil.Plugin.Widgets.Polls.Models
{
    public class PublicInfoModel : BaseNopModel, ICloneable
    {
        public PublicInfoModel()
        {
            Polls = new List<PollModel>();
        }

        public IList<PollModel> Polls { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
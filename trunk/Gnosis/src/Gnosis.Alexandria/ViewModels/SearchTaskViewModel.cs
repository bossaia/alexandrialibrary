﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Tasks;

namespace Gnosis.Alexandria.ViewModels
{
    public class SearchTaskViewModel
        : TaskViewModel<IMediaItem>
    {
        public SearchTaskViewModel(ILogger logger, ISearchTask task, string search)
            : base(logger, task, "Search", search, "pack://application:,,,/Images/sphinx_circle.png")
        {
            searchTask = task;
        }

        private readonly ISearchTask searchTask;

        public SearchFilters Filters
        {
            get { return searchTask.Filters; }
            set { searchTask.Filters = value; }
        }

        public void SetPattern(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            searchTask.SetPattern(pattern);
        }
    }
}
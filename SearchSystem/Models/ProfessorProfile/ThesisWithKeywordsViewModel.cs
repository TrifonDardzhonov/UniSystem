using SearchSystem.Entities;
using SearchSystem.Models.BaseViewModels;
using SearchSystem.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystem.Models
{
    public class ThesisWithKeywordsViewModel
    {
        public ThesisWithKeywordsViewModel()
        {
            this.Thesis = new ThesisViewModel();
            this.Keywords = new List<Keyword>();
            this.CheckboxList = new List<int>();
        }

        public ThesisViewModel Thesis { get; set; }
        public IList<Keyword> Keywords { get; set; }
        public IEnumerable<int> CheckboxList { get; set; }
    }
}
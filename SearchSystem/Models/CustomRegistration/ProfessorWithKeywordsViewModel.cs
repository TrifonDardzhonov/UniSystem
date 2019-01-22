using SearchSystem.Entities;
using SearchSystem.Models.BaseViewModels;
using SearchSystem.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystem.Models
{
    public class ProfessorWithKeywordsViewModel
    {
        public ProfessorWithKeywordsViewModel()
        {
            this.Professor = new ProfessorViewModel();
            this.Keywords = new List<Keyword>();
            this.CheckboxList = new List<int>();
        }

        public ProfessorViewModel Professor { get; set; }
        public IList<Keyword> Keywords { get; set; }
        public IEnumerable<int> CheckboxList { get; set; }
    }
}
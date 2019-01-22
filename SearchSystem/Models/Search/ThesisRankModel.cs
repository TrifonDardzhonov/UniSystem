using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystem.Models
{
    public class ThesisRankModel
    {
        public int ThesisId { get; set; }
        public double Rank { get; set; }
        public string ThesisTitle { get; set; }
        public string ThesisDescription { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Complexity { get; set; }
        public string ProfessorFullName { get; set; }
    }
}
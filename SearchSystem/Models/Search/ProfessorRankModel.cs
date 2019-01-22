using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystem.Models
{
    public class ProfessorRankModel
    {
        public int ProfessorId { get; set; }
        public double Rank { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subjects { get; set; }
    }
}
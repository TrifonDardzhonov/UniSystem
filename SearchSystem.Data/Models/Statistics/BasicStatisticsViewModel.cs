using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Data.Models.Statistics
{
    public class BasicStatisticsViewModel
    {
        public string DepartmentName { get; set; }

        public int ProfessorsCount { get; set; }

        public int FreeBachelorThesesCount { get; set; }

        public int BusyBachelorThesesCount { get; set; }

        public int AwardedBachelorThesesCount { get; set; }

        public int FreeMasterThesesCount { get; set; }

        public int BusyMasterThesesCount { get; set; }

        public int AwardedMasterThesesCount { get; set; }
    }
}

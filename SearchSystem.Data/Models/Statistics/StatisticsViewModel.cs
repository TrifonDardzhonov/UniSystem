using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Data.Models.Statistics
{
    public class StatisticsViewModel
    {
        public IEnumerable<BasicStatisticsViewModel> Departments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Models.Constants
{
    public static class ThesisConstraints
    {
        public static readonly int MINIMUM_THESIS_TITLE_LENGTH = 20;
        public static readonly int MAXIMUM_THESIS_TITLE_LENGTH = 250;

        public static readonly int MINIMUM_THESIS_DESCRIPTION_LENGTH = 50;
        public static readonly int MAXIMUM_THESIS_DESCRIPTION_LENGTH = 2000;

        public static readonly int MINIMUM_THESIS_STUDENT_NAME_LENGTH = 5;
        public static readonly int MAXIMUM_THESIS_STUDENT_NAME_LENGTH = 30;

        public static readonly int MINIMUM_THESIS_STUDENT_FAKNO_LENGTH = 100000;
        public static readonly long MAXIMUM_THESIS_STUDENT_FAKNO_LENGTH = 10000000000;

        public static readonly int MINIMUM_THESIS_REVIEWER_NAME_LENGTH = 5;
        public static readonly int MAXIMUM_THESIS_REVIEWER_NAME_LENGTH = 30;

        public static readonly int MAXIMUM_THESIS_SIMILARITY = 100;

        public static readonly int FIRST_N_THESES = 10;
    }
}

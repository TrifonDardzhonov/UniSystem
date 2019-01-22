using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchSystem.Models.Constants
{
    public static class ProfessorConstraints
    {
        public static readonly int MINIMUM_PROFESSOR_FIRST_NAME_LENGTH = 3;
        public static readonly int MAXIMUM_PROFESSOR_FIRST_NAME_LENGTH = 15;

        public static readonly int MINIMUM_PROFESSOR_LAST_NAME_LENGTH = 3;
        public static readonly int MAXIMUM_PROFESSOR_LAST_NAME_LENGTH = 15; 

        public static readonly int MINIMUM_PROFESSOR_EDUCATION_LENGTH = 20;
        public static readonly int MAXIMUM_PROFESSOR_EDUCATION_LENGTH = 1000;

        public static readonly int MINIMUM_PROFESSOR_SPECIALISATIONS_LENGTH = 2;
        public static readonly int MAXIMUM_PROFESSOR_SPECIALISATIONS_LENGTH = 1000;

        public static readonly int MINIMUM_PROFESSOR_SUBJECTS_LENGTH = 5;
        public static readonly int MAXIMUM_PROFESSOR_SUBJECTS_LENGTH = 1000;

        public static readonly int MINIMUM_PROFESSOR_INTERESTS_LENGTH = 2;
        public static readonly int MAXIMUM_PROFESSOR_INTERESTS_LENGTH = 1000;

        public static readonly int MINIMUM_PROFESSOR_AWARDS_AND_HONORS_LENGTH = 2;
        public static readonly int MAXIMUM_PROFESSOR_AWARDS_AND_HONORS_LENGTH = 1000;

        public static readonly int MINIMUM_PROFESSOR_CERTIFICATIONS_LENGTH = 2;
        public static readonly int MAXIMUM_PROFESSOR_CERTIFICATIONS_LENGTH = 1000;

        public static readonly int MINIMUM_PROFESSOR_PATENTS_LENGTH = 2;
        public static readonly int MAXIMUM_PROFESSOR_PATENTS_LENGTH = 1000;

        public static readonly int MINIMUM_PROFESSOR_PUBLICATIONS_LENGTH = 20;
        public static readonly int MAXIMUM_PROFESSOR_PUBLICATIONS_LENGTH = 20000;

        public static readonly int MINIMUM_PROFESSOR_OFFICE_HOURS_LENGTH = 2;
        public static readonly int MAXIMUM_PROFESSOR_OFFICE_HOURS_LENGTH = 200;

        public static readonly int MINIMUM_PROFESSOR_PHONE_LENGTH = 2;
        public static readonly int MAXIMUM_PROFESSOR_PHONE_LENGTH = 40;

        public static readonly int FIRST_N_PROFESSORS = 10; 
    }
}

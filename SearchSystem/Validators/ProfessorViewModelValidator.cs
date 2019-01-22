using FluentValidation;
using SearchSystem.Models.BaseViewModels;
using SearchSystem.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystem.Validators
{
    public class ProfessorViewModelValidator : AbstractValidator<ProfessorViewModel>
    {
        public ProfessorViewModelValidator()
        {
            RuleFor(professor => professor.FirstName)
                .NotEmpty()
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_FIRST_NAME_LENGTH, 
                        ProfessorConstraints.MAXIMUM_PROFESSOR_FIRST_NAME_LENGTH);

            RuleFor(professor => professor.LastName)
                .NotEmpty()
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_LAST_NAME_LENGTH, 
                        ProfessorConstraints.MAXIMUM_PROFESSOR_LAST_NAME_LENGTH);

            RuleFor(professor => professor.Education)
                .NotEmpty()
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_EDUCATION_LENGTH, 
                        ProfessorConstraints.MAXIMUM_PROFESSOR_EDUCATION_LENGTH);

            RuleFor(professor => professor.Specialisations)
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_SPECIALISATIONS_LENGTH,
                        ProfessorConstraints.MAXIMUM_PROFESSOR_SPECIALISATIONS_LENGTH);

            RuleFor(professor => professor.Subjects)
                .NotEmpty()
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_SUBJECTS_LENGTH, 
                        ProfessorConstraints.MAXIMUM_PROFESSOR_SUBJECTS_LENGTH);

            RuleFor(professor => professor.ProfesionalInterests)
                .NotEmpty()
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_INTERESTS_LENGTH, 
                        ProfessorConstraints.MAXIMUM_PROFESSOR_INTERESTS_LENGTH);

            RuleFor(professor => professor.AwardsAndHonors)
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_AWARDS_AND_HONORS_LENGTH,
                        ProfessorConstraints.MAXIMUM_PROFESSOR_AWARDS_AND_HONORS_LENGTH);

            RuleFor(professor => professor.Certifications)
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_CERTIFICATIONS_LENGTH,
                        ProfessorConstraints.MAXIMUM_PROFESSOR_CERTIFICATIONS_LENGTH);

            RuleFor(professor => professor.Patents)
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_PATENTS_LENGTH,
                        ProfessorConstraints.MAXIMUM_PROFESSOR_PATENTS_LENGTH);

            RuleFor(professor => professor.Publications)
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_PUBLICATIONS_LENGTH,
                        ProfessorConstraints.MAXIMUM_PROFESSOR_PUBLICATIONS_LENGTH);

            RuleFor(professor => professor.OfficeHours)
                .NotEmpty()
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_OFFICE_HOURS_LENGTH, 
                        ProfessorConstraints.MAXIMUM_PROFESSOR_OFFICE_HOURS_LENGTH);

            RuleFor(professor => professor.Phone)
                .NotEmpty()
                .Length(ProfessorConstraints.MINIMUM_PROFESSOR_PHONE_LENGTH, 
                        ProfessorConstraints.MAXIMUM_PROFESSOR_PHONE_LENGTH);

            RuleFor(professor => professor.Email)
                .NotEmpty().WithMessage("Моля въведете имейл")
                .EmailAddress().WithMessage("Веведете валидем Email адрес");
        }
    }
}
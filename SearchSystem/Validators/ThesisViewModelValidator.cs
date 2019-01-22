using FluentValidation;
using SearchSystem.Data.Enums;
using SearchSystem.Models.BaseViewModels;
using SearchSystem.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystem.Validators
{
    public class ThesisViewModelValidator : AbstractValidator<ThesisViewModel>
    {
        public ThesisViewModelValidator()
        {
            RuleFor(thesis => thesis.ThesisTitle)
                .NotEmpty().WithMessage("Моля въведете име")
                .Length(ThesisConstraints.MINIMUM_THESIS_TITLE_LENGTH,
                        ThesisConstraints.MAXIMUM_THESIS_TITLE_LENGTH);

            RuleFor(thesis => thesis.ThesisDescription)
                .NotEmpty().WithMessage("Моля въведете описание")
                .Length(ThesisConstraints.MINIMUM_THESIS_DESCRIPTION_LENGTH,
                        ThesisConstraints.MAXIMUM_THESIS_DESCRIPTION_LENGTH)
                .NotEqual(x => x.ThesisTitle).WithMessage("Името и Описанието на темата не могат да бъдат еднакви");

            RuleFor(thesis => thesis.StudentName)
                .NotEmpty().WithMessage("Моля въведете име и фамилия на студента")
                .Length(ThesisConstraints.MINIMUM_THESIS_STUDENT_NAME_LENGTH,
                        ThesisConstraints.MAXIMUM_THESIS_STUDENT_NAME_LENGTH)
                .When(x=>!x.Status.Equals(ThesisStatusEnum.Free));

            RuleFor(thesis => thesis.StudentFakNo)
                .NotEmpty().WithMessage("Моля въведете факултетен номер на студента")
                .GreaterThan(ThesisConstraints.MINIMUM_THESIS_STUDENT_FAKNO_LENGTH)
                .LessThan(ThesisConstraints.MAXIMUM_THESIS_STUDENT_FAKNO_LENGTH)
                .When(x => !x.Status.Equals(ThesisStatusEnum.Free));

            RuleFor(thesis => thesis.ReviewerName)
                .NotEmpty().WithMessage("Моля въведете име и фамилия на рецензента")
                .Length(ThesisConstraints.MINIMUM_THESIS_REVIEWER_NAME_LENGTH,
                        ThesisConstraints.MAXIMUM_THESIS_REVIEWER_NAME_LENGTH)
                .When(x => x.Status.Equals(ThesisStatusEnum.Awarded));

            RuleFor(thesis => thesis.АwardedOn)
                .LessThan(DateTime.Now).WithMessage("Невалидна дата");
        }
    }
}
using SearchSystem.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SearchSystem.Models.BaseViewModels
{
    [FluentValidation.Attributes.Validator(typeof(ProfessorViewModelValidator))]
    public class ProfessorViewModel
    {
        public int ProfessorId { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Водени дисциплини")]
        [DataType(DataType.MultilineText)]
        public string Subjects { get; set; }

        [Display(Name = "Образование")]
        [DataType(DataType.MultilineText)]
        public string Education { get; set; }

        [Display(Name = "Специализации")]
        [DataType(DataType.MultilineText)]
        public string Specialisations { get; set; }

        [Display(Name = "Награди и отличия")]
        [DataType(DataType.MultilineText)]
        public string AwardsAndHonors { get; set; }

        [Display(Name = "Сертификати")]
        [DataType(DataType.MultilineText)]
        public string Certifications { get; set; }

        [Display(Name = "Патенти")]
        [DataType(DataType.MultilineText)]
        public string Patents { get; set; }

        [Display(Name = "Публикации")]
        [DataType(DataType.MultilineText)]
        public string Publications { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Приемно време")]
        [DataType(DataType.MultilineText)]
        public string OfficeHours { get; set; }

        [Display(Name = "Телефон/и за контакт")]
        [DataType(DataType.MultilineText)]
        public string Phone { get; set; }

        [Display(Name = "Научни интереси")]
        [DataType(DataType.MultilineText)]
        public string ProfesionalInterests { get; set; }

        [Display(Name = "Катедра")]
        public string DepartmentName { get; set; }

        public string UserId { get; set; }
    }
}
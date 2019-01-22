using SearchSystem.Data;
using SearchSystem.Data.Enums;
using SearchSystem.Entities;
using SearchSystem.Models.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchSystem
{
    public static class ViewModelsExtensionMethods 
    { 
        public static void IfFree(this ThesisViewModel model)  
        {
            if (model.Status.Equals(ThesisStatusEnum.Free))
            {
                model.StudentName = null;
                model.StudentFakNo = 0;
                model.ReviewerName = null;
            }
        }

        public static Thesis ConvertToNewThesis(this ThesisViewModel model, int professorId)
        {
            Thesis thesis = new Thesis();

                thesis.ThesisTitle = model.ThesisTitle;

                thesis.ThesisDescription = model.ThesisDescription;

                thesis.ProfessorId = professorId;

                thesis.Status = model.Status;

                thesis.Type = model.Type;

                thesis.Complexity = model.Complexity;

                thesis.StudentName = model.StudentName;

                thesis.StudentFakNo = model.StudentFakNo;

                thesis.ReviewerName = model.ReviewerName;

                thesis.CreatedOn = DateTime.Now;

                thesis.АwardedOn = model.АwardedOn;

            return thesis;
        }

        public static ThesisViewModel ConvertToThesisVM(this Thesis model)
        {
            ThesisViewModel thesisVM = new ThesisViewModel();

                thesisVM.ThesisId = model.ThesisId;

                thesisVM.ThesisTitle = model.ThesisTitle;

                thesisVM.ThesisDescription = model.ThesisDescription;

                thesisVM.ProfessorId = model.ProfessorId;

                thesisVM.Status = model.Status;

                thesisVM.Type = model.Type;

                thesisVM.Complexity = model.Complexity;

                thesisVM.StudentName = model.StudentName;

                thesisVM.StudentFakNo = model.StudentFakNo;

                thesisVM.ReviewerName = model.ReviewerName;

                thesisVM.CreatedOn = model.CreatedOn;

                thesisVM.АwardedOn = model.АwardedOn;

            return thesisVM;
        }

        #region professor
        public static Professor ConvertToNewProfessor(this ProfessorViewModel model, string userId)
        {
            Professor professor = new Professor();

                professor.FirstName = model.FirstName;

                professor.LastName = model.LastName;

                professor.Education = model.Education;

                professor.Subjects = model.Subjects;

                professor.Specialisations = model.Specialisations;

                professor.AwardsAndHonors = model.AwardsAndHonors;

                professor.Certifications = model.Certifications;

                professor.Patents = model.Patents;

                professor.Publications = model.Publications;

                professor.OfficeHours = model.OfficeHours;

                professor.Phone = model.Phone;

                professor.Email = model.Email;

                professor.ProfesionalInterests = model.ProfesionalInterests;

                professor.DepartmentName = model.DepartmentName;

                professor.UserId = userId;

            return professor;
        }

        public static ProfessorViewModel ConvertToProfessorVM(this Professor model)
        {
            ProfessorViewModel professorVM = new ProfessorViewModel();

                professorVM.ProfessorId = model.ProfessorId;

                professorVM.FirstName = model.FirstName;

                professorVM.LastName = model.LastName;

                professorVM.Education = model.Education;

                professorVM.Subjects = model.Subjects;

                professorVM.Specialisations = model.Specialisations;

                professorVM.AwardsAndHonors = model.AwardsAndHonors; 

                professorVM.Certifications = model.Certifications;

                professorVM.Patents = model.Patents;

                professorVM.Publications = model.Publications;

                professorVM.OfficeHours = model.OfficeHours;

                professorVM.Phone = model.Phone;

                professorVM.Email = model.Email;

                professorVM.ProfesionalInterests = model.ProfesionalInterests;

                professorVM.DepartmentName = model.DepartmentName;

                professorVM.UserId = model.UserId;

            return professorVM;
        }
        #endregion
    }
}
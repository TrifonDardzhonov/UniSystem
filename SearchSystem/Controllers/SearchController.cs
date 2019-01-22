using SearchSystem.Data;
using SearchSystem.Data.Repository;
using SearchSystem.Entities;
using SearchSystem.Models;
using SearchSystem.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchSystem.Controllers
{

    public class SearchController : Controller
    {
        private UnitOfWork _db;

        public SearchController()
        {
            this._db = new UnitOfWork();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowKeywords(string departmentName)
        {
            ViewBag.departmentName = departmentName;

            int departmentId = this._db.DepartmentRepository.GetDepartmentIdByName(departmentName);
            IEnumerable<Keyword> keywords = this._db.KeywordRepository.GetAllKeywordsFromDepartment(departmentId);

            return View(keywords);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "SearchProfessors")]
        public ActionResult SearchProfessors(IEnumerable<int> checkboxList, string departmentName)
        {
            if (checkboxList != null)
            {
                ViewBag.Error = false;
                IEnumerable<Professor> professors = this._db.ProfessorRepository.GetAllProfessorsWithKeywordsFromDepartment(departmentName);//Get all professors from selected department
                
                //Make array for all professors with their ranks
                int professorsCount = professors.Count();
                if (professorsCount > 0)
                {
                    ProfessorRankModel[] ProfessorsRanks = new ProfessorRankModel[professorsCount];
                    for (int i = 0; i < ProfessorsRanks.Length; i++)
                    {
                        ProfessorsRanks[i] = new ProfessorRankModel();
                    }

                    int currentProfessor = 0;
                    int fullMatch = 0; //full match between user keyword and professor keyword
                    int diferentFromZero = 0;//full match or with same node
                    double professorRank = 0.0f;

                    IQueryable<KeywordBasicModel> userKeywords = this._db.KeywordRepository.GetUserKeywordsAndFields(checkboxList.ToList<int>());//Get keywords from UserKeywords

                    foreach (Professor professor in professors)
                    {
                        foreach (Keyword professorKeyword in professor.Keywords)
                        {
                            //foreach every keywords from user
                            foreach (KeywordBasicModel userKeyword in userKeywords)
                            {
                                //compare if they are equal (if compare return 0 they are equal)
                                if (string.Compare(professorKeyword.Value, userKeyword.Value) == 0)
                                {
                                    professorRank += 1.0f;
                                    fullMatch++;
                                    diferentFromZero++;
                                }
                                //compare if they have same node
                                else if (professorKeyword.FieldId == userKeyword.FieldId)
                                {
                                    professorRank += 0.5f;
                                    diferentFromZero++;
                                }
                                //if they dont equal and they dont have same node
                                else
                                {
                                    professorRank += 0.0f;
                                }
                            }
                        }

                        if (fullMatch == userKeywords.Count() || fullMatch == professor.Keywords.Count)
                        {
                            professorRank = 1.0f * 100;
                            ProfessorsRanks[currentProfessor].Rank = professorRank;
                            ProfessorsRanks[currentProfessor].ProfessorId = professor.ProfessorId;
                            ProfessorsRanks[currentProfessor].FirstName = professor.FirstName;
                            ProfessorsRanks[currentProfessor].LastName = professor.LastName;
                            ProfessorsRanks[currentProfessor].Subjects = professor.Subjects;
                        }
                        else
                        {
                            professorRank = Math.Round((professorRank / diferentFromZero) * 100, 2);
                            ProfessorsRanks[currentProfessor].Rank = professorRank;
                            ProfessorsRanks[currentProfessor].ProfessorId = professor.ProfessorId;
                            ProfessorsRanks[currentProfessor].FirstName = professor.FirstName;
                            ProfessorsRanks[currentProfessor].LastName = professor.LastName;
                            ProfessorsRanks[currentProfessor].Subjects = professor.Subjects;
                        }

                        //For every professor
                        fullMatch = 0;
                        professorRank = 0.0f;
                        diferentFromZero = 0;
                        currentProfessor++;
                    }
                    IEnumerable<ProfessorRankModel> sorted = ProfessorsRanks
                        .Where(r => double.IsNaN(r.Rank) == false)
                        .OrderByDescending(o => o.Rank)
                        .Take(ProfessorConstraints.FIRST_N_PROFESSORS);

                    return View(sorted);
                }
            }
            ViewBag.Error = true;
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "SearchThesеs")]
        public ActionResult SearchThesеs(IEnumerable<int> checkboxList, string departmentName, string type, string complexity, string status)
        {
            if (checkboxList!=null)
            {
                string defaultString = "Всички";
                ViewBag.Error = false;
                ViewBag.typeFilterApplied = !type.Equals(defaultString);
                ViewBag.complexityFilterApplied = !complexity.Equals(defaultString);
                ViewBag.statusFilterApplied = !status.Equals(defaultString);

                List<Thesis> theses = this._db.ThesisRepository
                    .GetAllThesesWithKeywordsFromDepartment(departmentName)
                    .Where(thesis => ((ViewBag.typeFilterApplied == true) ? thesis.Type.Equals(type) : true) &&
                                     ((ViewBag.complexityFilterApplied == true) ? thesis.Complexity.Equals(complexity) : true) &&
                                     ((ViewBag.statusFilterApplied == true) ? thesis.Status.Equals(status) : true))
                    .ToList<Thesis>();

                //Make array for all thesis with their ranks
                int thesesCount = theses.Count();
                if (thesesCount > 0)
                {
                    ThesisRankModel[] ThesesRanks = new ThesisRankModel[thesesCount];
                    for (int i = 0; i < ThesesRanks.Length; i++)
                    {
                        ThesesRanks[i] = new ThesisRankModel();
                    }

                    int currentThesis = 0;
                    int fullMatch = 0; //full match between user keyword and thesis keyword
                    int diferentFromZero = 0;//full match or with same node
                    double thesisRank = 0.0f;

                    IQueryable<KeywordBasicModel> userKeywords = this._db.KeywordRepository.GetUserKeywordsAndFields(checkboxList.ToList<int>());//Get keywords from UserKeywords

                    foreach (Thesis thesis in theses)
                    {
                        foreach (Keyword thesisKeyword in thesis.Keywords)
                        {
                            //foreach every keywords from user
                            foreach (KeywordBasicModel userKeyword in userKeywords)
                            {
                                //compare if they are equal (if compare return 0 they are equal)
                                if (string.Compare(thesisKeyword.Value, userKeyword.Value) == 0)
                                {
                                    thesisRank += 1.0f;
                                    fullMatch++;
                                    diferentFromZero++;
                                }
                                //compare if they have same node
                                else if (thesisKeyword.FieldId == userKeyword.FieldId)
                                {
                                    thesisRank += 0.5f;
                                    diferentFromZero++;
                                }
                                //if they dont equal and they dont have same node
                                else
                                {
                                    thesisRank += 0.0f;
                                }
                            }
                        }

                        if (fullMatch == userKeywords.Count() || fullMatch == thesis.Keywords.Count)
                        {
                            thesisRank = 1.0f * 100;
                            ThesesRanks[currentThesis].Rank = thesisRank;
                            ThesesRanks[currentThesis].ThesisId = thesis.ThesisId;
                            ThesesRanks[currentThesis].ThesisTitle = thesis.ThesisTitle;
                            ThesesRanks[currentThesis].ThesisDescription = thesis.ThesisDescription;
                            ThesesRanks[currentThesis].Status = thesis.Status;
                            ThesesRanks[currentThesis].Type = thesis.Type;
                            ThesesRanks[currentThesis].Complexity = thesis.Complexity;
                            ThesesRanks[currentThesis].ProfessorFullName = thesis.Professor.FirstName + " " + thesis.Professor.LastName;
                        }
                        else
                        {
                            thesisRank = Math.Round((thesisRank / diferentFromZero) * 100, 2);
                            ThesesRanks[currentThesis].Rank = thesisRank;
                            ThesesRanks[currentThesis].ThesisId = thesis.ThesisId;
                            ThesesRanks[currentThesis].ThesisTitle = thesis.ThesisTitle;
                            ThesesRanks[currentThesis].ThesisDescription = thesis.ThesisDescription;
                            ThesesRanks[currentThesis].Status = thesis.Status;
                            ThesesRanks[currentThesis].Type = thesis.Type;
                            ThesesRanks[currentThesis].Complexity = thesis.Complexity;
                            ThesesRanks[currentThesis].ProfessorFullName = thesis.Professor.FirstName + " " + thesis.Professor.LastName;
                        }

                        //For every thesis
                        fullMatch = 0;
                        thesisRank = 0.0f;
                        diferentFromZero = 0;
                        currentThesis++;
                    }
                    IEnumerable<ThesisRankModel> sorted = ThesesRanks
                        .Where(r=>double.IsNaN(r.Rank) == false)
                        .OrderByDescending(o => o.Rank)
                        .Take(ThesisConstraints.FIRST_N_THESES);

                    return View(sorted);
                }
            }
            ViewBag.Error = true;
            return View();
        }
	}
}
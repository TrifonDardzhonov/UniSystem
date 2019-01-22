using SearchSystem.Data;
using SearchSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SearchSystem.Entities;
using SearchSystem.Models.BaseViewModels;
using Newtonsoft.Json;
using SearchSystem.Models.Constants;
using SearchSystem.Data.Repository;

namespace SearchSystem.Controllers
{
    [Authorize(Roles = "Professor")]
    public class ProfessorProfileController : Controller
    {
        private UnitOfWork _db;
        private bool isUniqueThesis = true;

        public ProfessorProfileController()
        {
            this._db = new UnitOfWork();
        }

        public ActionResult Index()
        {
            return View();
        }

        //****************************************************
        public ActionResult ProfessorProfile()
        {
            int loggedProfessorId = this._db.ProfessorRepository.GetProfessorIDByUserId(User.Identity.GetUserId());
            HttpContext.Session.Add("loggedProfessorId", loggedProfessorId);

            int departmentId = this._db.ProfessorRepository.GetDepartmentIdByProfessorUserId(User.Identity.GetUserId());
            HttpContext.Session.Add("departmentId", departmentId);

            IEnumerable<Thesis> professorTheses = this._db.ProfessorRepository.GetProfessorTheses(loggedProfessorId);
            return View(professorTheses);
        }

        #region editing professor info
        [HttpGet]
        public ActionResult EditProfessor()
        {
            Professor professor = this._db.ProfessorRepository.GetProfessorById((int)HttpContext.Session["loggedProfessorId"]);
            return View(professor.ConvertToProfessorVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfessor(ProfessorViewModel model) 
        {
            if (model.ProfessorId == (int)HttpContext.Session["loggedProfessorId"])
            {
                if (ModelState.IsValid)
                {
                    this._db.ProfessorRepository.EditProfile(
                                        model.FirstName,
                                        model.LastName,
                                        model.Subjects,
                                        model.Education,
                                        model.Specialisations,
                                        model.AwardsAndHonors,
                                        model.Certifications,
                                        model.Patents,
                                        model.Publications,
                                        model.ProfesionalInterests,
                                        model.OfficeHours,
                                        model.Phone,
                                        model.Email,
                                        model.ProfessorId);

                    return RedirectToAction("ProfessorProfile");
                }
            }
            return View(model);
        }
        #endregion

        #region editing professor keywords
        [HttpGet]
        public ActionResult EditProfessorKeywords()
        {
            IEnumerable<Keyword> departmentKeywords = this._db.KeywordRepository.GetAllKeywordsFromDepartment((int)HttpContext.Session["departmentId"]);

            IEnumerable<int> professorKeywordsIds = this._db.ProfessorRepository.GetProfessorKeywordsIds((int)HttpContext.Session["loggedProfessorId"]);
            ViewBag.SelectedKeywords = professorKeywordsIds;

            return View(departmentKeywords);
        }

        [HttpPost]
        public ActionResult EditProfessorKeywords(IEnumerable<int> checkboxList)
        {
            if (checkboxList.Count() > 0)
            {
                Professor professor = this._db.ProfessorRepository.GetProfessorById((int)HttpContext.Session["loggedProfessorId"]);

                                this._db.ProfessorRepository.ClearKeywords(professor);

                this._db.ProfessorRepository.AddKWToProfessor(professor, checkboxList);
            }
            
            return RedirectToAction("ProfessorProfile");
        }
        #endregion

        #region create thesis
        [HttpGet]
        public ActionResult CreateThesis()
        {
            int departmentId = this._db.ProfessorRepository.GetDepartmentIdByProfessorUserId(User.Identity.GetUserId());

            ThesisWithKeywordsViewModel thesis = new ThesisWithKeywordsViewModel();
            thesis.Keywords = this._db.KeywordRepository.GetAllKeywordsFromDepartment(departmentId);

            return View(thesis);
        }

        //this name 'model' eat my soul !!!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateThesis(ThesisWithKeywordsViewModel model)
        {
            if (ModelState.IsValid && model.CheckboxList.Count() > 0)
            {
                Professor professor = this._db.ProfessorRepository.GetProfessorById((int)HttpContext.Session["loggedProfessorId"]);
                int similarThesisId = ReturnSimilarThesisId(model.Thesis.ThesisTitle, model.Thesis.ThesisDescription, model.CheckboxList, professor.DepartmentName);
                //There isn't similar thesis with same name or description
                if (isUniqueThesis)
                {
                    model.Thesis.IfFree();

                    Thesis thesis = model.Thesis.ConvertToNewThesis(professor.ProfessorId);
                    this._db.ThesisRepository.Add(thesis);
                    this._db.ThesisRepository.SaveChanges();

                    //After thesis is created, we get it with LINQ and then add keywords to it
                    this._db.ThesisRepository.AddKWToThesis((Thesis)this._db.ThesisRepository.GetThesisByTitle(model.Thesis.ThesisTitle), model.CheckboxList);
                    return RedirectToAction("ProfessorProfile");
                }
                else
                {
                    Thesis similarThesis = this._db.ThesisRepository.GetThesisByID(similarThesisId);
                    return View("SimilarThesis", similarThesis);
                }
            }
            //WE get keywords only for professor department
            model.Keywords = this._db.KeywordRepository.GetAllKeywordsFromDepartment((int)HttpContext.Session["departmentId"]);
            ViewBag.SelectedKeywords = model.CheckboxList;
            return View(model);
        }
        #endregion

        #region delete thesis
        public ActionResult DeleteThesis(int thesisid, int professorId)
        {
            if (professorId == (int)HttpContext.Session["loggedProfessorId"])
            {
                this._db.ThesisRepository.Delete(thesisid);
                this._db.ThesisRepository.SaveChanges();

                return RedirectToAction("ProfessorProfile");
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region edit thesis info
        [HttpGet]
        public ActionResult EditThesis(int thesisid, int professorId)
        {
            if (professorId == (int)HttpContext.Session["loggedProfessorId"])
            {
                Thesis thesis = this._db.ThesisRepository.GetThesisByID(thesisid);

                return View(thesis.ConvertToThesisVM());
            }
            return RedirectToAction("Index", "Home");
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditThesis(ThesisViewModel model)
        {
            if (model.ProfessorId == (int)HttpContext.Session["loggedProfessorId"])
            {
                if (ModelState.IsValid)
                {
                    model.IfFree();

                    this._db.ThesisRepository.EditThesis(
                                        model.ThesisTitle,
                                        model.ThesisDescription,
                                        model.Status,
                                        model.Type,
                                        model.Complexity,
                                        model.StudentName,
                                        model.StudentFakNo,
                                        model.ReviewerName,
                                        model.АwardedOn,
                                        model.ThesisId);

                    return RedirectToAction("ProfessorProfile");              
                }
            }
            return View(model);
        }
        #endregion

        #region edit thesis keywords
        [HttpGet]
        public ActionResult EditThesisKeywords(int thesisId, int professorId)
        {
            if (professorId == (int)HttpContext.Session["loggedProfessorId"])
            {
                IEnumerable<Keyword> departmentKeywords = this._db.KeywordRepository.GetAllKeywordsFromDepartment((int)HttpContext.Session["departmentId"]);

                IEnumerable<int> thesisKeywordsIds = this._db.ThesisRepository.GetThesisKeywordsIds(thesisId);

                ViewBag.ThesisId = thesisId;
                ViewBag.ProfessorId = professorId;
                ViewBag.SelectedKeywords = thesisKeywordsIds;

                return View(departmentKeywords);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditThesisKeywords(IEnumerable<int> checkboxList, int thesisId, int professorId)
        {
            if (professorId == (int)HttpContext.Session["loggedProfessorId"])
            {
                if (checkboxList.Count() > 0)
                {
                    Thesis thesis = this._db.ThesisRepository.GetThesisByID(thesisId);

                                 this._db.ThesisRepository.ClearKeywords(thesis);

                    this._db.ThesisRepository.AddKWToThesis(thesis, checkboxList);
                }
            }

            return RedirectToAction("ProfessorProfile");
        }
        #endregion

        //****************************************************Search for similar thesis
        [NonAction]
        public int ReturnSimilarThesisId(string newThesisTitle, string newThesisDescription, IEnumerable<int> newThesisKeywordsList, string departmentName)
        {
            IEnumerable<Thesis> oldTheses = this._db.ThesisRepository.GetAllThesesWithKeywordsFromDepartment(departmentName);//Get all professors from selected department
            
            if (oldTheses.Count() > 0)
            {

                //Get keywords from checkbox list from Index view
                List<int> keywordsList = new List<int>(newThesisKeywordsList);

                int currentOldThesis = 0;
                int fullMatch = 0; //full match between user keyword and thesis keyword
                int diferentFromZero = 0;//full match or with same node
                float oldThesisRank = 0.0f;
                
                IEnumerable<KeywordBasicModel> newThesisKeywords = this._db.KeywordRepository.GetUserKeywordsAndFields(keywordsList);//Get keywords from UserKeywords

                foreach (Thesis oldThesis in oldTheses)
                {
                    
                    if (string.Equals(oldThesis.ThesisTitle, newThesisTitle, StringComparison.OrdinalIgnoreCase) || 
                        string.Equals(oldThesis.ThesisDescription, newThesisDescription, StringComparison.OrdinalIgnoreCase))
                    {
                        isUniqueThesis = false;
                        return oldThesis.ThesisId;
                    }
                    foreach (Keyword oldThesisKeyword in oldThesis.Keywords)
                    {
                        //foreach every keywords from user
                        foreach (KeywordBasicModel newThesisKeyword in newThesisKeywords)
                        {
                            //compare if they are equal (if compare return 0 they are equal)
                            if (string.Compare(oldThesisKeyword.Value, newThesisKeyword.Value) == 0)
                            {
                                oldThesisRank += 1.0f;
                                fullMatch++;
                                diferentFromZero++;
                            }
                            //compare if they have same node
                            else if (oldThesisKeyword.FieldId == newThesisKeyword.FieldId)
                            {
                                oldThesisRank += 0.5f;
                                diferentFromZero++;
                            }
                            //if they dont equal and they dont have same node
                            else
                            {
                                oldThesisRank += 0.0f;
                            }
                        }
                    }

                    if (fullMatch == newThesisKeywords.Count() || fullMatch == oldThesis.Keywords.Count)
                    {
                        //Similarity 100%
                        return oldThesis.ThesisId;
                    }
                    else
                    {
                        oldThesisRank = (oldThesisRank / diferentFromZero) * 100;

                        if (oldThesisRank > ThesisConstraints.MAXIMUM_THESIS_SIMILARITY)
                        {
                            return oldThesis.ThesisId;
                        }

                    }
                    //For every thesis
                    fullMatch = 0;
                    oldThesisRank = 0.0f;
                    diferentFromZero = 0;
                    currentOldThesis++;
                }
            }
            return 0;
        }
    }
}
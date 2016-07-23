using MerchantEnrolmentPortal.Common.Utility;
using MerchantEnrolmentPortal.Helper;
using MerchantEnrolmentPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerchantEnrolmentPortal.Controllers
{
    public class ManageValidationRulesController : Controller
    {
        // GET: ManageValidationRules
        public ActionResult Index()
        {
            var codesetList = SessionStateFacade.GetSessionList<CodesetModel>(SessionName.CodeSetList);
            return View(codesetList);
        }

        /// <summary>
        /// Create Validation Rules
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Add New Validation Rules
        /// </summary>
        /// <param name="codesetModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CodesetModel codesetModel)
        {
            if (!ModelState.IsValid) return View();
            var merchantEnrolmentList = ValidationRulesRepository.Save(codesetModel);
            var responseMerchantEnrolmentModel = merchantEnrolmentList.OrderByDescending(x => x.RuleId).FirstOrDefault();
            return View(responseMerchantEnrolmentModel);
        }

        /// <summary>
        /// Edit Validation Rules
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var ruleList = SessionStateFacade.GetSessionList<CodesetModel>(SessionName.CodeSetList);
            var codesetModel = ruleList.Where(x => x.RuleId == id).FirstOrDefault();
            return View(codesetModel);
        }

        /// <summary>
        /// Edit Validation Rules
        /// </summary>
        /// <param name="codesetModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CodesetModel codesetModel)
        {
            if (!ModelState.IsValid) return View(codesetModel);
            var ruleModel = ValidationRulesRepository.Save(codesetModel);
            return View(codesetModel);
        }

        /// <summary>
        /// View Validation Rules
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(int? id)
        {
            var ruleList = SessionStateFacade.GetSessionList<CodesetModel>(SessionName.CodeSetList);
            var ruleModel = ruleList.Where(x => x.RuleId == id).FirstOrDefault();
            return View(ruleModel);
        }

        /// <summary>
        /// Delete Validation Rules
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue) return View();
            ValidationRulesRepository.Remove(id.HasValue ? Convert.ToInt32(id) : 0);

            return RedirectToAction("Index");
        }

    }
}
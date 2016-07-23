using MerchantEnrolmentPortal.Binder;
using MerchantEnrolmentPortal.Common.Utility;
using MerchantEnrolmentPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MerchantEnrolmentPortal.Controllers
{
    public class MerchantEnrolmentController : Controller
    {
        // GET: MerchantEnrolment
        public ActionResult Index(string option, string search, int? pageNumber)
        {
            IPagedList<MerchantEnrolmentModel> merchantEnrolmentList = null;
            if (option != null && search != null)
                Search(option, search, pageNumber, ref merchantEnrolmentList);
            else
                merchantEnrolmentList = GridBinder.BindMerchnatEnrolmentGrid(string.Empty, string.Empty).ToPagedList(pageNumber ?? 1, 5);
            return View(merchantEnrolmentList);
        }

        /// <summary>
        /// Create New Enrolment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Add New Merchant
        /// </summary>
        /// <param name="merchantEnrolmentModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(MerchantEnrolmentModel merchantEnrolmentModel)
        {
            if (merchantEnrolmentModel == null) return View();
            if (!ModelState.IsValid) return View();
            merchantEnrolmentModel.CreatedBy = !string.IsNullOrEmpty(merchantEnrolmentModel.CreatedBy) ? merchantEnrolmentModel.CreatedBy : SessionStateFacade.GetSession(SessionName.AKAName);
            merchantEnrolmentModel.CreatedOn = !string.IsNullOrEmpty(Convert.ToString(merchantEnrolmentModel.CreatedOn)) ? merchantEnrolmentModel.CreatedOn : DateTime.Now;
            var merchantEnrolmentList = GridBinder.SaveMerchantEnrolmentDetails(merchantEnrolmentModel);
            var responseMerchantEnrolmentModel = merchantEnrolmentList.OrderByDescending(x => x.MerchantId).FirstOrDefault();
            return View(responseMerchantEnrolmentModel);
        }

        /// <summary>
        /// Get Merchant for Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            IEnumerable<MerchantEnrolmentModel> merchantEnrolmentList = GridBinder.BindMerchnatEnrolmentGrid(string.Empty, string.Empty);
            var merchantEnrolmentModel = merchantEnrolmentList.Where(x => x.MerchantId == id).FirstOrDefault();
            return View(merchantEnrolmentModel);
        }

        /// <summary>
        /// Edit Merchant Details
        /// </summary>
        /// <param name="merchantEnrolmentModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(MerchantEnrolmentModel merchantEnrolmentModel)
        {
            var tempMerchantenrolments = SessionStateFacade.GetSessionList<MerchantEnrolmentModel>(SessionName.EnrollmentList);
            if (tempMerchantenrolments == null || tempMerchantenrolments.Count == 0)
                tempMerchantenrolments = GetAll().ToList();
            var itemToEdit = tempMerchantenrolments.SingleOrDefault(r => r.MerchantId == merchantEnrolmentModel.MerchantId);
            if (itemToEdit != null)
                tempMerchantenrolments.Remove(itemToEdit);

            var isDuplicate = tempMerchantenrolments.Any(x => (!string.IsNullOrEmpty(x.MerchantName) && x.MerchantName.Trim().Equals(merchantEnrolmentModel.MerchantName.ToString().Trim())));

            if (!isDuplicate)
            {
                var valueToClean = ModelState["MerchantName"];
                if (valueToClean != null && valueToClean.Errors != null)
                    valueToClean.Errors.Clear();
            }
            if (!ModelState.IsValid) return View(merchantEnrolmentModel);
            var merchantEnrolmentList = GridBinder.SaveMerchantEnrolmentDetails(merchantEnrolmentModel);
            return View(merchantEnrolmentModel);
        }

        /// <summary>
        /// View Merchant Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(int? id)
        {
            IEnumerable<MerchantEnrolmentModel> merchantEnrolmentList = GridBinder.BindMerchnatEnrolmentGrid(string.Empty, string.Empty);
            var merchantEnrolmentModel = merchantEnrolmentList.Where(x => x.MerchantId == id).FirstOrDefault();
            return View(merchantEnrolmentModel);
        }

        /// <summary>
        /// Remove Merchant Enrolment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue) return View();
            GridBinder.RemoveMerchantEnrolmentDetails(id.HasValue ? Convert.ToInt32(id) : 0);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// For Paging - Search
        /// </summary>
        /// <param name="option"></param>
        /// <param name="search"></param>
        /// <param name="pageNumber"></param>
        /// <param name="merchantSearchedList"></param>
        /// <returns></returns>
        private IPagedList<MerchantEnrolmentModel> Search(string option, string search, int? pageNumber, ref IPagedList<MerchantEnrolmentModel> merchantSearchedList)
        {
            var merchantEnrolments = SessionStateFacade.GetSessionList<MerchantEnrolmentModel>(SessionName.EnrollmentList);
            //if a user choose the radio button option as   
            if (option == "MerchantName")
                //Index action method will return a view with a records based on what a user specify the value in textbox  
                merchantSearchedList = merchantEnrolments.Where(x => x.MerchantName == search || search == null).ToPagedList(pageNumber ?? 1, 5);
            else if (option == "Country")
                merchantSearchedList = merchantEnrolments.Where(x => x.Country == search || search == null).ToPagedList(pageNumber ?? 1, 5);
            else
                merchantSearchedList = merchantEnrolments.ToPagedList(pageNumber ?? 1, 5);

            return merchantSearchedList;
        }

        public IEnumerable<MerchantEnrolmentModel> GetAll()
        {
            return GridBinder.BindMerchnatEnrolmentGrid(string.Empty, string.Empty);
        }

    }
}
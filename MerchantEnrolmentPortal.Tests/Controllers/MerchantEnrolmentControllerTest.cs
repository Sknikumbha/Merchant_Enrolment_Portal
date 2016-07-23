using MerchantEnrolmentPortal.Binder;
using MerchantEnrolmentPortal.Common.Utility;
using MerchantEnrolmentPortal.Controllers;
using MerchantEnrolmentPortal.Helper;
using MerchantEnrolmentPortal.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MerchantEnrolmentPortal.Tests.Controllers
{
    [TestClass]
    public class MerchantEnrolmentControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var controller = new MerchantEnrolmentController();
            var result = (ViewResult)controller.Index(string.Empty, string.Empty, 5);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable));
        }

        [TestMethod]
        public void Create()
        {
            var controller = new MerchantEnrolmentController();
            var merchantEnrolmentModel = new MerchantEnrolmentModel();
            var response = (ViewResult)controller.Create(merchantEnrolmentModel);
            var enrollmentModel = response.Model;
            Assert.IsNotNull(enrollmentModel);
            //Assert.AreEqual(merchantEnrolmentModel, enrollmentModel);
        }

        [TestMethod]
        public void Edit()
        {
            var controller = new MerchantEnrolmentController();
            var merchantEnrolmentModel = new MerchantEnrolmentModel();
            //var response = (ViewResult)controller.Edit(merchantEnrolmentModel);
            //var enrollmentModel = response.Model;
            //Assert.IsNotNull(enrollmentModel);

            int id = 1;
            string expectedView = "_Edit";
            var merchantEnrol = new MerchantEnrolmentModel()
            {
                MerchantId = 1,
                MerchantName = "Bank-E",
                Country = "England",
                MerchantProfit = Convert.ToDecimal("10,000"),
                NumberOfOutlets = 15,
                CreatedBy = "Joe bloggs",
                CreatedOn = Convert.ToDateTime("01/01/2016")
            };
            var merchantEnrol1 = GridBinder.BindMerchnatEnrolmentGrid(merchantEnrol.MerchantName, string.Empty);
            var expectedVm = new MerchantEnrolmentModel()
            {
                MerchantId = merchantEnrol.MerchantId,
                MerchantName = merchantEnrol.MerchantName,
                Country = merchantEnrol.Country,
                MerchantProfit = merchantEnrol.MerchantProfit,
                NumberOfOutlets = merchantEnrol.NumberOfOutlets,
                CreatedBy = merchantEnrol.CreatedBy,
                CreatedOn = merchantEnrol.CreatedOn
            };

            var actual = (ViewResult)controller.Edit(id);
            var actualVm = actual.Model as MerchantEnrolmentModel;

            //Assert.AreEqual(expectedView, actual.ViewName);
            Assert.AreEqual(expectedVm.MerchantName, actualVm.MerchantName);
            Assert.AreEqual(expectedVm.MerchantProfit, actualVm.MerchantProfit);
            Assert.AreEqual(expectedVm.MerchantId, actualVm.MerchantId);
            Assert.AreEqual(expectedVm.NumberOfOutlets, actualVm.NumberOfOutlets);
        }

        [TestMethod]
        public void Delete()
        {
            var controller = new MerchantEnrolmentController();
            var merchantEnrolmentModel = new MerchantEnrolmentModel();
            var response = (ViewResult)controller.Delete(1);
            var enrollmentModel = response.Model;
            Assert.IsNotNull(enrollmentModel);
        }

        //1.Model validation

        [TestMethod]
        public void TestValidation()
        {
            var model = new MerchantEnrolmentModel();
            // Set some properties here
            var context = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(model, context, results, true);

            // Assert here
        }
        //2.Controller with invalid modelstate

        [TestMethod]
        public void CreateTest()
        {
            var merchantController = new MerchantEnrolmentController();
            var merchantEnrol = new MerchantEnrolmentModel()
            {
                MerchantId = 0,
                MerchantName = "Bank-HDFC",
                Country = "England",
                MerchantProfit = Convert.ToDecimal("10,000"),
                NumberOfOutlets = 15,
                CreatedBy = "Joe bloggs",
                CreatedOn = Convert.ToDateTime("01/01/2016")
            };

            merchantController.Create(merchantEnrol);

            var merchantEnrolments = SessionStateFacade.GetSessionList<MerchantEnrolmentModel>(SessionName.EnrollmentList);
            CollectionAssert.Contains(merchantEnrolments, merchantEnrol);
        }

        [TestMethod]
        public void EditTest()
        {
            var merchantController = new MerchantEnrolmentController();
            var merchantEnrol = new MerchantEnrolmentModel()
            {
                MerchantId = 1,
                MerchantName = "Bank-ICICI",
                Country = "England",
                MerchantProfit = Convert.ToDecimal("10,000"),
                NumberOfOutlets = 15,
                CreatedBy = "Joe bloggs",
                CreatedOn = Convert.ToDateTime("01/01/2016")
            };

            // Lets call the action method now
            var response = (ViewResult)merchantController.Edit(merchantEnrol);
            var merchantEnrolments = merchantController.GetAll().ToList<MerchantEnrolmentModel>();//SessionStateFacade.GetSessionList<MerchantEnrolmentModel>(SessionName.EnrollmentList);
            var merchantEnrolment = response.Model;
            //CollectionAssert.Contains(merchantEnrolments, merchantEnrolment);
            //Assert.Equals(merchantEnrolment, merchantEnrol);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var merchantController = new MerchantEnrolmentController();
            //var merchantEnrolments = ValidationRulesRepository.Get();
            var merchantEnrolments = merchantController.GetAll().ToList();//SessionStateFacade.GetSessionList<MerchantEnrolmentModel>(SessionName.EnrollmentList);
            var itemToRemove = merchantEnrolments.FirstOrDefault(x=>x.MerchantId == 5);
            merchantController.Delete(5);
            var merchantEnrolmentsA = merchantController.GetAll().ToList();
            CollectionAssert.DoesNotContain(merchantEnrolmentsA, itemToRemove);
        }
    }
}

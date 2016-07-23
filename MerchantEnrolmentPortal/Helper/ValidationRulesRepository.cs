using MerchantEnrolmentPortal.Common.Utility;
using MerchantEnrolmentPortal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace MerchantEnrolmentPortal.Helper
{
    public static class ValidationRulesRepository
    {
        static List<CodesetModel> validationRules;
        private static int _nextId = 3;
        //static DateTime startDate;
        //static DateTime endDate;
        //static CodesetDto codesetDto;
        static ValidationRulesRepository()
        {
            validationRules = new List<CodesetModel>();

            validationRules.Add(new CodesetModel { RuleId = 1, Country = "France", StartDate = Convert.ToDateTime("10/01/2016"), EndDate = Convert.ToDateTime("24/02/2016") });
            validationRules.Add(new CodesetModel { RuleId = 2, Country = "Spain", StartDate = Convert.ToDateTime("15/02/2016"), EndDate = Convert.ToDateTime("01/04/2016") });

            SessionStateFacade.SetSessionList(SessionName.CodeSetList, validationRules);

        }

        public static List<CodesetModel> Get()
        {
            return validationRules;
        }

        public static List<CodesetModel> Save(CodesetModel codesetDto)
        {
            if (validationRules == null)
                validationRules = new List<CodesetModel>();
            if (codesetDto.RuleId == null || codesetDto.RuleId == 0)
            {
                codesetDto.RuleId = _nextId++;
                validationRules.Add(codesetDto);
            }
            else
            {
                //Update operation
                var found = validationRules.FirstOrDefault(x => x.RuleId == codesetDto.RuleId);
                int i = validationRules.IndexOf(found);
                validationRules[i] = codesetDto;
            }
            SessionStateFacade.SetSessionList(SessionName.CodeSetList, validationRules);

            return validationRules;
        }
        public static List<CodesetModel> Remove(int ruleId)
        {
            if (validationRules != null && validationRules.Count > 0)
            {
                var found = validationRules.FirstOrDefault(x => x.RuleId == ruleId);
                int i = validationRules.IndexOf(found);
                validationRules.Remove(found);
            }
            SessionStateFacade.SetSessionList(SessionName.CodeSetList, validationRules);

            return validationRules;
        }
    }
}
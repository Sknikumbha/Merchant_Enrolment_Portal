using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantEnrolmentPortal.Common.Utility
{
    public class SessionStateFacade
    {
        private static SessionStateFacade _instance;
        private static string _sessionName;

        //get the instance of session
        public static SessionStateFacade Instance
        {
            get { return _instance ?? (_instance = new SessionStateFacade()); }
        }

        //To reset the session
        public static void Reset()
        {
            if (Instance == null) return;
            foreach (var ses in Enum.GetValues(typeof(SessionName)))
            {
                _sessionName = ses.ToString();
                HttpContext.Current.Session[_sessionName] = null;
            }
        }

        /// <summary>
        /// Sets Session String
        /// </summary>
        /// <param name="type"></param>
        /// <param name="setValue"></param>
        public static void SetSession(SessionName type, string setValue)
        {
            _sessionName = type.ToString();
            if (HttpContext.Current != null)
                HttpContext.Current.Session[_sessionName] = setValue;
        }

        /// <summary>
        /// Get the session String
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSession(SessionName type)
        {
            _sessionName = type.ToString();
            return (HttpContext.Current != null) ? (string)((HttpContext.Current.Session[_sessionName] != null) ? HttpContext.Current.Session[_sessionName] : null) : null;
        }


        /// <summary>
        /// Set the session Object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static void SetSessionList<T>(SessionName sessionName, List<T> setList)
        {
            _sessionName = sessionName.ToString();
            if (HttpContext.Current != null)
                HttpContext.Current.Session[_sessionName] = setList;
        }

        /// <summary>
        /// Get the session Object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<T> GetSessionList<T>(SessionName type)
        {
            _sessionName = type.ToString();
            return (HttpContext.Current != null) ? (List<T>)(HttpContext.Current.Session[_sessionName] != null ? HttpContext.Current.Session[_sessionName] : new List<T>()) : new List<T>();
        }
    }
}
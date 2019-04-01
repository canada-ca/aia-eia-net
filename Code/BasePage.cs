using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace AIA
{
    public class BasePage : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            // base culture
            string culture = "en-CA";

            // querystring parameter
            var query = HttpContext.Current.Request.QueryString["lang"];
            var cookie = Request.Cookies["AIACulture"];

            if (!String.IsNullOrEmpty(query))
            {
                culture = query.Equals("fr") ? "fr-CA" : "en-CA";
            }
            else if (cookie != null && cookie.Value != null)
            {
                culture = cookie.Value;
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);

            base.InitializeCulture();
        }

        /// <summary>
        /// Date of creation of the resource.
        /// </summary>
        /// <remarks>
        /// Default is file system's creation date.
        /// </remarks>
        public virtual string Created
        {
            get
            {
                string str = ViewState["PageDateCreated"] as string;
                if (str == null)
                {
                    System.IO.FileInfo objInfo = new System.IO.FileInfo(Server.MapPath(Request.ServerVariables.Get("SCRIPT_NAME")));
                    return String.Format("{0:yyyy-MM-dd}", objInfo.CreationTime.Date);
                }
                else
                    return str;
            }
            set
            {
                ViewState["PageDateCreated"] = value;
            }
        }

        /// <summary>
        /// Date on which the resource was changed.
        /// </summary>
        /// <remarks>
        /// Default is file system's last write time date.
        /// </remarks>
        public virtual string Modified
        {
            get
            {
                string str = ViewState["PageDateModified"] as string;
                if (str == null)
                {
                    string pageURL = Request.ServerVariables.Get("SCRIPT_NAME");
                    pageURL = (pageURL.Substring(pageURL.Length - 5) == ".aspx") ? pageURL : pageURL + ".aspx";
                    System.IO.FileInfo objInfo = new System.IO.FileInfo(Server.MapPath(pageURL));
                    objInfo.Refresh();
                    return String.Format("{0:yyyy-MM-dd}", objInfo.LastWriteTime.Date);
                }
                else
                    return str;
            }
            set
            {
                ViewState["PageDateModified"] = value;
            }
        }

        /// <summary>
        /// A language of the resource.
        /// </summary>
        /// <remarks>
        /// Two-letter language. Default is English ("en").
        /// </remarks>
        public virtual string Language
        {
            get
            {
                string str = ViewState["PageLanguage"] as string;
                if (str == null)
                {
                    return Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
                }
                else
                    return str;
            }
            set
            {
                ViewState["PageLanguage"] = value;
            }
        }
    }
}
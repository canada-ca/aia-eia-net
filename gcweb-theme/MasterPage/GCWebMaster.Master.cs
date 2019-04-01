using AIA;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetGabaritGCWeb.gcweb_theme
{
    public partial class GCWebMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                textSearch.Attributes.Add("placeholder", GetLocalResourceObject("searchPlaceholder").ToString());
            }
        }

        public string PageModified
        {
            get { return ((BasePage)Page).Modified; }
        }

        public string TwoLetterLangName
        {
            get { return ((BasePage)Page).Language; }
        }

        protected void lnklanguage_Click(object sender, EventArgs e)
        {
            string lang = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            string culture = lang == "fr" ? "en-CA" : "fr-CA";

            HttpCookie cookie = new HttpCookie("AIACulture");
            cookie.Value = culture;
            Response.Cookies.Add(cookie);

            Response.Redirect(this.Page.Request.Url.ToString(), true);
        }

        protected void btnSearchSubmit_ServerClick(object sender, EventArgs e)
        {
            string url = string.Format("https://www.canada.ca/{0}/sr/srb.html?cdn=canada&st=s&num=10&langs={0}&st1rt=1&s5bm3ts21rch=x&q={1}&_charset_=utf-8&wb-srch-sub=", this.TwoLetterLangName, textSearch.Text.Trim());
            Response.Redirect(url);
        }
    }
}
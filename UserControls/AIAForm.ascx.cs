using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AIA.UserControls
{
    public partial class AIAForm : System.Web.UI.UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            // load
            this.LoadForm();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        private void LoadForm()
        {
            try
            {
                using (var context = new Models.Entities())
                {
                    _f = context.Forms.Include("Sections").Include("Sections.Questions").Include("Sections.Questions.Choices").Include("Conditions").FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            InitFormCtrls();
            SetSection(_currentIndex, true);
        }

        private void InitFormCtrls()
        {
            // one or more sections
            if (_f != null && _f.Sections != null && _f.Sections.Count > 0)
            {
                // set form title
                lblFormTitle.Text = this.FormTitle;
                // set last date modified
                lblLastDateModified.Text = string.Format("{0} : {1}", Resources.Global.lastDateModified, this.FormLastDateModified);

                int i = 0;
                Panel pnlSection;

                foreach (Models.Section currentSection in _f.Sections.OrderBy(x => x.position))
                {
                    pnlSection = new Panel() { ID = "pnlSection" + i, Visible = false };
                    pnlBody.Controls.Add(pnlSection);

                    // show title
                    Label title = new Label() { CssClass = "panel-title", Text = _lang.Equals("fr") ? currentSection.titleFR : currentSection.titleEN };
                    pnlSection.Controls.Add(title);

                    // one or more questions
                    if (currentSection.Questions != null && currentSection.Questions.Count > 0)
                    {
                        bool isVisible;
                        ListControl listCtrl;
                        foreach (Models.Question q in currentSection.Questions.OrderBy(x => x.position))
                        {
                            isVisible = (_f.Conditions.Where(x => x.destinationQuestionID == q.questionID).Count() == 0);
                            switch (q.questionTypeID)
                            {
                                case 1:
                                    // free text
                                    pnlSection.Controls.Add(new WetControls.Controls.WetTextbox()
                                    {
                                        ID = q.questionID.ToString(),
                                        LabelText = string.Format("{0}. {1}", q.questionNumber, (_lang.Equals("fr") ? q.labelFR : q.labelEN)),
                                        IsRequired = q.isRequired,
                                        Visible = isVisible
                                    });
                                    break;
                                case 2:
                                    // dropdown list
                                    listCtrl = new WetControls.Controls.WetDropDownList()
                                    {
                                        ID = q.questionID.ToString(),
                                        LabelText = string.Format("{0}. {1}", q.questionNumber, (_lang.Equals("fr") ? q.labelFR : q.labelEN)),
                                        IsRequired = q.isRequired,
                                        Visible = isVisible
                                    };
                                    listCtrl.DataTextField = _lang.Equals("fr") ? "textFR" : "textEN";
                                    listCtrl.DataValueField = "choiceID";
                                    listCtrl.DataSource = q.Choices.OrderBy(x => x.position).ThenBy(x => _lang.Equals("fr") ? x.textFR : x.textEN).ToList();
                                    listCtrl.DataBind();
                                    pnlSection.Controls.Add(listCtrl);
                                    // condition
                                    if (_f.Conditions.Where(x => x.sourceQuestionID == q.questionID).Count() > 0)
                                    {
                                        listCtrl.AutoPostBack = true;
                                        listCtrl.SelectedIndexChanged += this.Choice_OnSelectedIndexChanged;
                                        upForm.Triggers.Add(new AsyncPostBackTrigger() { ControlID = listCtrl.ID, EventName = "SelectedIndexChanged" });
                                    }
                                    break;
                                case 3:
                                    // select one
                                    listCtrl = new WetControls.Controls.WetRadioButtonList()
                                    {
                                        ID = q.questionID.ToString(),
                                        LabelText = string.Format("{0}. {1}", q.questionNumber, (_lang.Equals("fr") ? q.labelFR : q.labelEN)),
                                        IsRequired = q.isRequired,
                                        RepeatDirection = RepeatDirection.Horizontal,
                                        Visible = isVisible
                                    };
                                    listCtrl.DataTextField = _lang.Equals("fr") ? "textFR" : "textEN";
                                    listCtrl.DataValueField = "choiceID";
                                    listCtrl.DataSource = q.Choices.OrderBy(x => x.position).ThenBy(x => _lang.Equals("fr") ? x.textFR : x.textEN).ToList();
                                    listCtrl.DataBind();
                                    pnlSection.Controls.Add(listCtrl);
                                    if (_f.Conditions.Where(x => x.sourceQuestionID == q.questionID).Count() > 0)
                                    {
                                        listCtrl.AutoPostBack = true;
                                        listCtrl.SelectedIndexChanged += this.Choice_OnSelectedIndexChanged;
                                        upForm.Triggers.Add(new AsyncPostBackTrigger() { ControlID = listCtrl.ID, EventName = "SelectedIndexChanged" });
                                    }
                                    break;
                                case 4:
                                    // multiple choice
                                    listCtrl = new WetControls.Controls.WetCheckBoxList()
                                    {
                                        ID = q.questionID.ToString(),
                                        LabelText = string.Format("{0}. {1}", q.questionNumber, (_lang.Equals("fr") ? q.labelFR : q.labelEN)),
                                        RepeatLayout = RepeatLayout.Flow,
                                        IsRequired = q.isRequired,
                                        Visible = isVisible
                                    };
                                    listCtrl.DataTextField = _lang.Equals("fr") ? "textFR" : "textEN";
                                    listCtrl.DataValueField = "choiceID";
                                    listCtrl.DataSource = q.Choices.OrderBy(x => x.position).ThenBy(x => _lang.Equals("fr") ? x.textFR : x.textEN).ToList();
                                    listCtrl.DataBind();
                                    pnlSection.Controls.Add(listCtrl);
                                    if (_f.Conditions.Where(x => x.sourceQuestionID == q.questionID).Count() > 0)
                                    {
                                        listCtrl.AutoPostBack = true;
                                        listCtrl.SelectedIndexChanged += this.Choice_OnSelectedIndexChanged;
                                        upForm.Triggers.Add(new AsyncPostBackTrigger() { ControlID = listCtrl.ID, EventName = "SelectedIndexChanged" });
                                    }
                                    break;
                                case 5:
                                    // description
                                    pnlSection.Controls.Add(new WetControls.Controls.WetTextbox()
                                    {
                                        ID = q.questionID.ToString(),
                                        LabelText = string.Format("{0}. {1}", q.questionNumber, (_lang.Equals("fr") ? q.labelFR : q.labelEN)),
                                        IsRequired = q.isRequired,
                                        Rows = 3,
                                        TextMode = TextBoxMode.MultiLine,
                                        Visible = isVisible
                                    });
                                    break;
                            }
                        }
                    }
                    i++;
                }

                // previous button
                WetControls.Controls.WetButton btnPrevious = new WetControls.Controls.WetButton()
                {
                    ID = "btnPrevious",
                    Text = Resources.Global.previous,
                    CssClass = "mrgn-rght-md",
                    Visible = false,
                    EnableClientValidation = false
                };
                pnlFooter.Controls.Add(btnPrevious);
                btnPrevious.Click += this.BtntPrevious_OnClick;
                upForm.Triggers.Add(new PostBackTrigger() { ControlID = "btnPrevious" });

                // next button
                WetControls.Controls.WetButton btnNext = new WetControls.Controls.WetButton()
                {
                    ID = "btnNext",
                    Text = Resources.Global.next
                };
                pnlFooter.Controls.Add(btnNext);
                btnNext.Click += this.BtntNext_OnClick;
                upForm.Triggers.Add(new PostBackTrigger() { ControlID = "btnNext" });

                // finish button
                WetControls.Controls.WetButton btnFinish = new WetControls.Controls.WetButton()
                {
                    ID = "btnFinish",
                    Text = Resources.Global.finish,
                    ButtonType = WetControls.Controls.WetButton.BUTTON_TYPE.Primary,
                    Visible = false
                };
                pnlFooter.Controls.Add(btnFinish);
                btnFinish.Click += this.BtntFinish_OnClick;
                upForm.Triggers.Add(new PostBackTrigger() { ControlID = "btnFinish" });
            }
        }

        private void SetSection(int index, bool visible)
        {
            // manage section
            pnlBody.FindControl("pnlSection" + index).Visible = visible;
        }

        private void SetButtons(int index)
        {
            Control btnPrevious = (Control)pnlBody.FindControl("pnlFooter").FindControl("btnPrevious");
            Control btnNext = (Control)pnlBody.FindControl("pnlFooter").FindControl("btnNext");
            Control btnFinish = (Control)pnlBody.FindControl("pnlFooter").FindControl("btnFinish");

            btnPrevious.Visible = true;
            btnNext.Visible = true;
            btnFinish.Visible = false;

            if (index == 0)
            {
                btnPrevious.Visible = false;
            }
            else if (index == _f.Sections.Count - 1)
            {
                btnNext.Visible = false;
                btnFinish.Visible = true;
            }
        }

        // EVENTS
        protected void BtntNext_OnClick(object sender, EventArgs e)
        {
            SetSection(CurrentIndex, false);
            CurrentIndex++;
            SetSection(CurrentIndex, true);
            SetButtons(CurrentIndex);
        }
        protected void BtntPrevious_OnClick(object sender, EventArgs e)
        {
            SetSection(CurrentIndex, false);
            CurrentIndex--;
            SetSection(CurrentIndex, true);
            SetButtons(CurrentIndex);
        }
        protected void BtntFinish_OnClick(object sender, EventArgs e)
        {

        }

        protected void Choice_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ListControl ctrl = (ListControl)sender;

            List<Models.Condition> conditions;
            // get the condition for this question
            try
            {
                int questionId = int.Parse(ctrl.ID);
                using (var context = new Models.Entities())
                {
                    conditions = context.Conditions.Where(x => x.sourceQuestionID == questionId).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (conditions.Count > 0)
            {
                // target control
                Control ctrlDestination;

                foreach (Models.Condition cond in conditions)
                {
                    ctrlDestination = ctrl.Parent.FindControl(cond.destinationQuestionID.ToString());

                    var item = ctrl.Items.FindByValue(cond.sourceChoiceID.ToString());
                    ctrlDestination.Visible = item.Selected;
                }
            }
        }

        // PRIVATE PROPERTIES
        private Models.Form _f;
        private int _currentIndex = 0;
        private string _lang
        {
            get { return System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName; }
        }
        private string FormTitle
        {
            get { return _lang.Equals("fr") ? _f.nameFR : _f.nameEN; }
        }
        private string FormLastDateModified
        {
            get { return _f.dateModified.HasValue ? _f.dateModified.Value.ToLongDateString() : string.Empty; }
        }
        private int SectionCount
        {
            get { return _f.Sections == null ? 0 : _f.Sections.Count; }
        }

        // PUBLIC PROPERTIES
        public int CurrentIndex
        {
            get
            {
                if (ViewState["CurrentIndex"] != null)
                {
                    _currentIndex = (int)ViewState["CurrentIndex"];
                }
                return _currentIndex;
            }
            set
            {
                ViewState["CurrentIndex"] = value;
                _currentIndex = value;
            }
        }
    }
}
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace CodeSmells.Bloaters.LongMethod
{
    public partial class ApproveProposal : BasePage
    {
        private void save_Click(object sender, System.EventArgs e)
        {
            if (dueDate.Text.Length < 1)
            {
                this.errorMessage.Text = "Please enter a due date.";
                this.errorMessage.Visible = true;
                return;
            }

            string errMsg = "";
            try
            {
                ArticleProposal proposal = new ArticleProposal(articleProposalId);

                if (proposal.ProposalText.Length < 1)
                {
                    this.errorMessage.Text = "Proposal not found.";
                }
                else
                {
                    User user = new User(proposal.AuthorId);

                    if (user != null)
                    {
                        proposal.DueDate = Convert.ToDateTime(this.dueDate.Text);

                        if (sendEmail.Checked)
                        {
                            string emailMsg = "Dear " + user.FirstName + " " + user.LastName + ",\n\n"
                                + "Thanks for your article proposal entitled \"" + proposal.ProposalTitle + ".\""
                                + "  It has been approved with an expected due date of " + proposal.DueDate.ToString("dd MMM yyyy")
                                + ".  If you find you will be unable to make the due date, please "
                                + "let us know."
                                + "\n\n"
                                + "When you are ready to submit your article, please do so using our online "
                                + "submission tool via the Submit Article link under the authors menu on our Web site."
                                + "\n\n"
                                + "Thank you for choosing to publish with Acme.  We look forward to working with you."
                                + "\n\n"
                                + "Cordially,"
                                + "\n\n"
                                + "The Acme Editor Team"
                                ;

                            SendMessage(
                                System.Configuration.ConfigurationSettings.AppSettings["sendArticleSubmissionsFrom"]
                                , user.EmailAddress
                                , "Acme.Com Article Proposal Accepted"
                                , emailMsg
                                );
                        }

                        proposal.Save();

                        this.articleProposalInfo.Refresh();

                        this.errorMessage.Text = "Proposal due date processed.";
                        this.dueDate.Enabled = false;
                        this.save.Enabled = false;

                    }
                    else
                        this.errorMessage.Text = "Author " + proposal.AuthorId.ToString() + " not found";
                }

                this.errorMessage.Visible = true;

            }
            catch (Exception exc)
            {
                WriteError("approveProposal.asax.cs::next_Click : " + exc.ToString());
                this.errorMessage.Text = "An exception occured while saving your information. Please try again later.";
                this.errorMessage.Visible = true;
            }
        }
    }

    public partial class ApproveProposal2 : BasePage
    {
        private ArticleProposalApprovalService _approvalService = new ArticleProposalApprovalService();
        private void save_Click(object sender, System.EventArgs e)
        {
            if (dueDate.Text.Length < 1)
            {
                this.errorMessage.Text = "Please enter a due date.";
                this.errorMessage.Visible = true;
                return;
            }

            var result = _approvalService.Save(articleProposalId, dueDate.Text, sendEmail.Checked);

            if (result.IsSuccessful)
            {
                this.articleProposalInfo.Refresh();
                this.errorMessage.Text = "Proposal due date processed.";
                this.dueDate.Enabled = false;
                this.save.Enabled = false;
            }
            else
            {
                this.errorMessage.Text = result.ResultMessage;
            }
            this.errorMessage.Visible = true;
        }
    }

    public class AuthorNotificationService
    {
        private MessageTemplateService _messageTemplateService = new MessageTemplateService();

        public void NotifyAuthorProposalAccepted(User user, ArticleProposal proposal)
        {
            string emailMsg = _messageTemplateService.GenerateProposalApprovalEmailBody(user, proposal);
            SendMessage(
                System.Configuration.ConfigurationSettings.AppSettings["sendArticleSubmissionsFrom"]
                , user.EmailAddress
                , "Acme.Com Article Proposal Accepted"
                , emailMsg
                );
        }
  
        private void SendMessage(string appSettings, string emailAddress, string p, string emailMsg)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

    }
    public class MessageTemplateService
    {
        public string GenerateProposalApprovalEmailBody(User user, ArticleProposal proposal)
        {
            return "Dear " + user.FirstName + " " + user.LastName + ",\n\n"
                                            + "Thanks for your article proposal entitled \"" + proposal.ProposalTitle + ".\""
                                            + "  It has been approved with an expected due date of " + proposal.DueDate.ToString("dd MMM yyyy")
                                            + ".  If you find you will be unable to make the due date, please "
                                            + "let us know."
                                            + "\n\n"
                                            + "When you are ready to submit your article, please do so using our online "
                                            + "submission tool via the Submit Article link under the authors menu on our Web site."
                                            + "\n\n"
                                            + "Thank you for choosing to publish with Acme.  We look forward to working with you."
                                            + "\n\n"
                                            + "Cordially,"
                                            + "\n\n"
                                            + "The Acme Editor Team"
                                            ;
        }
    }
    public class ArticleProposalApprovalService
    {
        private AuthorNotificationService _authorNotificationService = new AuthorNotificationService();
        public ArticleProposalSaveResult Save(int articleProposalId, string dueDate, bool shouldSendEmail)
        {
            var result = new ArticleProposalSaveResult();
            try
            {
                ArticleProposal proposal = new ArticleProposal(articleProposalId);

                if (proposal.ProposalText.Length < 1)
                {
                    result.ResultMessage = "Proposal not found.";
                    return result;
                }
                User user = new User(proposal.AuthorId);
                if (user == null)
                {
                    result.ResultMessage = "Author " + proposal.AuthorId.ToString() + " not found";
                    return result;
                }

                proposal.DueDate = Convert.ToDateTime(dueDate);

                if (shouldSendEmail)
                {
                    _authorNotificationService.NotifyAuthorProposalAccepted(user, proposal);

                }

                proposal.Save();

                result.IsSuccessful = true;
                return result;
            }
            catch (Exception ex)
            {
                WriteError("approveProposal.asax.cs::next_Click : " + ex.ToString());
                result.ResultMessage = "An exception occured while saving your information. Please try again later.";
                return result;
            }
        }
  
        private void WriteError(string toString)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
    public class ArticleProposalSaveResult
    {
        public bool IsSuccessful { get; set; }
        public string ResultMessage { get; set; }
    }

    #region SupportingFakes
    public partial class ApproveProposal
    {
        protected TextBox dueDate;
        protected Label errorMessage;
        protected CheckBox sendEmail;
        protected int articleProposalId;
        protected ArticleProposalControl articleProposalInfo;
        protected Button save;
    }
    public partial class ApproveProposal2
    {
        protected TextBox dueDate;
        protected Label errorMessage;
        protected CheckBox sendEmail;
        protected int articleProposalId;
        protected ArticleProposalControl articleProposalInfo;
        protected Button save;
    }

    public class ArticleProposal
    {
        public ArticleProposal(int articleProposalId)
        {

        }
        public string ProposalText { get; set; }
        public string ProposalTitle { get; set; }
        public int AuthorId { get; set; }
        public DateTime DueDate { get; set; }
        public void Save() { }
    }
    public class User
    {
        public User(int userId)
        {

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
    public class ArticleProposalControl : System.Web.UI.WebControls.WebControl
    {
        public void Refresh() { }
    }
    public class BasePage : System.Web.UI.Page
    {
        protected void SendMessage(string appSettings, string emailAddress, string p, string emailMsg)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        protected void WriteError(string toString)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
    #endregion

}

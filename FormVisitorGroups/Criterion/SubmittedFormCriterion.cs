using System;
using System.Security.Principal;
using System.Web;
using EPiServer.Forms.Helpers;
using EPiServer.Personalization.VisitorGroups;
using FormVisitorGroups.Models;

namespace FormVisitorGroups.Criterion
{
    [VisitorGroupCriterion(
    Category = "Episerver Forms",
    DisplayName = "Submitted Form",
    Description = "Criterion that checks if the visitor has submitted a specific Form (cookie based)",
    LanguagePath = "/formvisitorgroups/criteria/submittedformcriterion")]
    public class SubmittedFormCriterion : CriterionBase<SubmittedFormModel>
    {
        public override bool IsMatch(IPrincipal principal, HttpContextBase httpContext)
        {
            bool hasAlreadyPosted = FormsExtensions.HasAlreadyPosted(Guid.Parse(base.Model.SelectedForm), httpContext);
            return hasAlreadyPosted;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EPiServer.Forms.Core.Data;
using EPiServer.Forms.Core.Models;
using EPiServer.Forms.Helpers;
using EPiServer.Framework;
using EPiServer.Globalization;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.ServiceLocation;
using FormVisitorGroups.Enums;
using FormVisitorGroups.Impl;
using FormVisitorGroups.Models;

namespace FormVisitorGroups.Criterion
{
    [VisitorGroupCriterion(
        Category = "Episerver Forms",
        DisplayName = "Submitted form value",
        Description = "Check for the value submitted in form (the latest submission of the form by the user within the current session)",
        LanguagePath = "/formvisitorgroups/criteria/formvaluecriteria")]
    public class FormValueCriteria : CriterionBase<FormValueCriterionModel>
    {
        public override bool IsMatch(IPrincipal principal, HttpContextBase httpContext)
        {
            try
            {
                // Check that the form has been submitted by the user
                var formGuid = GetFormId(Model.FormField);
                var flag = FormsExtensions.HasAlreadyPosted(formGuid, httpContext);
                if (flag == false)
                {
                    return false;
                }

                var formSubmissionRepo = ServiceLocator.Current.GetInstance<ISubmittedFormsRepository>();
                if (formSubmissionRepo.IsActive() == false)
                {
                    return false;
                }

                // Get a list of form submission Ids
                var formSubmissionIdsList = formSubmissionRepo.GetFormSubmissionIdsList();

                // Note! Language isn't required to get SubmissionData from the default 
                // Form permanent store but constructor requires a language
                var submissions = ServiceLocator.Current
                    .GetInstance<FormDataManager>()
                    .GetSubmissionData(new FormIdentity(formGuid, "en"), formSubmissionIdsList.ToArray())
                    .ToList();

                // Get the field name to check and the form of that field in the form
                var fieldName = GetFieldId(Model.FormField);
                if (fieldName != string.Empty && submissions.Count > 0)
                {
                    var formValue = submissions[submissions.Count - 1].Data[fieldName].ToString();
                    return CheckForMatch(formValue, Model.Value);
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private bool CheckForMatch(string formValue, string criteriaValue)
        {
            switch (Model.Condition)
            {
                case FormValueCondition.HasValue:
                    return !String.IsNullOrWhiteSpace(formValue);

                case FormValueCondition.DoesNotHaveAValue:
                    return String.IsNullOrWhiteSpace(formValue);

                case FormValueCondition.Contains:
                    return (Model.CaseSensitive
                        ? formValue.IndexOf(criteriaValue, 0, StringComparison.CurrentCulture) >= 0
                        : formValue.IndexOf(criteriaValue, 0, StringComparison.CurrentCultureIgnoreCase) >= 0);

                case FormValueCondition.DoesNotContain:
                    return !(Model.CaseSensitive
                        ? formValue.IndexOf(criteriaValue, 0, StringComparison.CurrentCulture) >= 0
                        : formValue.IndexOf(criteriaValue, 0, StringComparison.CurrentCultureIgnoreCase) >= 0);

                case FormValueCondition.Is:
                    return (Model.CaseSensitive
                        ? criteriaValue.Equals(formValue, StringComparison.CurrentCulture)
                        : criteriaValue.Equals(formValue, StringComparison.CurrentCultureIgnoreCase));

                case FormValueCondition.IsNot:
                    return !(Model.CaseSensitive
                        ? criteriaValue.Equals(formValue, StringComparison.CurrentCulture)
                        : criteriaValue.Equals(formValue, StringComparison.CurrentCultureIgnoreCase));

                default:
                    return false;
            }
        }

        private Guid GetFormId(string value)
        {
            Guid formIdGuid;
            Guid.TryParse(Model.FormField.Split(">".ToCharArray())[0].Trim(), out formIdGuid);
            return formIdGuid;
        }

        private string GetFieldId(string value)
        {
            var formField = Model.FormField.Split(">".ToCharArray());
            if (formField.Length == 2)
            {
                return formField[1].Trim();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.Web.Mvc.VisitorGroups;
using FormVisitorGroups.Criterion;
using FormVisitorGroups.Enums;
using FormVisitorGroups.SelectionFactories;

namespace FormVisitorGroups.Models
{
    public class FormValueCriterionModel : CriterionModelBase
    {
        /// <summary>
        /// The name of the cookie to check
        /// </summary>
        [Required, DojoWidget(SelectionFactoryType = typeof(FormFieldSelectionFactory), LabelTranslationKey= "")]
        public string FormField { get; set; }

        /// <summary>
        /// The condition to apply when checking the form value
        /// </summary>
        [Required, DojoWidget(SelectionFactoryType = typeof(EnumSelectionFactory), LabelTranslationKey = "")]
        public FormValueCondition Condition { get; set; }

        /// <summary>
        /// The value to check for submitted form value
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Whether the value check is case senstive or not
        /// </summary>
        public bool CaseSensitive { get; set; }

        public override ICriterionModel Copy()
        {
            return base.ShallowCopy();
        }
    }
}
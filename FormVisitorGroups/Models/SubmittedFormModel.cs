using System.ComponentModel.DataAnnotations;
using EPiServer.Data;
using EPiServer.DataAnnotations;
using EPiServer.Personalization.VisitorGroups;
using FormVisitorGroups.Criterion;
using FormVisitorGroups.SelectionFactories;

namespace FormVisitorGroups.Models
{
    public class SubmittedFormModel : CriterionModelBase
    {
        //[Ignore]
        //public override Identity Id { get; set; }

        [Required, 
            DojoWidget(SelectionFactoryType = typeof(SubmittedFormSelectionFactory), 
            LabelTranslationKey = "/episerver/forms/samples/criteria/formcriterion/selectedform", 
            AdditionalOptions = "{ selectOnClick: true }")]
        public string SelectedForm { get; set; }

        public override ICriterionModel Copy()
        {
            return base.ShallowCopy();
        }
    }
}
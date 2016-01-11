using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.Forms.Core.Events;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using FormVisitorGroups.Impl;

namespace FormVisitorGroups.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class FormSubmissionInit : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var formEvents = ServiceLocator.Current.GetInstance<FormsEvents>();
            formEvents.FormsSubmissionFinalized += formEvents_FormsSubmissionFinalized;
        }

        void formEvents_FormsSubmissionFinalized(object sender, FormsEventArgs e)
        {
            var formEvent = (FormsSubmittedEventArgs)e;
            var submissionId = formEvent.FormSubmissionId.ToString();

            var submittedFormRepo = ServiceLocator.Current.GetInstance<ISubmittedFormsRepository>();
            submittedFormRepo.AddFormSubmissionId(submissionId);
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}

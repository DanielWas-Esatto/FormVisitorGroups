using EPiServer.Personalization.VisitorGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPiServer.Forms.Core.Data;
using EPiServer.ServiceLocation;

namespace FormVisitorGroups.SelectionFactories
{
    public class SubmittedFormSelectionFactory : ISelectionFactory
    {
        public IEnumerable<SelectListItem> GetSelectListItems(Type propertyType)
        {
            var formDataManager = ServiceLocator.Current.GetInstance<FormDataManager>();
            return formDataManager
                .GetFormsInfo(null)
                .Select(f => new SelectListItem {Text = f.Name, Value = f.FormGuid.ToString()});
        }
    }
}
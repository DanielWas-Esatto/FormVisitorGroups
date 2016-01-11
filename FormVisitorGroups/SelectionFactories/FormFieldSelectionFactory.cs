using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EPiServer.Forms.Core.Data;
using EPiServer.Forms.Core.Models;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.ServiceLocation;

namespace FormVisitorGroups.SelectionFactories
{
    public class FormFieldSelectionFactory : ISelectionFactory
    {
        private Injected<FormDataManager> _formDataManager;

        public IEnumerable<SelectListItem> GetSelectListItems(Type property)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var form in _formDataManager.Service.GetFormsInfo(null))
            {
                var mappings = _formDataManager.Service.GetFieldMappings(new FormIdentity(form.FormGuid, "en"));
                foreach (var fieldMapping in mappings)
                {
                    if (!fieldMapping.Name.StartsWith("SYS"))
                    {
                        items.Add(new SelectListItem
                        {
                            Text = form.Name + " > " + fieldMapping.FriendlyName,
                            Value = form.FormGuid.ToString() + " > " + fieldMapping.Name
                        });
                    }
                }
            }

            return items;
        }
    }
}
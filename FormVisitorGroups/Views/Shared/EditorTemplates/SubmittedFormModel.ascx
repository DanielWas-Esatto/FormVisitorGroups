<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FormVisitorGroups.Models.SubmittedFormModel>" %>
<%@ Import Namespace="EPiServer.Personalization.VisitorGroups" %>
 
<div id='FormModel'>
    <div class="epi-critera-block">
        <span class="epi-criteria-inlineblock">
            <%= Html.DojoEditorFor(model => model.SelectedForm)%>
        </span>
    </div>
</div>
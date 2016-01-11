<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FormVisitorGroups.Models.FormValueCriterionModel>" %>
<%@ Import Namespace="EPiServer.Personalization.VisitorGroups" %>
 
<div id='FormModel'>
    <div class="epi-critera-block">
        <span class="epi-criteria-inlineblock">
            <%= Html.DojoEditorFor(model => model.FormField)%>
            <%= Html.DojoEditorFor(model => model.Condition)%>
            <%= Html.DojoEditorFor(model => model.Value)%>
            <%= Html.DojoEditorFor(model => model.CaseSensitive)%> <%= Html.TranslateFallback("/formvisitorgroups/criteria/formvaluecriteria/casesensitive", "Case sensitive")%> 
        </span>
    </div>
</div>
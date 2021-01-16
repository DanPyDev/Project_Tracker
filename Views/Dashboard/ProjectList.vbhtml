@ModelType IEnumerable(Of Project_Tracker.ProjectModel)
@Code
    ViewData("Title") = "Projects List"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>

    #Projects, #Projects:hover {
        background-color: #626161 !important;
        list-style-position: inside !important;
        border: 1px solid black !important;
        border-right: none !important;
        -webkit-border-radius: 10px 0 0 10px !important;
    }

</style>

<h2>Projects List</h2>

<p>
    @Html.ActionLink("Create New Project", "AddProject", "Dashboard")
</p>

<table class="table">
    <tr>
        <th style="white-space:nowrap;">
            @Html.DisplayNameFor(Function(model) model.ProjectTitle)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ProjectDescription)
        </th>
        <th></th>
    </tr>

    @For Each item In Model
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.ProjectTitle)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.ProjectDescription)
            </td>
            <td>
                @Html.ActionLink("Details", "ProjectDetails", New With {.id = item.Id}) |
                @Html.ActionLink("Delete", "DeleteProject", New With {.id = item.Id})
            </td>
        </tr>
    Next

</table>

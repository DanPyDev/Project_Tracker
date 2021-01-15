@ModelType IEnumerable(Of DataLibrary.RoleModel)
@Code
    ViewData("Title") = "Role List"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>

    #ManRoles, #ManRoles:hover {
        background-color: #626161 !important;
        list-style-position: inside !important;
        border: 1px solid black !important;
        border-right: none !important;
        -webkit-border-radius: 10px 0 0 10px !important;
    }
</style>

<h2>Role List</h2>

<p>
    @Html.ActionLink("Create New", "AddRole", "Dashboard")
</p>

<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Role)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Description)
        </th>
        <th></th>
    </tr>

    @For Each item In Model
        @<tr>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Role)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Description)
            </td>
            <td>
                @Html.ActionLink("Edit", "EditRole", New With {.id = item.Id}) |
                @Html.ActionLink("Delete", "DeleteRole", New With {.id = item.Id})
            </td>
        </tr>
    Next

    @If Model.Count = 0 Then
        @<tr>
            <td>
                There are no Roles. Please Create Roles Using the Button Above.
            </td>
            <td></td>
        </tr>
    End If

</table>

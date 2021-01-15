@ModelType IEnumerable(Of DataLibrary.RequestModel)
@Code
ViewData("Title") = "Requests"
Layout = "~/Views/Shared/_Layout.vbhtml"
End Code


<style>

    #Requests, #Requests:hover {
        background-color: #626161 !important;
        list-style-position: inside !important;
        border: 1px solid black !important;
        border-right: none !important;
        -webkit-border-radius: 10px 0 0 10px !important;
    }
</style>


<h2>Requests</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Title)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Description)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Title)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Description)
        </td>
        <td>
            @Html.ActionLink("Details", "RequestDetails", New With {.id = item.Id}) |
            @Html.ActionLink("Delete", "DeleteRequest", New With {.id = item.Id})
        </td>
    </tr>
Next

</table>

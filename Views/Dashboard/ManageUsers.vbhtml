@ModelType UserRoleViewModel
@Code
    ViewData("Title") = "Manage Users"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>

    #ManUsers, #ManUsers:hover {
        background-color: #626161 !important;
        list-style-position: inside !important;
        border: 1px solid black !important;
        border-right: none !important;
        -webkit-border-radius: 10px 0 0 10px !important;
    }

    .select-box {
        width: 600px;
        margin: 100px auto;
    }

    .bs-searchbox .form-control {
        background-image: url("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyZpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuNi1jMTExIDc5LjE1ODMyNSwgMjAxNS8wOS8xMC0wMToxMDoyMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTUgKFdpbmRvd3MpIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjREMTA3MzIzRjZCODExRTg5ODU5RThGOUE5MjEzQTkxIiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjREMTA3MzI0RjZCODExRTg5ODU5RThGOUE5MjEzQTkxIj4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6NEQxMDczMjFGNkI4MTFFODk4NTlFOEY5QTkyMTNBOTEiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6NEQxMDczMjJGNkI4MTFFODk4NTlFOEY5QTkyMTNBOTEiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz6SGakMAAABBUlEQVR42mJgQABGII4B4n1A/AGIfwDxPSCeAsTqDASAABBvBuL/QPwGiNcB8XwgPgEV+w7E0bg0MwPxVqjCZiDmR5O3AOKrUHlXbAZEQSVb8LhQGohfAvEtIOZAlzwExK+BmIuAN/OwuYIJiA2A+CAQfyNgwB4obYpuACcQv2cgDGAWcKMb8ByI9YgwQAFK30eXmAb1mwkBA1YD8U8gVkKXUIE6DxRVsjg050AtWY3L9GioAlBUFUC9pAwN8ZVQORB+B8QpuAzxBOJrSIqR8WqoK2H8VFyGsAOxCxCXA3ETECdBvcgAtfk/MYbgA6lohiRRasgnZjIMOAfET4DYEIg3AQQYAKvpRrTzVBWuAAAAAElFTkSuQmCC");
        background-position: right 10px center;
        background-repeat: no-repeat;
        border-radius: 50px;
        height: 28px;
        padding: 0 10px;
        border: 1px solid #ccc;
        font-size: 12px;
    }
</style>

@*<select class="selectpicker" multiple="" data-live-search="true" data-live-search-placeholder="Search" tabindex="-98">
        <optgroup label="Driver Groups">
            <option>BEC</option>
            <option>VMA</option>

        </optgroup>
        <optgroup label="Drivers">
            <option>Stan</option>
            <option>Fanny</option>
            <option>Rudy</option>
            <option>Ahmed</option>
        </optgroup>

    </select>*@

<h2>Manage Users</h2>

<h3>Manage the User-Role Assignements of the Users with Access to this Web App.</h3>
<p>To Add new Users Please Assign then to this App in your Okta Org Page.</p>

<hr />

@Using (Html.BeginForm())
    @Html.AntiForgeryToken()

    @<div class="form-horizontal">

         <select class="selectpicker" multiple="" data-actions-box="true" data-live-search="true" data-live-search-placeholder="Search" tabindex="-98">
             <optgroup label="Users">
                @For Each item In Model.Users
                    @<option data-tokens=@(item.Id + "_Users")>
                        @Html.DisplayFor(Function(modelItem) item.Name)
                    </option>
                Next
             </optgroup>
         </select>
         
        <span style="display:inline-block; width: 50px;"></span>

        <select class="selectpicker" multiple="" data-title="No Roles Select" data-max-options="1" data-live-search="true" data-live-search-placeholder="Search" tabindex="-98">
            <optgroup label="Roles">
                @For Each item In Model.Roles
                    @<option data-tokens=@(item.Id.ToString + "_Roles")>
                        @Html.DisplayFor(Function(modelItem) item.Role)
                    </option>
                Next
            </optgroup>
        </select>
        
        <br>
        <br>

        <div class="form-group">
            <div class="col-md-10">
                <input id="assignRole" type="button" value="Assing" class="btn btn-default" />
            </div>
        </div>

        <br>
        <br>
    </div>
End Using


<table class="table">
    <tr>
        <th style="white-space:nowrap;">
            @Html.DisplayFor(Function(model) model.NameString)
        </th>
        <th>
            @Html.DisplayFor(Function(model) model.EmailString)
        </th>
        <th>
            @Html.DisplayFor(Function(model) model.RoleString)
        </th>
    </tr>

    @code
        Dim nas = 0
    End Code

    @For Each item In Model.Users
        If Not item.Role.Equals("N/A") Then
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Name)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Email)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Role)
                </td>
            </tr>
        Else
            nas += 1
        End If
    Next

    @If nas = Model.Users.Count Then
        @<tr>
            <td>
                There are no Users Assigned to any Roles. Please Select one or more Users From the Dropdown on the Top Left and one Role on the Top Right to Assign Roles.
            </td>
            <td></td>
            <td></td>
        </tr>
    End If

</table>

<script>
    var button = document.getElementById("assignRole");
    button.addEventListener(type="click", function () {
        var selected = document.querySelectorAll("[class='selected']");

        var role = "";
        var users = new Array();
        selected.forEach(function (item, index) {
            var tok = item.childNodes[0].dataset.tokens;
            if (tok.includes("_Users")) {
                users.push(tok.split("_")[0]);
            } else if (tok.includes("_Roles")) {
                role = tok.split("_")[0];
            }

        });

        if (selected.length - 1 != users.length && selected.length != users.length) {
            alert("Error in selecting Users and Role. Please try Again.");
            throw "Users Can Only be Assigned 1 Role at a Time... I don't even know how u got here...";
        } else if (selected.length == users.length) {
            alert("Error no Roles Selected. Please Select a Role to Assing the Selected Users.");
            throw "Please Select a Role to Assign the Selected Users.";
        }

        const data = role.toString().concat("_").concat(users.toString());

        $.post("/Dashboard/ManageUsersRole", data, function (data, status) {location.reload()})

    });
</script>
@ModelType ProjectDetailsModel
@Code
    ViewData("Title") = "ProjectDetails"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Details for Project: <b>@Html.DisplayFor(Function(modelItem) Model.Project.ProjectTitle)</b></h2>

<div class="row">
    <div class="col-sm-6">
        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="form-horizontal">
                <hr />
                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                @Html.HiddenFor(Function(model) model.Project.Id)

                <div class="form-group">
                    <label class="control-label col-md-2">Project Title</label>
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.Project.ProjectTitle, New With {.htmlAttributes = New With {.class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.Project.ProjectTitle, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    <label class="control-label col-md-2">Project Description</label>
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.Project.ProjectDescription, New With {.htmlAttributes = New With {.class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.Project.ProjectDescription, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <input type="submit" value="Save" class="btn btn-default" />
                    </div>
                </div>
            </div>
        End Using
    </div>

    <div class="col-sm-6">
        <h4><b>Add an Attatchment</b></h4>
        
        <form name="FileUpload1" method="post" action=@("/Dashboard/UploadProjFile/" + Model.Project.Id.ToString()) enctype="multipart/form-data">
            <div class="row">
                <div class="col-sm-7">
                    <input style="line-height:34px;" type="file" id="Files" name="filename" multiple="">
                </div>

                <div class="col-sm-5">
                    <input id="btnUpload" type="submit" value="Upload" class="btn btn-default" />
                </div>
            </div>
        </form>
        
        <br>

        <table class="table">
            <tr>
                <th style="white-space:nowrap;">
                    File Name
                    @*@Html.DisplayFor(Function(model) model.Files.Name)*@
                </th>
                <th></th>
                <th></th>
            </tr>

            @code
                Dim nas = 0
            End Code

            @If Model.Files.Count > 0 Then
                @For Each item In Model.Files
                    @<tr>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Name)
                        </td>
                        <td>
                            <input id=@item.Id type="button" value="Download" class="btn btn-default" />
                        </td>
                        <td>
                            @Html.ActionLink("Delete", "DeleteProjectFile", New With {.FileId = item.Id, .ProjId = Model.Project.Id})
                        </td>
                    </tr>
                Next
            Else
                @<tr>
                    <td>
                        There are no Files Associated with this Project.
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            End If

        </table>
    </div>
</div>


<div>
    @Html.ActionLink("Back to Projects", "ProjectList")
</div>

<br>
<br>

<div class="row">
    <div class="col-lg-6">

        <h4><b>Assign Users to this Project</b></h4>

        <span style="display:inline-block; width: 1px;"></span>

        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="form-horizontal">
                <div class="row">
                    <div class="col-sm-9">
                        <select class="selectpicker" multiple="" data-actions-box="true" data-live-search="true" data-live-search-placeholder="Search" tabindex="-98">
                            <optgroup label="Users">
                                @For Each item In Model.UsersFull
                                    @<option data-tokens=@(item.Id + "_Users")>
                                        @Html.DisplayFor(Function(modelItem) item.Name)
                                    </option>
                                Next
                            </optgroup>
                        </select>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <div class="col-md-10">
                                <input id="assignUsers" type="button" value="Assing" class="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        End Using

        <span style="display:inline-block; width: 1px;"></span>

        <table class="table">
            <tr>
                <th style="white-space:nowrap;">
                    User Name
                </th>
                <th>
                    Email
                </th>
                <th>
                    Role
                </th>
                <th></th>
            </tr>

            @code
                nas = 0
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
                        <td>
                            @Html.ActionLink("Unassign", "RemoveProjectUser", New With {.ProjId = Model.Project.Id, .UserId = item.Id})
                        </td>
                    </tr>
                Else
                    nas += 1
                End If
            Next

            @If nas = Model.Users.Count Then
                @<tr>
                    <td>
                        There are no Users Assigned to this project.
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            End If

        </table>
    </div>

    <div class="col-lg-6">
        <h4><b>Project Tickets</b></h4>
        
        <table class="table">
            <tr>
                <th style="white-space:nowrap;">
                    Title
                </th>
                <th>
                    Submitter
                </th>
                <th>
                    Status
                </th>
                <th>
                    Created
                </th>
            </tr>

            @If Model.Tickets.Ticket.Count > 0 Then
                Dim index = 0
                For Each item In Model.Tickets.Ticket
                    @<tr>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Title)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) Model.Tickets.Submitter(index).Name)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Status)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Created)
                        </td>
                    </tr>
                    index += 1
                Next
            Else
                @<tr>
                    <td>
                        There are no Tickets Associated with this Project.
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            End If

        </table>
    </div>
</div>



@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section

<script>
    var button = document.querySelector("[value='Download']");

    if (button == null) {

    } else {
        button.addEventListener(type = "click", function () {
            const data = button.id;
            const url = "/Dashboard/DownloadFile/".concat(button.id.toString());
            $.post(url, data, function (data, status, response) {
                //console.log(response.getResponseHeader("Content-Disposition").split("filename=")[1])
                //console.log(response.getResponseHeader("Content-Type"))
                const url = window.URL
                    .createObjectURL(new Blob([data]));
                const link = document.createElement('a');
                link.href = url;
                link.setAttribute('download', response.getResponseHeader("Content-Disposition").split("filename=")[1]);
                link.click();
            })
        });
    }

    var button1 = document.getElementById("assignUsers");
    button1.addEventListener(type="click", function () {
        var selected = document.querySelectorAll("[class='selected']");

        var users = new Array();
        
        selected.forEach(function (item, index) {
            var tok = item.childNodes[0].dataset.tokens;
            users.push(tok.split("_")[0]);
        });

        const data = users.toString();

        $.post("/Dashboard/AssignUsers", data, function (data, status) {location.reload()})

    });
</script>

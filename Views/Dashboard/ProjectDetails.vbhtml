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

        <form name="FileUpload1" method="post" action=@("/Dashboard/UploadProjFile/" + Model.Project.Id.ToString()) enctype="multipart/form-data">
            <div>
                <input type="file" id="Files" name="filename" multiple="">
            </div>
            <br>
            <div>
                <input id="btnUpload" type="submit" value="Upload" class="btn btn-default" />
            </div>
        </form>
    </div>

    <div class="col-sm-6">
        <span style="display:inline-block; height: 12px;"></span>
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


<span style="display:inline-block; height: 40px;"></span>


<div class="row">
    <div class="col-sm-6">
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
                </tr>
            End If

        </table>
    </div>

    <div class="col-sm-6">
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
</script>

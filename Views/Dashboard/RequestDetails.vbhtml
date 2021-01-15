@ModelType Project_Tracker.ReqFilesModel
@Code
    ViewData("Title") = "RequestDetails"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Details for Request: <b>@Model.Request.Title</b></h2>

<div class="row">
    <div class="col-sm-6">
        @Using (Html.BeginForm())
            @Html.AntiForgeryToken()

            @<div class="form-horizontal">
                <hr />
                @Html.ValidationSummary(True, "", New With {.class = "text-danger"})
                <div class="form-group">
                    @Html.LabelFor(Function(model) model.Request.Title, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.Request.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.Request.Title, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(Function(model) model.Request.Description, htmlAttributes:=New With {.class = "control-label col-md-2"})
                    <div class="col-md-10">
                        @Html.EditorFor(Function(model) model.Request.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
                        @Html.ValidationMessageFor(Function(model) model.Request.Description, "", New With {.class = "text-danger"})
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-offset-2 col-md-10">
                        <input type="submit" value="Create" class="btn btn-default" />
                    </div>
                </div>
            </div>
        End Using

        <form name="FileUpload1" method="post" action=@("/Dashboard/UploadReqFile/" + Model.Request.Id.ToString()) enctype="multipart/form-data">
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
                            @Html.ActionLink("Delete", "DeleteRequestFile", New With {.FileId = item.Id, .ReqId = Model.Request.Id})
                        </td>
                    </tr>
                Next
            Else
                @<tr>
                    <td>
                        There are no Files Associated with this Request.
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            End If

        </table>
    </div>
</div>

<div>
    @Html.ActionLink("Back to Request List", "Requests", "Dashboard")
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
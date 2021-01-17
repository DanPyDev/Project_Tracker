Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Web.Mvc
Imports DataLibrary.ProjectProcessor
Imports DataLibrary.UserProcessor
Imports DataLibrary.RoleProcessor
Imports DataLibrary.FileProcessor
Imports DataLibrary.RequestProcessor
Imports DataLibrary.ReqFileProcessor
Imports DataLibrary.ProjFileProcessor
Imports DataLibrary.ProjectTicketsProcessor
Imports DataLibrary.TicketSubmitterProcessor
Imports DataLibrary.ProjectUserProcessor
Imports System.IO
Imports System.Threading.Tasks
Imports System.Data.SqlClient

Namespace Controllers
    Public Class DashboardController
        Inherits Controller

        ' GET: /Dashboard/Home
        <Authorize>
        Function Home() As ActionResult
            Return View()
        End Function

        ' GET: /Dashboard/ManageRoles
        <Authorize>
        Function ManageRoles() As ActionResult
            Return View()
        End Function

        ' GET: /Dashboard/ManageUsers
        <Authorize>
        Function ManageUsers() As ActionResult
            Dim client = New HttpClient()
            client.BaseAddress = New Uri("https://dev-5394022.okta.com")

            Dim cred = "00yBILpDzJWfo65vuOh__u8H1pePCbPjny6aItuuME"

            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            client.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("SSWS", cred)

            Dim Response = client.GetAsync("/api/v1/users?limit=25").Result

            Dim stringData = Response.Content.ReadAsStringAsync().Result
            'System.Diagnostics.Debug.WriteLine(stringData)

            Dim entries = stringData.Split(CType(",", Char()), StringSplitOptions.RemoveEmptyEntries)

            Dim printing = False

            Dim resp = New List(Of Dictionary(Of String, String))

            Dim currUser = 0

            For Each entry As String In entries
                Dim key = entry.Split(CType(":", Char()), 2)(0).Replace("""", " ").Replace("{", " ").Replace("[", " ").Trim()
                Dim val = entry.Split(CType(":", Char()), 2)(1).Replace("""", " ").Replace("{", " ").Replace("}", " ").Trim()

                If key.Equals("profile") Then
                    printing = True
                    key = val.Split(CType(":", Char()), 2)(0).Trim()
                    val = val.Split(CType(":", Char()), 2)(1).Trim()
                ElseIf key.Equals("credentials") Then
                    printing = False
                    currUser += 1
                ElseIf key.Equals("id") Then
                    resp.Add(New Dictionary(Of String, String))
                    resp(currUser).Add(key, val)
                    'System.Diagnostics.Debug.WriteLine(key + ":" + val)
                End If

                If printing Then
                    If key.Equals("firstName") Or key.Equals("lastName") Or key.Equals("email") Then
                        resp(currUser).Add(key, val)
                        'System.Diagnostics.Debug.WriteLine(key + ":" + val)
                    End If
                End If
            Next

            For Each dict As Dictionary(Of String, String) In resp
                InsertUser(dict("id"), dict("firstName") + " " + dict("lastName"), dict("email"))
            Next

            Dim data = LoadUsers()

            Dim Users = New List(Of DataLibrary.UserModel)()

            For Each row As DataLibrary.UserModel In data
                Users.Add(New DataLibrary.UserModel() With {
                             .Id = row.Id,
                             .Name = row.Name,
                             .Email = row.Email,
                             .Role = row.Role
                             })
            Next

            Dim data1 = LoadRoles()

            Dim Roles = New List(Of DataLibrary.RoleModel)()

            For Each row As DataLibrary.RoleModel In data1
                Roles.Add(New DataLibrary.RoleModel() With {
                             .Id = row.Id,
                             .Role = row.Role,
                             .Description = row.Description
                             })
            Next

            Dim UserRolesView = New UserRoleViewModel() With {
                            .NameString = "Name",
                            .EmailString = "Email",
                            .RoleString = "Role",
                            .Users = Users,
                            .Roles = Roles
                            }

            Return View(UserRolesView)
        End Function

        <Authorize>
        <HttpPost>
        Function ManageUsersRole() As ActionResult
            Dim raw = New StreamReader(Request.InputStream).ReadToEnd()

            If Integer.TryParse(raw.Split("_")(0), vbNull) Then
                Dim role = LoadRole(Convert.ToInt32(raw.Split("_")(0))).Role

                Dim Users = New List(Of DataLibrary.UserModel)()

                If raw.Contains(",") Then
                    For Each user As String In raw.Split("_")(1).Split(",")
                        Users.Add(New DataLibrary.UserModel() With {
                                                            .Id = user
                                                         })
                    Next
                Else
                    Users.Add(New DataLibrary.UserModel() With {
                                                            .Id = raw.Split("_")(1)
                                                         })
                End If

                For Each user As DataLibrary.UserModel In Users
                    AssignRole(user.Id, role)
                Next
            Else
                Response.Write("<script language=""javascript"">alert('No Changes Were Made. Please Select one Role to Assign Users to.');</script>")
            End If

            Return RedirectToAction("ManageUsers", "Dashboard")
        End Function

        ' GET: /Dashboard/Requests
        <Authorize>
        Function Requests() As ActionResult
            Dim RequestsD = LoadRequests()

            Return View(RequestsD)
        End Function

        <Authorize>
        Function DeleteRequest(id As Integer) As ActionResult
            DataLibrary.RequestProcessor.DeleteRequest(id)

            Return RedirectToAction("RequestList", "Dashboard")
        End Function

        <Authorize>
        Function RequestDetails(id As Integer) As ActionResult
            Dim Files = LoadBoundReqFiles(id)
            Dim Request = LoadRequest(id)

            Dim ReqFiles = New ReqFilesModel() With {
                                        .Files = Files,
                                        .Request = Request
                                }

            Return View(ReqFiles)
        End Function

        <Authorize>
        Function AddRequest() As ActionResult
            Dim ReqFiles As ReqFilesModel
            Dim tempReqLoaded = TempReq()

            If tempReqLoaded = 0 Then
                CreateRequest("TempTitle", "TempDescription")

                ReqFiles = New ReqFilesModel() With {
                                            .Files = New List(Of DataLibrary.FileModel),
                                            .Request = New DataLibrary.RequestModel
                                    }
            Else
                Dim Files = LoadBoundReqFiles(tempReqLoaded)
                Dim Request = LoadRequest(tempReqLoaded)

                ReqFiles = New ReqFilesModel() With {
                                            .Files = Files,
                                            .Request = Request
                                    }
            End If

            Return View(ReqFiles)
        End Function

        <Authorize>
        <HttpPost>
        Function AddRequest(model As ReqFilesModel) As ActionResult
            UpdateRequest(TempReq(), model.Request.Title, model.Request.Description)
            CleanRequests()
            CleanUnboundReqFiles()

            Return RedirectToAction("Requests", "Dashboard")
        End Function

        <Authorize>
        <HttpPost>
        Function UploadReqFile(id As Integer) As ActionResult
            Dim files = Request.Files

            If files.Count > 0 Then
                For Each file As String In files
                    Dim filename = files(file).FileName
                    Dim contentType = files(file).ContentType
                    Dim Fdata = New BinaryReader(files(file).InputStream).ReadBytes(CInt(files(file).InputStream.Length))

                    Dim fileId = CreateFile(filename, contentType, Fdata)
                    BindReqFile(id, fileId)
                Next
            End If

            Return Redirect(Request.UrlReferrer.ToString())
        End Function

        <Authorize>
        <HttpPost>
        Function UploadProjFile(id As Integer) As ActionResult
            Dim files = Request.Files

            If files.Count > 0 Then
                For Each file As String In files
                    Dim filename = files(file).FileName
                    Dim contentType = files(file).ContentType
                    Dim Fdata = New BinaryReader(files(file).InputStream).ReadBytes(CInt(files(file).InputStream.Length))

                    Dim fileId = CreateFile(filename, contentType, Fdata)
                    BindProjFile(id, fileId)
                Next
            End If

            Return Redirect(Request.UrlReferrer.ToString())
        End Function

        <Authorize>
        <HttpPost>
        Function DownloadFile(id As Integer) As ActionResult
            Dim File = LoadFile(id)

            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = File.ContentType
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + File.Name)
            Response.BinaryWrite(File.Data)
            Response.Flush()
            Response.End()

            Return Redirect(Request.UrlReferrer.ToString())
        End Function


        <Authorize>
        <Route("Dashboard/DeleteRequestFile/{FileId}/{ReqId}")>
        Function DeleteRequestFile(FileId As Integer, ReqId As Integer) As ActionResult
            DeleteBoundReqFile(ReqId, FileId)

            Return RedirectToAction("AddRequest", "Dashboard")
        End Function

        <Authorize>
        <Route("Dashboard/DeleteProjectFile/{FileId}/{ProjId}")>
        Function DeleteProjectFile(FileId As Integer, ProjId As Integer) As ActionResult
            DeleteBoundProjFile(ProjId, FileId)

            Return RedirectToAction("ProjectDetails", "Dashboard", New With {.id = ProjId})
        End Function

        ' GET: /Dashboard/ManageRoles
        <Authorize>
        Function RoleList() As ActionResult
            Dim data = LoadRoles()

            Dim roles = New List(Of DataLibrary.RoleModel)()

            For Each row As DataLibrary.RoleModel In data
                roles.Add(New DataLibrary.RoleModel() With {
                             .Id = row.Id,
                             .Role = row.Role,
                             .Description = row.Description
                             })
            Next

            Return View(roles)
        End Function

        <Authorize>
        Function AddRole() As ActionResult
            Return View()
        End Function

        <Authorize>
        Function EditRole(id As Integer) As ActionResult
            Dim Role = LoadRole(id)
            Return View(Role)
        End Function

        <Authorize>
        Function DeleteRole(id As Integer) As ActionResult
            Dim Role = LoadRole(id)

            UnassignRole(Role.Role)
            RemoveRole(id)

            Return RedirectToAction("RoleList", "Dashboard")
        End Function

        <Authorize>
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function AddRole(model As DataLibrary.RoleModel) As ActionResult
            If (ModelState.IsValid) Then
                CreateRole(model.Role, model.Description)
            End If

            Return RedirectToAction("RoleList", "Dashboard")
        End Function

        ' GET: /Dashboard/Projects
        <Authorize>
        Function ProjectList() As ActionResult
            Dim data = LoadProjects()

            Dim projects = New List(Of ProjectModel)()

            For Each row As DataLibrary.ProjectModel In data
                projects.Add(New ProjectModel() With {
                             .Id = row.Id,
                             .ProjectTitle = row.ProjectTitle,
                             .ProjectDescription = row.ProjectDescription
                             })
            Next

            Return View(projects)
        End Function

        <Authorize>
        Function DeleteProject(id As Integer) As ActionResult
            DataLibrary.ProjectProcessor.DeleteProject(id)

            Return RedirectToAction("ProjectList", "Dashboard")
        End Function

        <Authorize>
        Function ProjectDetails(id As Integer) As ActionResult
            Dim Files = LoadBoundProjFiles(id)
            Dim Project = LoadProject(id)

            Dim Tickets = New TicketSubmitterModel With {
                                        .Ticket = New List(Of DataLibrary.TicketModel),
                                        .Submitter = New List(Of DataLibrary.UserModel)
                                }

            For Each ticket As DataLibrary.TicketModel In LoadBoundProjTickets(id)
                Tickets.Ticket.Add(ticket)
                Tickets.Submitter.Add(LoadBoundTicketSubmitter(ticket.Id))
            Next

            Dim Users = LoadBoundProjUsers(id)

            Dim ProjDetails = New ProjectDetailsModel() With {
                                        .Files = Files,
                                        .Project = Project,
                                        .Tickets = Tickets,
                                        .UsersFull = LoadUsers(),
                                        .Users = Users
                                }

            Return View(ProjDetails)
        End Function

        <Authorize>
        Function AddProject() As ActionResult
            Return View()
        End Function

        <Authorize>
        <HttpPost>
        <ValidateAntiForgeryToken>
        Function AddProject(model As ProjectModel) As ActionResult
            If (ModelState.IsValid) Then
                CreateProject(model.ProjectTitle, model.ProjectDescription)
            End If

            Return RedirectToAction("ProjectList", "Dashboard")
        End Function
    End Class
End Namespace


'/Dashboard/Home
'/Dashboard/ManageRoles
'/Dashboard/ManageUsers
'/Dashboard/Projects
'/Dashboard/Requests
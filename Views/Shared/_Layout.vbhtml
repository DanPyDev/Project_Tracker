<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - Project Tracker v0.1</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
</head>
<body>
    <div class="wrapper">
        @If (Context.User.Identity.IsAuthenticated) Then
            @<nav id="sidebar" Class="">
                <div Class="sidebar-header">
                    <h3> Welcome, <b>@Context.User.Identity.Name</b></h3>
                </div>

                <ul Class="list-unstyled components">
                    <!--<li Class="active">
                        <a href = "#homeSubmenu" data-toggle="collapse" aria-expanded="false" Class="dropdown-toggle">Home</a>
                        <ul Class="collapse list-unstyled" id="homeSubmenu">
                            <li>
                    <a href = "#" > Home 1</a>
                            </li>
                            <li>
                    <a href = "#" > Home 2</a>
                            </li>
                            <li>
                    <a href = "#" > Home 3</a>
                            </li>
                        </ul>
                    </li>-->
                    <li id="Dash">
                        @Html.ActionLink("Dashboard", "Home", "Dashboard")
                    </li>
                    <li id="Projects">
                        @Html.ActionLink("Projects", "ProjectList", "Dashboard")

                        <!--<a href = "#pageSubmenu" data-toggle="collapse" aria-expanded="false" Class="dropdown-toggle">Pages</a>
                        <ul Class="collapse list-unstyled" id="pageSubmenu">
                            <li>
                        <a href = "#" > Page 1</a>
                            </li>
                            <li>
                        <a href = "#" > Page 2</a>
                            </li>
                            <li>
                        <a href = "#" > Page 3</a>
                            </li>
                        </ul>-->
                    </li>
                    <li id="Requests">
                        @Html.ActionLink("Requests", "Requests", "Dashboard")
                    </li>
                    <li id="ManRoles">
                        @Html.ActionLink("Manage Roles", "RoleList", "Dashboard")
                    </li>
                    <li id="ManUsers">
                        @Html.ActionLink("Manage Users", "ManageUsers", "Dashboard")
                    </li>
                </ul>
            </nav>
        End If

        <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        @If (Context.User.Identity.IsAuthenticated) Then
                            @<li>@Html.ActionLink("Home", "Index", "Home")</li>
                        Else
                            @<li>@Html.ActionLink("Home", "Index", "Home")</li>
                        End If

                        <li>@Html.ActionLink("About", "About", "Home")</li>
                        <li>@Html.ActionLink("Contact", "Contact", "Home")</li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        @If (Context.User.Identity.IsAuthenticated) Then
                            @<li>
                                <a onclick="document.getElementById('logout_form').submit();" style="cursor: pointer;">
                                    Sign out
                                </a>
                            </li>
                        Else
                            @<li>@Html.ActionLink("Sign In", "Login", "Account")</li>
                        End If
                    </ul>

                    @If (Context.User.Identity.IsAuthenticated) Then
                        @<form action="/Account/SignOut" method="post" id="logout_form"></form>
                    End If

                </div>
            </div>
        </div>

        <div class="container body-content">
            <br />
            @RenderBody()
            <!--<p>Hello, <b>@Context.User.Identity.Name</b></p>-->
            <hr />
        </div>
        <footer class="fixed-bottom">
            <p>&copy; @DateTime.Now.Year - Project Tracker v0.1</p>
        </footer>
    </div>

    @RenderSection("scripts", required:=False)
</body>
</html>

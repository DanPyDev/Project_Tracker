@Code
    ViewData("Title") = "Home Page"
End Code

@*<div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS and JavaScript.</p>
        <p><a href="https://asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET MVC gives you a powerful, patterns-based way to build dynamic websites that
                enables a clean separation of concerns and gives you full control over markup
                for enjoyable, agile development.
            </p>
            <p><a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301865">Learn more &raquo;</a></p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.</p>
            <p><a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301866">Learn more &raquo;</a></p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>You can easily find a web hosting company that offers the right mix of features and price for your applications.</p>
            <p><a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301867">Learn more &raquo;</a></p>
        </div>
    </div>*@

@If (Context.User.Identity.IsAuthenticated) Then
    @<p>Hello, <b>@Context.User.Identity.Name</b></p>
Else
    @<h1>Project Tracker</h1>
    @<h2>Home Page for the Project Tracker Web App. This App will register and help you manage any projects you are working on.</h2>
    @<hr />
    @<p>Please Login with your account to access your projects</p>
    @<p>Don't have an account? Contact your Organization's Admin</p>
    @<p>Or Experiment the Web App using this Guest Account</p>
End If

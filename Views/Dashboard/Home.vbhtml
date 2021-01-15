@Code
    ViewData("Title") = "Home"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>

    #Dash, #Dash:hover {
        background-color: #626161 !important;
        list-style-position: inside !important;
        border: 1px solid black !important;
        border-right: none !important;
        -webkit-border-radius: 10px 0 0 10px !important;
    }
</style>

<h2>Dashboard</h2>

<h1>Welcome, @Context.User.Identity.Name</h1>

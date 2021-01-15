Imports Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.AspNet.Identity
Imports System.Collections.Generic
Imports System.Configuration
Imports Okta.AspNet

<Assembly: OwinStartupAttribute(GetType(Startup))>

Partial Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType)
        app.UseCookieAuthentication(New CookieAuthenticationOptions())

        app.UseOktaMvc(New OktaMvcOptions() With {
            .OktaDomain = ConfigurationManager.AppSettings("okta:OktaDomain"),
            .ClientId = ConfigurationManager.AppSettings("okta:ClientId"),
            .ClientSecret = ConfigurationManager.AppSettings("okta:ClientSecret"),
            .RedirectUri = ConfigurationManager.AppSettings("okta:RedirectUri"),
            .PostLogoutRedirectUri = "https://localhost:44333/Account/PostSignOut"
        })
    End Sub
End Class

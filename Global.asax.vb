Imports System.IdentityModel.Claims
Imports System.Web.Helpers
Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

        AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier
    End Sub
End Class

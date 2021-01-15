Public Class UserRoleViewModel
    Private _NameString As String
    Private _EmailString As String
    Private _RoleString As String
    Private _Users As New List(Of DataLibrary.UserModel)
    Private _Roles As New List(Of DataLibrary.RoleModel)

    Public Property NameString() As String
        Get
            Return _NameString
        End Get
        Set(ByVal value As String)
            _NameString = value
        End Set
    End Property

    Public Property EmailString() As String
        Get
            Return _EmailString
        End Get
        Set(ByVal value As String)
            _EmailString = value
        End Set
    End Property

    Public Property RoleString() As String
        Get
            Return _RoleString
        End Get
        Set(ByVal value As String)
            _RoleString = value
        End Set
    End Property

    Public Property Users() As List(Of DataLibrary.UserModel)
        Get
            Return _Users
        End Get
        Set(ByVal value As List(Of DataLibrary.UserModel))
            For Each user As DataLibrary.UserModel In value
                _Users.Add(New DataLibrary.UserModel() With {
                             .Id = user.Id,
                             .Name = user.Name,
                             .Email = user.Email,
                             .Role = user.Role
                             })
            Next
        End Set
    End Property

    Public Property Roles() As List(Of DataLibrary.RoleModel)
        Get
            Return _Roles
        End Get
        Set(ByVal value As List(Of DataLibrary.RoleModel))
            For Each role As DataLibrary.RoleModel In value
                _Roles.Add(New DataLibrary.RoleModel() With {
                             .Id = role.Id,
                             .Role = role.Role,
                             .Description = role.Description
                             })
            Next
        End Set
    End Property
End Class

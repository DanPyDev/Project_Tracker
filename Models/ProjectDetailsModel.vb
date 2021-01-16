Public Class ProjectDetailsModel
    Private _Project As DataLibrary.ProjectModel
    Private _Files As List(Of DataLibrary.FileModel)
    Private _Users As List(Of DataLibrary.UserModel)
    Private _Tickets As TicketSubmitterModel

    Public Property Project() As DataLibrary.ProjectModel
        Get
            Return _Project
        End Get
        Set(ByVal value As DataLibrary.ProjectModel)
            _Project = value
        End Set
    End Property

    Public Property Files() As List(Of DataLibrary.FileModel)
        Get
            Return _Files
        End Get
        Set(ByVal value As List(Of DataLibrary.FileModel))
            _Files = value
        End Set
    End Property

    Public Property Users() As List(Of DataLibrary.UserModel)
        Get
            Return _Users
        End Get
        Set(ByVal value As List(Of DataLibrary.UserModel))
            _Users = value
        End Set
    End Property

    Public Property Tickets() As TicketSubmitterModel
        Get
            Return _Tickets
        End Get
        Set(ByVal value As TicketSubmitterModel)
            _Tickets = value
        End Set
    End Property
End Class

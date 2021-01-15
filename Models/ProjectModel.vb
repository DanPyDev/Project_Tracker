Imports System.ComponentModel.DataAnnotations

Public Class ProjectModel

    Private _Id As Integer
    Private _ProjectTitle As String
    Private _ProjectDescription As String

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property

    <Display(Name:="Project Title")>
    <Required(ErrorMessage:="Project Title is required to create a new project.")>
    Public Property ProjectTitle() As String
        Get
            Return _ProjectTitle
        End Get
        Set(ByVal value As String)
            _ProjectTitle = value
        End Set
    End Property

    <Display(Name:="Project Description")>
    Public Property ProjectDescription() As String
        Get
            Return _ProjectDescription
        End Get
        Set(ByVal value As String)
            _ProjectDescription = value
        End Set
    End Property

End Class

Public Class ProjFilesModel
    Private _Project As DataLibrary.ProjectModel
    Private _Files As List(Of DataLibrary.FileModel)

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
End Class

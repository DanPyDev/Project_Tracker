Public Class ReqFilesModel
    Private _Request As DataLibrary.RequestModel
    Private _Files As List(Of DataLibrary.FileModel)

    Public Property Request() As DataLibrary.RequestModel
        Get
            Return _Request
        End Get
        Set(ByVal value As DataLibrary.RequestModel)
            _Request = value
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

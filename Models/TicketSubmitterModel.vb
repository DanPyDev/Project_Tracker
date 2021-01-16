Public Class TicketSubmitterModel
    Private _Ticket As List(Of DataLibrary.TicketModel)
    Private _Submitter As List(Of DataLibrary.UserModel)

    Public Property Ticket() As List(Of DataLibrary.TicketModel)
        Get
            Return _Ticket
        End Get
        Set(ByVal value As List(Of DataLibrary.TicketModel))
            _Ticket = value
        End Set
    End Property

    Public Property Submitter() As List(Of DataLibrary.UserModel)
        Get
            Return _Submitter
        End Get
        Set(ByVal value As List(Of DataLibrary.UserModel))
            _Submitter = value
        End Set
    End Property
End Class

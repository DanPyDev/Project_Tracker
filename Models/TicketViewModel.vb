Public Class TicketViewModel
    Private _ProjId As Integer
    Private _Ticket As DataLibrary.TicketModel

    Public Property ProjId() As Integer
        Get
            Return _ProjId
        End Get
        Set(value As Integer)
            _ProjId = value
        End Set
    End Property

    Public Property Ticket() As DataLibrary.TicketModel
        Get
            Return _Ticket
        End Get
        Set(value As DataLibrary.TicketModel)
            _Ticket = value
        End Set
    End Property

End Class

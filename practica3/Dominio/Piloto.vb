Public Class Piloto
    Public Property idPILOTO As String
    Public Property Nombre As String
    Public Property Fecha_Nac As Date
    Public Property Pais As String
    Public ReadOnly Property PilotDAO As PilotoDAO

    Public Sub New()
        Me.PilotDAO = New PilotoDAO
    End Sub

    Public Sub New(id As String)
        Me.PilotDAO = New PilotoDAO
        Me.idPILOTO = id
    End Sub

    Public Sub LeerTodosPiloto()
        Me.PilotDAO.LeerTodas()
    End Sub

    Public Sub LeerPiloto()
        Me.PilotDAO.Leer(Me)
    End Sub

    Public Function InsertarPiloto() As Integer
        Return Me.PilotDAO.Insertar(Me)
    End Function

    Public Function ActualizarPiloto() As Integer
        Return Me.PilotDAO.Actualizar(Me)
    End Function

    Public Function BorrarPiloto() As Integer
        Return Me.PilotDAO.Borrar(Me)
    End Function
End Class
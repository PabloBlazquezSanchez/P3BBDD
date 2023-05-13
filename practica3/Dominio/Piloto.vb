Public Class Piloto
    Public Property idPILOTO As String
    Public Property Nombre As String
    Public Property Fecha_Nac As Date
    Public Property Pais As String
    Public ReadOnly Property PilotoDAO As PilotoDAO

    Public Sub New()
        Me.PilotoDAO = New PilotoDAO
    End Sub

    Public Function LeerClasificaciones() As Collection
        Return Me.PilotoDAO.LeerClasificaciones(Me.idPILOTO)
    End Function

    Public Sub New(id As String)
        Me.PilotoDAO = New PilotoDAO
        Me.idPILOTO = id
    End Sub

    Public Sub LeerTodosPiloto()
        Me.PilotoDAO.LeerTodas()
    End Sub

    Public Sub LeerPiloto()
        Me.PilotoDAO.Leer(Me)
    End Sub

    Public Function DevolverNombrePiloto(ByVal id As Integer) As String
        Return Me.PilotoDAO.DevolverNombrePiloto(id)
    End Function

    Public Function InsertarPiloto() As Integer
        Return Me.PilotoDAO.Insertar(Me)
    End Function

    Public Function ActualizarPiloto() As Integer
        Return Me.PilotoDAO.Actualizar(Me)
    End Function

    Public Function BorrarPiloto() As Integer
        Return Me.PilotoDAO.Borrar(Me)
    End Function
End Class
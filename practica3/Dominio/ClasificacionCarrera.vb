Public Class ClasificacionCarrera
    Public Property EDICION As Integer
    Public Property PILOTO As Piloto
    Public Property POSICION As Integer
    Public ReadOnly Property ClasifDAO As ClasificacionCarreraDAO

    Public Sub New()
        Me.ClasifDAO = New ClasificacionCarreraDAO
    End Sub
	
	Public Sub New(id As Piloto)
        Me.ClasifDAO = New ClasificacionCarreraDAO
        Me.PILOTO = id
    End Sub
    Public Function ResultadoPiloto(id As String, edicion As String) As String
        Return Me.ClasifDAO.ResultadoPiloto(id, edicion)
    End Function

    Public Sub LeerTodosClasif()
        Me.ClasifDAO.LeerTodas()
    End Sub

    Public Sub LeerClasif()
        Me.ClasifDAO.Leer(Me)
    End Sub

    Public Function InsertarClasif() As String
        Return Me.ClasifDAO.Insertar(Me)
    End Function

    Public Function ActualizarClasif() As String
        Return Me.ClasifDAO.Actualizar(Me)
    End Function

    Public Function BorrarClasif() As String
        Return Me.ClasifDAO.Borrar(Me)
    End Function
End Class
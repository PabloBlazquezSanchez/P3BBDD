Public Class Pais
    Public Property idPAIS As String
    Public Property Nombre As String
    Public ReadOnly Property PaisDAO As PaisDAO

    Public Sub New()
        Me.PaisDAO = New PaisDAO
    End Sub

    Public Sub New(id As String)
        Me.PaisDAO = New PaisDAO
        Me.idPAIS = id
    End Sub

    Public Sub LeerTodosPaises()
        Me.PaisDAO.LeerTodas()
    End Sub

    Public Sub LeerPais()
        Me.PaisDAO.Leer(Me)
    End Sub
    Public Function InsertarPais() As String
        Return Me.PaisDAO.Insertar(Me)
    End Function

    Public Function ActualizarPais() As String
        Return Me.PaisDAO.Actualizar(Me)
    End Function

    Public Function BorrarPais() As String
        Return Me.PaisDAO.Borrar(Me)
    End Function
End Class
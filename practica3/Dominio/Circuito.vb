Public Class Circuito
    Public Property IdCircuito As Integer
    Public Property Nombre As String
    Public Property Ciudad As String
    Public Property Pais As String
    Public Property Longitud As Decimal
    Public Property Curva As Integer
    Public ReadOnly Property CircuDAO As CircuitoDAO

    Public Sub New()
        Me.CircuDAO = New CircuitoDAO
    End Sub

    Public Sub New(id As String)
        Me.CircuDAO = New CircuitoDAO
        Me.IdCircuito = id
    End Sub

    Public Sub LeerTodosCircuitos()
        Me.CircuDAO.LeerTodas()
    End Sub

    Public Sub LeerCircuito()
        Me.CircuDAO.Leer(Me)
    End Sub

    Public Function InsertarCircuito() As String
        Return Me.CircuDAO.Insertar(Me)
    End Function

    Public Function ActualizarCircuito() As String
        Return Me.CircuDAO.Actualizar(Me)
    End Function

    Public Function BorrarCircuito() As String
        Return Me.CircuDAO.Borrar(Me)
    End Function
End Class
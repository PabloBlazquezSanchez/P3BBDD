Public Class InscripcionMundial
    Public Property PILOTO As Integer
    Public Property TEMPORADA As Integer
    Public ReadOnly Property InscrMunDAO As InscripcionMundialDAO

    Public Sub New()
        Me.InscrMunDAO = New InscripcionMundialDAO
    End Sub

    Public Sub New(idPiloto As String)
        Me.InscrMunDAO = New InscripcionMundialDAO
        Me.PILOTO = idPiloto
    End Sub

    Public Sub LeerTodasInscripciones()
        Me.InscrMunDAO.LeerTodas()
    End Sub

    Public Sub LeerInscripcion()
        Me.InscrMunDAO.Leer(Me)
    End Sub

    Public Function InsertarInscripcion() As String
        Return Me.InscrMunDAO.Insertar(Me)
    End Function

    Public Function ActualizarInscripcion() As String
        Return Me.InscrMunDAO.Actualizar(Me)
    End Function

    Public Function BorrarInscripcion() As String
        Return Me.InscrMunDAO.Borrar(Me)
    End Function

    Public Function ObtenerDorsalesInscripcion(ByVal i As Integer) As Integer()
        Return Me.InscrMunDAO.ObtenerDorsales(i)
    End Function
End Class
Public Class Edicion
	Public Property idEDICION As Integer
	Public Property idGRAN_PREMIO As Integer
	Public Property NOMBRE As String
	Public Property CIRCUITO As Integer
	Public Property FECHA As Date
	Public Property ANIO As Integer
	Public Property PILOTO_VR As Integer
    Public ReadOnly Property EdDAO As EdicionDAO

    Public Sub New()
        Me.EdDAO = New EdicionDAO
    End Sub

    Public Sub New(id As String)
        Me.EdDAO = New EdicionDAO
        Me.idEDICION = id
    End Sub

    Public Function GetGPAnio(ByVal fecha As String) As Collection
        Return Me.EdDAO.GetGPAnio(fecha)
    End Function
    Public Function GetEdicionPiloto(ByVal id As String) As Collection
        Return Me.EdDAO.GetEdicionPiloto(id)
    End Function

    Public Function ObtenerPartGP_Piloto(ByVal id As String) As Collection
        Return Me.EdDAO.ObtenerPartGP_Piloto(id)
    End Function

    Public Function ObtenerEdicionesDeGP(ByVal id As String) As Collection
        Return Me.EdDAO.ObtenerEdicionesDeGP(id)
    End Function

    Public Sub LeerTodasEdiciones()
        Me.EdDAO.LeerTodas()
    End Sub

    Public Sub LeerEdicion()
        Me.EdDAO.Leer(Me)
    End Sub

    Public Function InsertarEdicion() As String
        Return Me.EdDAO.Insertar(Me)
    End Function

    Public Function ActualizarEdicion() As String
        Return Me.EdDAO.Actualizar(Me)
    End Function

    Public Function BorrarEdicion() As String
        Return Me.EdDAO.Borrar(Me)
    End Function
End Class
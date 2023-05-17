Public Class GranPremio
	Public Property idGRAN_PREMIO As Integer
	Public Property PAIS As String
	Public Property NOMBRE As String
	Public ReadOnly Property GPDAO As GranPremioDAO

	Public Sub New()
		Me.GPDAO = New GranPremioDAO
	End Sub

	Public Sub New(id As String)
		Me.GPDAO = New GranPremioDAO
		Me.idGRAN_PREMIO = id
	End Sub

    Public Sub LeerTodosGP()
        Me.GPDAO.LeerTodas()
    End Sub

    Public Sub LeerGP()
        Me.GPDAO.Leer(Me)
    End Sub

    Public Sub LeerNombreGP()
        Me.GPDAO.LeerNombre(Me)
    End Sub

    Public Function InsertarGP() As String
        Return Me.GPDAO.Insertar(Me)
    End Function

    Public Function ActualizarGP() As String
        Return Me.GPDAO.Actualizar(Me)
    End Function

    Public Function BorrarGP() As String
        Return Me.GPDAO.Borrar(Me)
    End Function
End Class
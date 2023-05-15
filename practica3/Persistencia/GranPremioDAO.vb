Public Class GranPremioDAO
    Public ReadOnly GP As Collection

    Public Sub New()
        Me.GP = New Collection
    End Sub

    Public Sub LeerTodas()
        Dim g As GranPremio
        Dim col, aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM GRAN_PREMIO ORDER BY idGRAN_PREMIO")
        For Each aux In col
            g = New GranPremio(CInt(aux(1).ToString))
            g.PAIS = aux(2).ToString
            g.NOMBRE = aux(3).ToString
            Me.GP.Add(g)
        Next
    End Sub

    Public Sub Leer(ByRef g As GranPremio)
        Dim col As Collection : Dim aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM GRAN_PREMIO WHERE idGRAN_PREMIO='" & g.idGRAN_PREMIO & "';")
        For Each aux In col
            g.idGRAN_PREMIO = CInt(aux(1).ToString)
            g.PAIS = aux(2).ToString
            g.NOMBRE = aux(3).ToString
        Next
    End Sub



    Public Function Insertar(ByVal g As GranPremio) As String
        Return AgenteBD.ObtenerAgente().Modificar("INSERT INTO GRAN_PREMIO VALUES ('" & g.idGRAN_PREMIO & "', '" & g.PAIS & "', '" & g.NOMBRE & "');")
    End Function

    Public Function Actualizar(ByVal g As GranPremio) As String
        Return AgenteBD.ObtenerAgente().Modificar("UPDATE GRAN_PREMIO SET PAIS='" & g.PAIS & "', NOMBRE'" & g.NOMBRE & "' WHERE idGRAN_PREMIO='" & g.idGRAN_PREMIO & "';")
    End Function

    Public Function Borrar(ByVal g As GranPremio) As String
        Return AgenteBD.ObtenerAgente().Modificar("DELETE FROM GRAN_PREMIO WHERE idGRAN_PREMIO='" & g.idGRAN_PREMIO & "';")
    End Function
End Class

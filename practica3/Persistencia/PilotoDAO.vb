Public Class PilotoDAO
    Public ReadOnly Pilotos As Collection

    Public Sub New()
        Me.Pilotos = New Collection
    End Sub

    Public Function LeerTodas() As Collection
        Dim p As Piloto
        Dim col, aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM PILOTO ORDER BY idPILOTO")
        For Each aux In col
            p = New Piloto(CInt(aux(1).ToString))
            p.Nombre = aux(2).ToString
            p.Fecha_Nac = aux(3)
            p.Pais = aux(4).ToString
            Me.Pilotos.Add(p)
        Next
        Return Pilotos
    End Function



    Public Sub Leer(ByRef p As Piloto)
        Dim col As Collection : Dim aux As Collection
        col = AgenteBD.ObtenerAgente.Leer("SELECT * FROM PILOTO WHERE idPILOTO='" & p.idPILOTO & "';")
        For Each aux In col
            p.idPILOTO = CInt(aux(1).ToString)
            p.Nombre = aux(2).ToString
            p.Fecha_Nac = aux(3)
            p.Pais = aux(4).ToString
        Next
    End Sub

    Public Function DevolverNombrePiloto(ByVal id As Integer) As String
        Dim col As Collection
        Dim iter As Collection
        Dim cadena As String = ""
        col = AgenteBD.ObtenerAgente.Leer("SELECT NOMBRE FROM PILOTO WHERE idPILOTO='" & id & "';")
        For Each iter In col
            cadena = CStr(iter(1))
        Next
        Return cadena
    End Function

    Public Function Insertar(ByVal p As Piloto) As String
        Return AgenteBD.ObtenerAgente.Modificar("INSERT INTO PILOTO VALUES ('" & p.idPILOTO & "', '" & p.Nombre & "', '" & p.Fecha_Nac & "', '" & p.Pais & "');")
    End Function

    Public Function Actualizar(ByVal p As Piloto) As String
        Return AgenteBD.ObtenerAgente.Modificar("UPDATE PILOTO SET NOMBRE='" & p.Nombre & "', FECHA_NACIMIENTO='" & p.Fecha_Nac & "', PAIS='" & p.Pais & "' WHERE idPILOTO='" & p.idPILOTO & "';")
    End Function

    Public Function Borrar(ByVal p As Piloto) As String
        Return AgenteBD.ObtenerAgente.Modificar("DELETE FROM PILOTO WHERE idPILOTO='" & p.idPILOTO & "';")
    End Function
End Class
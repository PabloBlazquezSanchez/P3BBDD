Public Class ClasificacionCarreraDAO
    Public ReadOnly Clasificaciones As Collection

    Public Sub New()
        Me.Clasificaciones = New Collection
    End Sub

    Public Function LeerTodas() As Collection
        Dim c As New ClasificacionCarrera
        Dim col, aux As Collection
        Dim Pilto As New Piloto
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM CLASIFICACION_CARRERA ORDER BY EDICION")
        For Each aux In col
            c.EDICION = aux(1).ToString
            Pilto.idPILOTO = aux(2).ToString
            c.PILOTO = Pilto
            c.POSICION = aux(3).ToString
            Me.Clasificaciones.Add(c)
        Next
        Return Me.Clasificaciones

    End Function

    Public Sub Leer(ByRef c As ClasificacionCarrera)
        Dim col As Collection : Dim aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM CLASIFICACION_CARRERA WHERE PILOTO='" & c.EDICION & "';")
        For Each aux In col
            c.EDICION = aux(1).ToString
            c.PILOTO = aux(2)
            c.POSICION = aux(3).ToString
        Next
    End Sub

    Public Function ResultadoPiloto(id As String, edicion As String) As String
        Dim col As Collection : Dim aux As Collection
        Dim c As New ClasificacionCarrera
        col = AgenteBD.ObtenerAgente().Leer("SELECT POSICION FROM CLASIFICACION_CARRERA WHERE EDICION='" & edicion & "' AND PILOTO='" & id & "';")
        For Each aux In col
            c.POSICION = aux(1).ToString
        Next
        Return c.POSICION
    End Function

    Public Function PosicionCarrera(id_piloto As Integer, edicion As Integer) As Integer
        Dim col As Collection : Dim aux As Collection
        Dim c As New ClasificacionCarrera
        col = AgenteBD.ObtenerAgente().Leer("SELECT POSICION FROM CLASIFICACION_CARRERA WHERE EDICION='" & edicion & "' AND PILOTO='" & id_piloto & "';")
        For Each aux In col
            c.POSICION = aux(1)
        Next
        Return c.POSICION
    End Function

    Public Function Insertar(ByVal c As ClasificacionCarrera) As String
        Return AgenteBD.ObtenerAgente().Modificar("INSERT INTO CLASIFICACION_CARRERA VALUES (" & c.EDICION & ", " & c.PILOTO.idPILOTO & ", " & c.POSICION & ");")
    End Function

    Public Function Actualizar(ByVal c As ClasificacionCarrera) As String
        Return AgenteBD.ObtenerAgente().Modificar("UPDATE CLASIFICACION_CARRERA SET PILOTO='" & c.PILOTO.idPILOTO & "', POSICION='" & c.POSICION & "' WHERE EDICION='" & c.EDICION & "';")
    End Function

    Public Function Borrar(ByVal c As ClasificacionCarrera) As String
        Return AgenteBD.ObtenerAgente().Modificar("DELETE FROM CLASIFICACION_CARRERA WHERE EDICION='" & c.EDICION & "';")
    End Function
End Class

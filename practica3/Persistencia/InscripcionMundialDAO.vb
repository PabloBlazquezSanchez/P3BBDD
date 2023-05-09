Public Class InscripcionMundialDAO
    Public ReadOnly Inscripciones As Collection
    Public Sub New()
        Me.Inscripciones = New Collection
    End Sub

    Public Sub LeerTodas()
        Dim i As InscripcionMundial
        Dim col, aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM INSCRIPCION_MUNDIAL ORDER BY PILOTO")
        For Each aux In col
            i = New InscripcionMundial(CInt(aux(1)))
            i.TEMPORADA = aux(2)
            Me.Inscripciones.Add(i)
        Next
    End Sub

    Public Sub Leer(ByRef i As InscripcionMundial)
        Dim col As Collection : Dim aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM INSCRIPCION_MUNDIAL WHERE PILOTO='" & i.PILOTO & "';")
        For Each aux In col
            i.PILOTO = CInt(aux(1))
            i.TEMPORADA = aux(2)
        Next
    End Sub

    Public Function Insertar(ByVal i As InscripcionMundial) As String
        Return AgenteBD.ObtenerAgente().Modificar("INSERT INTO INSCRIPCION_MUNDIAL VALUES ('" & i.PILOTO & "', '" & i.TEMPORADA & "');")
    End Function

    Public Function Actualizar(ByVal i As InscripcionMundial) As String
        Return AgenteBD.ObtenerAgente().Modificar("UPDATE INSCRIPCION_MUNDIAL SET TEMPORADA='" & i.TEMPORADA & "' WHERE PILOTO='" & i.PILOTO & "';")
    End Function

    Public Function Borrar(ByVal i As InscripcionMundial) As String
        Return AgenteBD.ObtenerAgente().Modificar("DELETE FROM INSCRIPCION_MUNDIAL WHERE PILOTO='" & i.PILOTO & "';")
    End Function

    Public Function ObtenerDorsales(ByVal i As Integer) As Integer()
        Dim dorsales() As Integer
        Dim col As Collection
        Dim aux As Collection
        col = AgenteBD.ObtenerAgente.Leer("SELECT PILOTO FROM INSCRIPCION_MUNDIAL WHERE TEMPORADA='" & i & "';")
        ReDim dorsales(col.Count - 1)
        Dim j As Integer = 0
        For Each aux In col
            dorsales(j) = CInt(aux(1))
            j += 1
        Next
        Return dorsales
    End Function

End Class

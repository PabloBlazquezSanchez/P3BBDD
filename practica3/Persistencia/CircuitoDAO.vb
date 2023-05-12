Public Class CircuitoDAO
    Public ReadOnly Circuitos As Collection

    Public Sub New()
        Me.Circuitos = New Collection
    End Sub

    Public Function LeerTodas() As Collection
        Dim c As Circuito
        Dim col, aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM CIRCUITO ORDER BY idCIRCUITO")
        For Each aux In col
            c = New Circuito(CInt(aux(1).ToString))
            c.Nombre = aux(2).ToString
            c.Ciudad = aux(3).ToString
            c.Pais = aux(4).ToString
            c.Longitud = aux(5)
            c.Curva = aux(6)
            Me.Circuitos.Add(c)
        Next
        Return Circuitos
    End Function

    Public Sub Leer(ByRef c As Circuito)
        Dim col As Collection : Dim aux As Collection
        col = AgenteBD.ObtenerAgente.Leer("SELECT * FROM CIRCUITO WHERE idCIRCUITO='" & c.IdCircuito & "';")
        For Each aux In col
            c.IdCircuito = CInt(aux(1).ToString)
            c.Nombre = aux(2).ToString
            c.Ciudad = aux(3).ToString
            c.Pais = aux(4).ToString
            c.Longitud = aux(5).ToString
            c.Curva = aux(6).ToString
        Next
    End Sub

    Public Function Insertar(ByVal c As Circuito) As String
        Return AgenteBD.ObtenerAgente().Modificar("INSERT INTO CIRCUITO VALUES ('" & c.IdCircuito & "', '" & c.Nombre & "', '" & c.Ciudad & "', '" & c.Pais & "', '" & c.Longitud & "', '" & c.Curva & "');")
    End Function

    Public Function Actualizar(ByVal c As Circuito) As String
        Return AgenteBD.ObtenerAgente().Modificar("UPDATE CIRCUITO SET NOMBRE='" & c.Nombre & "', CIUDAD='" & c.Ciudad & "', PAIS='" & c.Pais & "', LONGITUD='" & c.Longitud & "', CURVA='" & c.Curva & "' WHERE idCIRCUITO='" & c.IdCircuito & "';")
    End Function

    Public Function Borrar(ByVal c As Circuito) As String
        Return AgenteBD.ObtenerAgente().Modificar("DELETE FROM CIRCUITO WHERE idCIRCUITO='" & c.IdCircuito & "';")
    End Function
End Class

Public Class EdicionDAO
    Public ReadOnly Ediciones As Collection

    Public Sub New()
        Me.Ediciones = New Collection
    End Sub

    Public Function GetGPAnio(fecha As String) As Collection
        Dim col, aux As Collection
        Dim resultado As New Collection
        col = AgenteBD.ObtenerAgente.Leer("SELECT * FROM EDICION WHERE ANIO='" & fecha & "' ORDER BY idGRAN_PREMIO;")
        For Each aux In col
            Dim e As New Edicion()
            e.idEDICION = CInt(aux(1))
            e.idGRAN_PREMIO = aux(2).ToString
            e.NOMBRE = aux(3).ToString
            e.CIRCUITO = aux(4).ToString
            e.FECHA = aux(5).ToString
            e.ANIO = aux(6).ToString
            e.PILOTO_VR = aux(7)
            resultado.Add(e)
        Next
        Return resultado
    End Function

    Public Function GetEdicionPiloto(ByVal name As String) As Collection
        Dim col As Collection
        Dim iter As Object
        Dim resultado As New Collection
        col = AgenteBD.ObtenerAgente.Leer("SELECT DISTINCT ANIO FROM EDICION E JOIN CLASIFICACION_CARRERA C ON E.idEDICION=C.EDICION WHERE C.PILOTO ='" & name & "' ORDER BY ANIO DESC;")
        For Each iter In col
            resultado.Add(CStr(iter(1)))
        Next
        Return resultado
    End Function

    Public Function ObtenerPartGP_Piloto(ByVal id As String) As Collection

        Dim p As Edicion
        Dim col, aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM EDICION e JOIN CLASIFICACION_CARRERA c ON e.idEDICION=c.EDICION WHERE c.PILOTO='" & id & "';")
        For Each aux In col
            p = New Edicion(aux(1).ToString)
            p.idGRAN_PREMIO = aux(2).ToString
            Me.Ediciones.Add(p)
        Next
        Return Ediciones
    End Function

    Public Function ObtenerEdicionesDeGP(ByVal id As String) As Collection
        Dim resultado As New Collection
        Dim col, aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM EDICION e JOIN GRAN_PREMIO g ON e.idGRAN_PREMIO=g.idGRAN_PREMIO WHERE e.idGRAN_PREMIO='" & id & "';")
        For Each aux In col
            Dim e As New Edicion()
            e.idEDICION = CInt(aux(1))
            e.idGRAN_PREMIO = aux(2).ToString
            e.NOMBRE = aux(3).ToString
            e.CIRCUITO = aux(4).ToString
            e.FECHA = aux(5).ToString
            e.ANIO = aux(6).ToString
            e.PILOTO_VR = aux(7)
            resultado.Add(e)
        Next
        Return resultado
    End Function

    Public Function LeerTodas() As Collection
        Dim col, aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM EDICION ORDER BY idEDICION")
        For Each aux In col
            Dim e As New Edicion()
            e.idEDICION = CInt(aux(1))
            e.idGRAN_PREMIO = aux(2).ToString
            e.NOMBRE = aux(3).ToString
            e.CIRCUITO = aux(4).ToString
            e.FECHA = aux(5).ToString
            e.ANIO = aux(6).ToString
            e.PILOTO_VR = aux(7)
            Ediciones.Add(e)
        Next
        Return Ediciones
    End Function

    Public Sub Leer(ByRef e As Edicion)
        Dim col As Collection : Dim aux As Collection
        col = AgenteBD.ObtenerAgente.Leer("SELECT * FROM EDICION WHERE idEDICION= '" & e.idEDICION & "';")
        For Each aux In col
            'e = New Edicion(CInt(aux(0).ToString)) EN TODO CASO 1
            e.idGRAN_PREMIO = CInt(aux(2).ToString)
            e.NOMBRE = aux(3).ToString
            e.CIRCUITO = aux(4).ToString
            e.FECHA = aux(5).ToString
            e.ANIO = aux(6).ToString
            e.PILOTO_VR = aux(7)
        Next
    End Sub

    Public Function Insertar(ByVal e As Edicion) As String
        Dim fecha As Date = DateTime.ParseExact(e.FECHA.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)

        Return AgenteBD.ObtenerAgente.Modificar("INSERT INTO EDICION VALUES ('" & e.idEDICION & "', '" & e.idGRAN_PREMIO & "', '" & e.NOMBRE & "', '" & e.CIRCUITO & "', '" & fecha.ToString("yyyy/MM/dd") & "', '" & e.ANIO & "', '" & e.PILOTO_VR & "');")
    End Function

    Public Function Actualizar(ByVal e As Edicion) As String
        Dim fecha As Date = DateTime.ParseExact(e.FECHA.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)

        Return AgenteBD.ObtenerAgente.Modificar("UPDATE EDICION SET NOMBRE='" & e.NOMBRE & "', CIRCUITO='" & e.CIRCUITO & "', FECHA='" & fecha.ToString("yyyy/MM/dd") & "', ANIO='" & e.ANIO & "', PILOTO_VR='" & e.PILOTO_VR & "' WHERE idEDICION='" & e.idEDICION & "';")
    End Function

    Public Function Borrar(ByVal e As Edicion) As String
        Return AgenteBD.ObtenerAgente.Modificar("DELETE FROM EDICION WHERE idEDICION='" & e.idEDICION & "';")
    End Function
End Class

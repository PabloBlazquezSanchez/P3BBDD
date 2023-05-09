﻿Public Class EdicionDAO
    Public ReadOnly Ediciones As Collection

    Public Sub New()
        Me.Ediciones = New Collection
    End Sub

    Public Sub LeerTodas()
        Dim e As Edicion
        Dim col, aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM EDICION ORDER BY idEDICION")
        For Each aux In col
            e = New Edicion(CInt(aux(1).ToString))
            e.NOMBRE = aux(2).ToString
            Me.Ediciones.Add(e)
        Next
    End Sub

    Public Sub Leer(ByRef e As Edicion)
        Dim col As Collection : Dim aux As Collection
        col = AgenteBD.ObtenerAgente.Leer("SELECT * FROM EDICION WHERE idEDICION= '" & e.idEDICION & "';")
        For Each aux In col
            e.NOMBRE = aux(2).ToString
        Next
    End Sub

    Public Function Insertar(ByVal e As Edicion) As String
        Return AgenteBD.ObtenerAgente.Modificar("INSERT INTO EDICION VALUES ('" & e.idEDICION & "', '" & e.idGRAN_PREMIO & "', '" & e.NOMBRE & "', '" & e.CIRCUITO & "', '" & e.FECHA & "', '" & e.ANIO & "', '" & e.PILOTO_VR & "');")
    End Function

    Public Function Actualizar(ByVal e As Edicion) As String
        Return AgenteBD.ObtenerAgente.Modificar("UPDATE EDICION SET VALUES idGRAN_PREMIO='" & e.idGRAN_PREMIO & "', NOMBRE='" & e.NOMBRE & "', CIRCUITO='" & e.CIRCUITO & "', FECHA='" & e.FECHA & "', ANIO='" & e.ANIO & "', PILOTO_VR='" & e.PILOTO_VR & "' WHERE idEDICION='" & e.idEDICION & "';")
    End Function

    Public Function Borrar(ByVal e As Edicion) As String
        Return AgenteBD.ObtenerAgente.Modificar("DELETE FROM EDICION WHERE idEDICION='" & e.idEDICION & "';")
    End Function
End Class

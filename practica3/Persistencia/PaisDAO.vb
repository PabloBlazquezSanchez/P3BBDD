Imports System.Data.OleDb
Public Class PaisDAO
    Public ReadOnly Paises As Collection

    Public Sub New()
        Me.Paises = New Collection
    End Sub

    Public Function LeerTodas() As Collection

        Dim p As Pais
        Dim col, aux As Collection
        col = AgenteBD.ObtenerAgente().Leer("SELECT * FROM PAIS ORDER BY idPAIS")
        For Each aux In col
            p = New Pais(aux(1).ToString)
            p.Nombre = aux(2).ToString
            Me.Paises.Add(p)
        Next
        Return Paises
    End Function

    Public Sub Leer(ByRef p As Pais)
        Dim col As Collection : Dim aux As Collection
        col = AgenteBD.ObtenerAgente.Leer("SELECT * FROM PAIS WHERE idPAIS='" & p.idPAIS & "';")
        For Each aux In col
            p.Nombre = aux(2).ToString
        Next
    End Sub

    Public Function Insertar(ByVal p As Pais) As String
        Return AgenteBD.ObtenerAgente.Modificar("INSERT INTO PAIS VALUES ('" & p.idPAIS & "', '" & p.Nombre & "');")
    End Function

    Public Function Actualizar(ByVal p As Pais) As String
        Return AgenteBD.ObtenerAgente.Modificar("UPDATE PAIS SET NOMBRE='" & p.Nombre & "' WHERE idPAIS='" & p.idPAIS & "';")
    End Function

    Public Function Borrar(ByVal p As Pais) As String
        Return AgenteBD.ObtenerAgente.Modificar("DELETE FROM PAIS WHERE idPAIS='" & p.idPAIS & "';")
    End Function
End Class
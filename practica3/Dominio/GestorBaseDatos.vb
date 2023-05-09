Public Class GestorBaseDatos
    Public Sub New()
        comprobarbbdd()

    End Sub


    Public Sub comprobarbbdd()
        leerPais()
        leerCircuito()
        leerGP()
        leerINscripcion()
        leerEdicion()
        leerPiloto()
    End Sub

    Public Sub leerPais()
        AgenteBD.ObtenerAgente().Leer("SELECT * FROM Peliculas")
    End Sub

    Public Sub leerCircuito()
        AgenteBD.ObtenerAgente().Leer("SELECT * FROM Personas")
    End Sub

    Public Sub leerGP()
        AgenteBD.ObtenerAgente().Leer("SELECT * FROM Generos")
    End Sub

    Public Sub leerINscripcion()
        AgenteBD.ObtenerAgente().Leer("SELECT * FROM Roles")
    End Sub

    Public Sub leerPiloto()
        AgenteBD.ObtenerAgente().Leer("SELECT * FROM PeliGen")
    End Sub

    Public Sub leerEdicion()
        AgenteBD.ObtenerAgente().Leer("SELECT * FROM Participa")
    End Sub
End Class

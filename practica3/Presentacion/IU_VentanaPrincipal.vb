Public Class IU_VentanaPrincipal
    Private estadoCircuito As Integer
    Private circuito As Circuito
    Private estadoPiloto As Integer
    Private piloto As Piloto

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Opciones de controles'
        DateTimeNacimiento2.MaxDate = Now
        DateTimeNacimiento2.Value = Now
        llenarComboBoxPaises(CBPaisPiloto)
        rutaEjecutable = Application.StartupPath
        rutaEjecutable = rutaEjecutable.Substring(0, rutaEjecutable.Length - 9) & "Imagenes\"


        estadoPartiPeli = 0
        estadoPartiRol = 0
        estadoCheckAñadir = 0
        Dim personas As Personas
        personas = New Personas()
        Dim pelicula As ClasificacionCarrera
        pelicula = New ClasificacionCarrera()
        Dim generos = New Generos
        Dim roles = New Roles()
        generos.leertodo()
        pelicula.leertodo()
        personas.leertodo()
        roles.leertodo()

        For Each film As ClasificacionCarrera In pelicula.peliculasDAO.listaPeliculas()
            ListBoxCircuitos.Items.Add(film.idPelicula & "   " & film.titulo)

        Next

        For Each piloto As Piloto In personas.personasDAO.listaPersonas()
            ListBoxPilotos.Items.Add(person.idPersona & "  " & person.nombre & " " & person.apellido)
        Next

        For Each gener As Generos In generos.generoDAO.listaGeneros()
            ListBoxPaises.Items.Add(gener.idGenero & "  " & gener.descGenero)
        Next

        For Each paiss As Pais In roles.rolesDAO.listaRoles()
            ListBoxRoles.Items.Add(rol.idRol & "  " & rol.descRol)
        Next


    End Sub

    Function comprobarNombrePropio(ByVal Nombre As String) As Boolean
        Dim valido As Boolean
        valido = True
        For i = 0 To Nombre.Length - 1
            If Not (Char.IsLetter(Nombre(i)) Or Char.IsSeparator(Nombre(i))) Then
                valido = False
                Exit For
            End If
        Next
        Return valido
    End Function

    Private Sub LimpiarTextoFormularioGeneral(gb As GroupBox)
        For Each c As Control In gb.Controls
            If TypeOf (c) Is TextBox Then
                c.Text = ""

            ElseIf (TypeOf (c) Is ComboBox) Then
                c.ResetText()

            ElseIf (TypeOf (c) Is DateTimePicker) Then
                CType(c, DateTimePicker).Value = DateTime.MinValue
            End If
        Next
    End Sub

    Private Function comprobarCamposCircuito() As Boolean
        Dim camposValidos As Boolean
        camposValidos = True
        If CBPaisCircuito.SelectedIndex = "-1" Or TextBoxCiudadCircuito.Text = "" Or TextBoxNombreCircuito.Text = "" Or TextBoxCurvasCircuito.Text = "" Or TextBoxIDCircuito.Text = "" Or TextBoxLongitudCircuito.Text = "" Then
            camposValidos = False
            MsgBox("Es necesario que rellene todos los campos en Datos del circuito", vbExclamation)
            Return camposValidos
            Exit Function
        End If
        Return camposValidos
    End Function

    Private Sub BtLimpiarCir_Click(sender As Object, e As EventArgs) Handles BtLimpiarCir.Click
        LimpiarFormEditaCir()
    End Sub

    Private Sub LimpiarFormEditaCir()
        LimpiarTextoFormularioGeneral(GBEditarAñadirCircuito)
    End Sub

    Private Sub BtCancelarCir_Click(sender As Object, e As EventArgs) Handles BtCancelarCir.Click
        Dim volver As Integer
        volver = MsgBox("¿Estas seguro de que desea volver? Se perderán los cambios no guardados.", vbYesNo + vbDefaultButton2, "Cerrar modo edición.")
        If (volver = vbYes) Then
            estadoCircuito = -1
            ModoEditarAñadirCir(False)
        End If
    End Sub

    Private Sub ModoEditarAñadirCir(mode As Boolean)
        GBBotonesOpcionesCir.Enabled = Not mode
        ListBoxCircuitos.Enabled = Not mode
        GBEditarAñadirCircuito.Enabled = mode
        GBBotonesEdicionCir.Enabled = mode
        LimpiarFormEditaCir()
    End Sub

    Private Sub BtElditCir_Click(sender As Object, e As EventArgs) Handles BtEditCir.Click
        estadoCircuito = 1
        ModoEditarAñadirCir(True)
        TextBoxNombreCircuito.Text = Me.circuito.Nombre
        TextBoxCiudadCircuito.Text = Me.circuito.Ciudad
        TextBoxCurvasCircuito.Text = Me.circuito.Curva
        TextBoxLongitudCircuito.Text = Me.circuito.Longitud
        TextBoxIDCircuito.Text = Me.circuito.IdCircuito
        CBPaisCircuito.SelectedIndex = Me.circuito.Pais
    End Sub

    Private Function comprobarCamposPil() As Boolean
        Dim camposValidos As Boolean
        camposValidos = True

        If TextBoxNombrePiloto.Text = "" Or TextBoxIDPiloto.Text = "" Or DateTimeNacimiento2.Value = DateTime.MinValue Then
            camposValidos = False
            MsgBox("Es necesario que rellene todos los campos en Datos Personales", vbExclamation)

        ElseIf Not (comprobarNombrePropio(TextBoxNombrePiloto.Text)) Then
            camposValidos = False
            MsgBox("Nombre y apellidos no válido. Sólo puede contener letras y espacios", vbExclamation)

        ElseIf Not (comprobarNombrePropio(TextBoxIDPiloto.Text)) Then
            camposValidos = False
            MsgBox("ID del piloto no válido. Sólo puede contener letras y espacios ", vbExclamation)
        End If

        If CBPaisPiloto.SelectedItem Is Nothing Then
            camposValidos = False
            MsgBox("Es necesario que seleccione un país de nacimiento", vbExclamation)
        End If
        Return camposValidos

    End Function

    Private Sub ModoEditarAñadirPil(mode As Boolean)
        GBOpcionesPer.Enabled = Not mode
        ListBoxPilotos.Enabled = Not mode
        GBDatosPersonales.Enabled = mode
        GBBotonesEdicionPiloto.Enabled = mode
        LimpiarFormEditarPil()
    End Sub

    Private Sub LimpiarFormEditarPil()
        LimpiarTextoFormularioGeneral(GBDatosPersonales)
    End Sub

    Private Sub BtEditarPer_Click(sender As Object, e As EventArgs) Handles BtEditarPer.Click
        estadoPiloto = 1
        ModoEditarAñadirPil(True)
        TextBoxNombrePiloto.Text = piloto.Nombre
        DateTimeNacimiento2.Value = Piloto.Fecha_Nac
        TextBoxIDPiloto.Text = Piloto.idPILOTO
        CBPaisPiloto.Text = Piloto.Pais
    End Sub

    Private Sub BtAñadirCir_Click(sender As Object, e As EventArgs) Handles BtAñadirCir.Click
        TextBoxNombreCircuito.Enabled = True
        TextBoxCiudadCircuito.Enabled = True
        CBPaisCircuito.Enabled = True
        TextBoxCurvasCircuito.Enabled = True
        TextBoxLongitudCircuito.Enabled = True
        TextBoxIDCircuito.Enabled = True
    End Sub

    Private Sub BtAñadirPer_Click(sender As Object, e As EventArgs) Handles BtAñadirPer.Click

    End Sub

    Private Sub BtGuardarCir_Click(sender As Object, e As EventArgs) Handles BtGuardarCir.Click

    End Sub

    Private Sub TabGranPremio_DoubleClick(sender As Object, e As EventArgs) Handles TabGranPremio.DoubleClick

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

    End Sub
End Class
Public Class IU_VentanaPrincipal
    Private estadoCircuito As Integer
    Private circuito As Circuito
    Private estadoPiloto As Integer
    Private piloto As Piloto

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
        ' Declaración e inicialización de los arrays
        Dim puntos() As Integer = {10, 20, 30, 40, 50}
        Dim dorsales() As Integer = {1, 2, 3, 4, 5}

        ' Agrega las columnas al control DataGridView
        DataGridView1.Columns.Add("Dorsal", "Dorsal")
        DataGridView1.Columns.Add("Puntos", "Puntos")

        ' Asignación de los dorsales aleatorios al control DataGridView
        Dim dorsalesDisponibles As New List(Of Integer)(dorsales)
        Dim rnd As New Random()
        For i As Integer = 0 To puntos.Length - 1
            Dim j As Integer = rnd.Next(0, dorsalesDisponibles.Count)
            Dim dorsal As Integer = dorsalesDisponibles(j)
            DataGridView1.Rows.Add(dorsal, puntos(i))
            dorsalesDisponibles.RemoveAt(j)
        Next i

        ' Configuración de las propiedades del DataGridView
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill ' Ajusta el ancho de las columnas automáticamente

        ' Habilitar el botón nuevamente
        Button12.Enabled = False
        DataGridView1.ReadOnly = True
        DataGridView1.RowHeadersVisible = False


    End Sub

End Class
Imports MySql.Web

Public Class IU_VentanaPrincipal
    Private estadoCircuito As Integer
    Private circuito As Circuito
    Private estadoPiloto As Integer
    Private estadoPais As Integer
    Private paisEdi As Pais

    Private pais As Pais
    Private piloto As New Piloto()

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim myPais As New Pais()
        myPais = New Pais()
        myPais.LeerTodosPaises()
        ListBoxPaises.Items.Clear()
        For Each pais As Pais In myPais.PaisDAO.LeerTodas
            ListBoxPaises.Items.Add(pais.idPAIS & " - " & pais.Nombre)
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
        DateTimeNacimiento2.Value = piloto.Fecha_Nac
        TextBoxIDPiloto.Text = piloto.idPILOTO
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


    Private Sub AnadirEdicion_Click(sender As Object, e As EventArgs) Handles ButtonAnadirEdicion.Click
        ' Declaración e inicialización de los arrays
        Dim puntos() As Integer = {25, 18, 15, 12, 10, 8, 6, 4, 2, 1}
        Dim dorsales() As Integer
        ' Obtención de los dorsales
        Dim InscripcionMd As New InscripcionMundial()
        dorsales = InscripcionMd.ObtenerDorsalesInscripcion(2022)

        ' Agrega las columnas al control DataGridView
        DataGridViewEdicion.Columns.Add("Dorsal", "Dorsal")
        DataGridViewEdicion.Columns.Add("Puntos", "Puntos")
        DataGridViewEdicion.Columns.Add("Piloto", "Piloto")

        ' Establecer el color de texto negro y alinear el texto en el centro de las celdas
        DataGridViewEdicion.DefaultCellStyle.ForeColor = Color.Black
        DataGridViewEdicion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' También puedes hacer lo mismo para el otro DataGridView
        DataGridView2.DefaultCellStyle.ForeColor = Color.Black
        DataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' Asignación de los dorsales aleatorios al control DataGridView
        Dim dorsalesDisponibles As New List(Of Integer)(dorsales)
        Dim rnd As New Random()
        Dim j As Integer
        Dim dorsal As Integer
        Dim nombre As String

        Dim VMR As Integer = rnd.Next(0, UBound(dorsales))

        If (VMR + 1 <= 10) Then
            puntos(VMR) = puntos(VMR) + 1
        End If

        For i As Integer = 0 To dorsales.Length - 1
            j = rnd.Next(0, dorsalesDisponibles.Count)
            dorsal = dorsalesDisponibles(j)
            nombre = piloto.DevolverNombrePiloto(dorsal)
            If (i < puntos.Length) Then
                DataGridViewEdicion.Rows.Add(dorsal, puntos(i), nombre)
            Else
                DataGridViewEdicion.Rows.Add(dorsal, 0, nombre) 'Y si haces un ToString tras objeto piloto?
            End If
            dorsalesDisponibles.RemoveAt(j)
        Next i
        DataGridView2.Columns.Add("Dorsal", "Dorsal")
        DataGridView2.Columns.Add("Puntos", "Puntos")
        DataGridViewEdicion.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewEdicion.ColumnHeadersDefaultCellStyle.Font = New Font(DataGridViewEdicion.Font, FontStyle.Bold)
        DataGridView2.Rows.Add(DataGridViewEdicion.Rows(VMR).Cells(0).Value, DataGridViewEdicion.Rows(VMR).Cells(2).Value)

        ' Configuración de las propiedades del DataGridView
        DataGridViewEdicion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill ' Ajusta el ancho de las columnas automáticamente
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        ' Habilitar el botón nuevamente
        ButtonAnadirEdicion.Enabled = False

        DataGridViewEdicion.ReadOnly = True
        DataGridViewEdicion.RowHeadersVisible = False

        DataGridView2.ReadOnly = True
        DataGridView2.RowHeadersVisible = False
        DataGridView2.ScrollBars = False


    End Sub

    Private Sub ListBoxPaises_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxPaises.SelectedIndexChanged

        If ListBoxPaises.SelectedItem IsNot Nothing Then
            BtBorrarPais.Enabled = True
            BtEditarPais.Enabled = True
            Dim split As String() = ListBoxPaises.SelectedItem.ToString().Split(New [Char]() {" "c})
            Dim id As String
            id = split(0)
            Dim pais As Pais
            pais = New Pais
            pais.idPAIS = id
            pais.LeerPais()
            Me.pais = pais
            Me.paisEdi = pais
        Else
            BtBorrarPais.Enabled = False
            BtEditarPais.Enabled = False
        End If
    End Sub
    Private Sub BtBorrarPais_Click(sender As Object, e As EventArgs) Handles BtBorrarPais.Click
        Dim borrar As Integer
        borrar = MsgBox("¿Estás seguro de que desea eliminar el pais seleccionado? Se borrará de todas los datos que lo contengan", +vbYesNo + vbDefaultButton2, "Eliminar Persona.")
        If (borrar = vbYes) Then

            Try
                    Me.pais.BorrarPais()
                    ListBoxPaises.Items.RemoveAt(ListBoxPaises.SelectedIndex)
                Catch ex As Exception
                ' Manejar la excepción aquí
                MsgBox("No se pudo borrar país al estar vinculado con otros datos ")
            End Try

        End If
    End Sub

    Private Sub BtAñadirPais_Click(sender As Object, e As EventArgs) Handles BtAñadirPais.Click
        Me.estadoPais = 0
        GBEditarAñadirPais.Enabled = True
        TextBoxDescPais.Enabled = True
        BtAñadirPais.Enabled = False
        ListBoxPaises.Enabled = False

    End Sub

    Private Sub BtLimpiarPais_Click(sender As Object, e As EventArgs) Handles BtLimpiarPais.Click
        LimpiarTextoFormularioGeneral(GBEditarAñadirPais)
    End Sub

    Function CrearIDPais(country As String) As String
        ' Convertir el nombre del país a mayúsculas
        Dim upperCountry As String = country.ToUpper()
        Dim ids As New List(Of String)

        ' Leer todos los países desde la base de datos y almacenar sus IDs en la lista
        Dim myPais As New Pais()
        myPais.LeerTodosPaises()

        For Each paiss As Pais In myPais.PaisDAO.LeerTodas
            ids.Add(paiss.idPAIS)
        Next

        ' Generar ID de tres letras a partir de las tres primeras de la variable country
        Dim id As String
        Dim index As Integer = 0

        Do
            id = upperCountry.Substring(index, 3)
            index += 1

            ' Si el ID generado es igual a alguno del array, se generará un ID distinto utilizando la siguiente letra de country
            If ids.Contains(id) Then
                If index + 2 >= upperCountry.Length Then
                    MsgBox("No se puede generar un ID único con las letras disponibles en el nombre del país.")
                    Return ""
                End If
            Else
                Exit Do
            End If
        Loop

        Return id
    End Function


    Private Sub BtGuardarPais_Click(sender As Object, e As EventArgs) Handles BtGuardarPais.Click
        If TextBoxDescPais.Text = "" Then
            MsgBox("Introduzca un nombre para el pais", vbExclamation)
        ElseIf Not (comprobarNombrePropio(TextBoxDescPais.Text)) Then
            MsgBox("Nombre no válido. Solo puede contener letras y espacios.", vbExclamation)
        ElseIf TextBoxDescPais.Text.Length < 3 Then
            MsgBox("Nombre no válido. Escribe otro de mayor longitud.", vbExclamation)
        Else
            Dim descPais As String
            Dim pais As Pais
            descPais = TextBoxDescPais.Text().Trim()
            descPais = descPais.Substring(0, 1).ToUpper + descPais.Substring(1, descPais.Length - 1).ToLower
            Dim idPais As String
            idPais = CrearIDPais(descPais)
            If idPais = "" Then
                Return
            End If

            pais = New Pais(idPais)
            pais.Nombre = descPais

            If Me.estadoPais = 0 Then 'Añadir un pais'
                pais.InsertarPais()
                pais.LeerPais()
                ListBoxPaises.Items.Add(pais.idPAIS & " - " & pais.Nombre)

            ElseIf Me.estadoPais = 1 Then 'Editar un pais ya existente'
                Dim indice As Integer
                Try
                    Dim actualizar As Integer
                    Me.paisEdi.Nombre = descPais
                    actualizar = paisEdi.ActualizarPais
                    If (actualizar <> 1) Then
                        MessageBox.Show("Error. No se pudo modificar")
                        BtCancelarPais.PerformClick()
                    Else
                        MessageBox.Show("Pais modificado con éxito")
                        indice = ListBoxPaises.SelectedIndex
                        ListBoxPaises.Items.RemoveAt(indice)
                        ListBoxPaises.Items.Insert(indice, Me.paisEdi.idPAIS & " - " & Me.paisEdi.Nombre)
                        BtCancelarPais.PerformClick()

                    End If
                Catch

                End Try
            End If

            GBEditarAñadirPais.Enabled = False
            BtAñadirPais.Enabled = True
            TextBoxDescPais.Text = ""
        End If

    End Sub

    Private Sub BtEditarPais_Click(sender As Object, e As EventArgs) Handles BtEditarPais.Click
        TextBoxDescPais.Text = pais.Nombre
        GBEditarAñadirPais.Enabled = True
        BtAñadirPais.Enabled = False
        BtBorrarPais.Enabled = False
        BtEditarPais.Enabled = False
        ListBoxPaises.Enabled = False
        Me.estadoPais = 1

    End Sub


    Private Sub BtCancelarPais_Click(sender As Object, e As EventArgs) Handles BtCancelarPais.Click
        Me.estadoPais = -1
        BtAñadirPais.Enabled = True
        BtBorrarPais.Enabled = True
        BtEditarPais.Enabled = True
        GBEditarAñadirPais.Enabled = False
        ListBoxPaises.Enabled = True
    End Sub

    Private Sub DataGridViewEdicion_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewEdicion.CellContentClick

    End Sub
End Class
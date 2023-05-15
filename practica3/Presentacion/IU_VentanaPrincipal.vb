Imports System.Globalization
Imports MySql.Web

Public Class IU_VentanaPrincipal
    Private estadoCircuito As Integer
    Private circuito As Circuito
    Private estadoPiloto As Integer
    Private estadoPais As Integer
    Private paisEdi As Pais
    Private pilotoEdi As Piloto
    Private circuitoEdi As Circuito

    Private estadoGP As Integer
    Private GranPremio As GranPremio
    Private GranPremioEdi As GranPremio

    Private pais As Pais
    Private piloto As New Piloto()

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim myPais As New Pais()
        For Each pais As Pais In myPais.PaisDAO.LeerTodas
            ListBoxPaises.Items.Add(pais.idPAIS & " - " & pais.Nombre)

        Next

        Dim myPiloto As New Piloto()
        For Each piloto As Piloto In myPiloto.PilotoDAO.LeerTodas
            ListBoxPilotos.Items.Add(piloto.idPILOTO & " - " & piloto.Nombre)

        Next

        Dim myCircuito As New Circuito()
        For Each circuito As Circuito In myCircuito.CircuDAO.LeerTodas
            ListBoxCircuitos.Items.Add(circuito.IdCircuito & " - " & circuito.Nombre)

        Next

        Dim myGranPremio As New GranPremio()
        For Each GranPremio As GranPremio In myGranPremio.GPDAO.LeerTodas
            ListBoxGranPremio.Items.Add(GranPremio.idGRAN_PREMIO & " - " & GranPremio.NOMBRE)
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
                CType(c, DateTimePicker).Value = DateTimeNacimiento.MaxDate
            End If
        Next
    End Sub

    '--------------------------------'
    '    MÉTODOS PARA CIRCUITOS      '
    '--------------------------------'

    Private Function comprobarCamposCircuito() As Boolean
        Dim camposValidos As Boolean
        camposValidos = True
        If TextBoxNombreCircuito.Text = "" Or TextBoxCiudadCircuito.Text = "" Or CBPaisCircuito.SelectedItem Is Nothing Or TextBoxCurvasCircuito.Text = "" Or TextBoxLongitudCircuito.Text = "" Or TextBoxIDCircuito.Text = "" Then
            camposValidos = False
            MsgBox("Es necesario que rellene todos los campos en Datos del circuito", vbExclamation)
            'Return camposValidos
            'Exit Function
        End If
        Return camposValidos
    End Function


    Private Sub ListBoxCircuitos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxCircuitos.SelectedIndexChanged
        LimpiarFormEditaCir()
        If ListBoxCircuitos.SelectedItem IsNot Nothing Then
            BtElimCir.Enabled = True
            BtEditCir.Enabled = True
            Dim split As String() = ListBoxCircuitos.SelectedItem.ToString().Split(New [Char]() {" "c})
            Dim id As String
            id = split(0)
            Dim circuito As Circuito = New Circuito
            Try
                circuito.IdCircuito = id
                circuito.LeerCircuito()
                Me.circuito = circuito
                Me.circuitoEdi = circuito
                Dim myPais As New Pais(circuito.Pais)
                myPais.LeerPais()
                CBPaisCircuito.SelectedText = myPais.Nombre
                TextBoxNombreCircuito.Text = circuito.Nombre
                TextBoxCiudadCircuito.Text = circuito.Ciudad
                TextBoxCurvasCircuito.Text = circuito.Curva
                TextBoxLongitudCircuito.Text = circuito.Longitud
                TextBoxIDCircuito.Text = circuito.IdCircuito


                Dim textoBuscado As String = myPais.Nombre
                CBPaisCircuito.Items.Clear()
                For Each pais As Pais In myPais.PaisDAO.LeerTodas
                    CBPaisCircuito.Items.Add(pais.Nombre)
                Next

                For Each item As Object In CBPaisCircuito.Items
                    If item.ToString().Contains(textoBuscado) Then
                        CBPaisCircuito.SelectedItem = item
                        Exit For
                    End If
                Next


            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        Else
            BtEliminarPil.Enabled = False
            BtEditarPil.Enabled = False
        End If
    End Sub

    Private Sub BtAñadirCir_Click(sender As Object, e As EventArgs) Handles BtAñadirCir.Click
        Me.estadoCircuito = 0
        ModoEditarAñadirCir(True)
        LimpiarFormEditaCir()
        ListBoxCircuitos.Enabled = False
    End Sub

    Private Sub BtElditCir_Click(sender As Object, e As EventArgs) Handles BtEditCir.Click
        estadoCircuito = 1
        ModoEditarAñadirCir(True)

    End Sub

    Private Sub BtElimCir_Click(sender As Object, e As EventArgs) Handles BtElimCir.Click
        Dim borrar As Integer
        borrar = MsgBox("¿Estás seguro de que desea eliminar el circuito seleccionado?", +vbYesNo + vbDefaultButton2 + vbQuestion, "Eliminar Circuito")
        If (borrar = vbYes) Then
            Try
                Me.circuito.BorrarCircuito()
                ListBoxCircuitos.Items.RemoveAt(ListBoxCircuitos.SelectedIndex)
            Catch ex As Exception
                ' Manejar la excepción aquí
                MsgBox("No se pudo borrar el circuito al estar vinculado con otros datos.", vbExclamation)
            End Try
        End If
    End Sub

    Private Sub BtCancelarCir_Click(sender As Object, e As EventArgs) Handles BtCancelarCir.Click
        Dim volver As Integer
        volver = MsgBox("¿Estas seguro de que desea volver? Se perderán los cambios no guardados.", vbYesNo + vbDefaultButton2 + vbQuestion, "Cerrar modo edición.")
        If (volver = vbYes) Then
            DeshacerCamposCircuito()
        End If
    End Sub

    Private Sub DeshacerCamposCircuito()
        estadoCircuito = -1
        'Undo
        TextBoxNombreCircuito.Undo()
        TextBoxNombreCircuito.ClearUndo()
        TextBoxCiudadCircuito.Undo()
        TextBoxCiudadCircuito.ClearUndo()
        TextBoxLongitudCircuito.Undo()
        TextBoxLongitudCircuito.ClearUndo()
        TextBoxCurvasCircuito.Undo()
        TextBoxCurvasCircuito.Undo()
        TextBoxIDCircuito.Undo()
        TextBoxIDCircuito.ClearUndo()
        ModoEditarAñadirCir(False)
    End Sub

    Private Sub ModoEditarAñadirCir(mode As Boolean)
        GBBotonesOpcionesCir.Enabled = Not mode
        ListBoxCircuitos.Enabled = Not mode
        GBEditarAñadirCircuito.Enabled = mode
        GBBotonesEdicionCir.Enabled = mode
    End Sub

    Private Sub BtLimpiarCir_Click(sender As Object, e As EventArgs) Handles BtLimpiarCir.Click
        LimpiarFormEditaCir()
    End Sub

    Private Sub LimpiarFormEditaCir()
        LimpiarTextoFormularioGeneral(GBEditarAñadirCircuito)
    End Sub

    Private Sub BtGuardarCir_Click(sender As Object, e As EventArgs) Handles BtGuardarCir.Click
        If (comprobarCamposCircuito() = True) Then
            'Primero miro si el ID ya existe. Si es así, lo tiro para atrás.'
            'Para ello, obtengo los ID en una lista. Si el ID está dentro de la lista, error.
            Dim check As Boolean = True
            Dim myTrack As New Circuito()
            myTrack.LeerTodosCircuitos()
            For Each iteradorCir As Circuito In myTrack.CircuDAO.LeerTodas
                If iteradorCir.IdCircuito = TextBoxIDCircuito.Text() Then
                    check = False
                End If
            Next
            If (Not check And Me.estadoCircuito = 0) Then 'Intento crear un circuito con un ID ya existente
                MsgBox("ID de circuito ya existe.", vbExclamation)
            Else
                'Por lo demás, no tengo restricciones de repetición de grandes premios, así que me limito a insertarlo'
                Dim circuitoInsercion As Circuito = New Circuito()
                Dim paisCirc As Pais = New Pais()
                Dim nombreC As String = TextBoxNombreCircuito.Text.Trim()
                Dim ciudadC As String = TextBoxCiudadCircuito.Text.Trim()
                Dim abr As String = paisCirc.GetAbreviacion(CBPaisCircuito.SelectedItem)
                Dim lon As Integer = CInt(TextBoxLongitudCircuito.Text)
                Dim cur As Integer = CInt(TextBoxCurvasCircuito.Text)
                Dim idC As Integer = CInt(TextBoxIDCircuito.Text)

                If abr = "" Then
                    MsgBox("Error con el país entrante.", vbExclamation)
                    Exit Sub
                Else
                    circuitoInsercion.Nombre = nombreC
                    circuitoInsercion.Ciudad = ciudadC
                    circuitoInsercion.Pais = abr
                    circuitoInsercion.Longitud = lon
                    circuitoInsercion.Curva = cur
                    circuitoInsercion.IdCircuito = idC
                    If Me.estadoCircuito = 0 Then 'Añadir un pais'
                        circuitoInsercion.InsertarCircuito()
                        circuitoInsercion.LeerCircuito()
                        ListBoxCircuitos.Items.Add(circuitoInsercion.IdCircuito & " - " & circuitoInsercion.Nombre)
                        MsgBox("Se ha añadido a la base de datos el circuito " & circuitoInsercion.Nombre & " correctamente.", vbInformation)
                    ElseIf Me.estadoCircuito = 1 Then 'Editar un pais ya existente'
                        Dim indice As Integer
                        Try
                            Dim actualizar As Integer
                            Me.circuitoEdi = circuitoInsercion
                            actualizar = circuitoEdi.ActualizarCircuito
                            If (actualizar <> 1) Then
                                MsgBox("Error. No se pudo modificar", vbCritical)
                                'BtCancelarCir.PerformClick()
                            Else
                                MsgBox("Circuito modificado con éxito", vbInformation)
                                indice = ListBoxCircuitos.SelectedIndex
                                ListBoxCircuitos.Items.RemoveAt(indice)
                                ListBoxCircuitos.Items.Insert(indice, Me.circuitoEdi.IdCircuito & " - " & Me.circuitoEdi.Nombre)
                                'BtCancelarCir.PerformClick()
                            End If
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, ex.Source)
                        End Try
                    End If
                    estadoCircuito = -1
                    ModoEditarAñadirCir(False)
                End If
            End If
        End If
    End Sub

    '--------------------------------'
    '      MÉTODOS PARA PILOTOS      '
    '--------------------------------'

    Private Sub ListBoxPilotos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxPilotos.SelectedIndexChanged
        If ListBoxPilotos.SelectedItem IsNot Nothing Then
            ButtonSelectInformePil.Enabled = True
            GroupBoxInformePil.Visible = False
            GroupBoxInformePil2.Visible = False
            BtEliminarPil.Enabled = True
            BtEditarPil.Enabled = True
            Dim split As String() = ListBoxPilotos.SelectedItem.ToString().Split(New [Char]() {" "c})
            Dim id As String
            id = split(0)
            Dim piloto As Piloto
            piloto = New Piloto
            piloto.idPILOTO = id

            Try
                piloto.LeerPiloto()
                Me.piloto = piloto
                Me.pilotoEdi = piloto
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
            TextBoxNombrePiloto.Text = piloto.Nombre
            DateTimeNacimiento.Value = piloto.Fecha_Nac
            TextBoxIDPiloto.Text = piloto.idPILOTO
            Dim myPais As New Pais(piloto.Pais)
            myPais.LeerPais()
            Dim textoBuscado As String = myPais.Nombre

            CBPaisPiloto.Items.Clear()
            For Each pais As Pais In myPais.PaisDAO.LeerTodas
                CBPaisPiloto.Items.Add(pais.Nombre)
            Next

            For Each item As Object In CBPaisPiloto.Items
                If item.ToString().Contains(textoBuscado) Then
                    CBPaisPiloto.SelectedItem = item
                    Exit For
                End If
            Next
            'generarFichaPiloto(piloto)
        Else
            BtEliminarPil.Enabled = False
            BtEditarPil.Enabled = False
        End If
    End Sub

    Private Sub generarFichaPiloto(piloto As Piloto)

        TextBoxNombre1.Text = piloto.Nombre
        TextBoxPilInforme2.Text = piloto.Nombre

        Dim myEdicion As Collection
        Dim edi As New Edicion
        myEdicion = edi.EdDAO.GetEdicionPiloto(piloto.idPILOTO)

        Dim myEdiciones As Collection
        Dim ed As New Edicion
        myEdiciones = ed.ObtenerPartGP_Piloto(piloto.idPILOTO)

        Dim fecha As String
        ListBoxAñoInforme.Items.Clear()
        ListBoxEdicionGPInforme.Items.Clear()


        For Each fecha In myEdicion
            ListBoxAñoInforme.Items.Add(fecha)
        Next

        Dim nombre As Edicion
        For Each nombre In myEdiciones
            Dim GP As New GranPremio(nombre.idGRAN_PREMIO)
            GP.LeerGP()
            ListBoxEdicionGPInforme.Items.Add(GP.NOMBRE)
        Next

    End Sub

    Private Function CalcularPuntuacion(ByVal posicion As Integer, ByVal vueltaRapida As String) As Integer
        Dim puntuaciones() As Integer = {25, 18, 15, 12, 10, 8, 6, 4, 2, 1}
        Dim puntuacion As Integer = 0

        If posicion > 0 And posicion <= 10 Then
            puntuacion = puntuaciones(posicion - 1)

            If vueltaRapida = "Sí" And posicion <= 10 Then
                puntuacion += 1
            End If
        End If

        Return puntuacion
    End Function

    Private Function comprobarCamposPil() As Boolean
        Dim camposValidos As Boolean
        camposValidos = True

        If TextBoxNombrePiloto.Text = "" Or TextBoxIDPiloto.Text = "" Or DateTimeNacimiento.Value = DateTime.MinValue Then
            camposValidos = False
            MsgBox("Es necesario que rellene todos los campos en Datos Personales", vbExclamation)

        ElseIf Not (comprobarNombrePropio(TextBoxNombrePiloto.Text)) Then
            camposValidos = False
            MsgBox("Nombre y apellidos no válido. Sólo puede contener letras y espacios", vbExclamation)

        ElseIf Not IsNumeric(TextBoxIDPiloto.Text) Then
            camposValidos = False
            MsgBox("ID del piloto no válido. Sólo puede contener letras y espacios ", vbExclamation)
        End If

        If String.IsNullOrEmpty(CBPaisPiloto.Text) Then
            camposValidos = False
            MsgBox("Es necesario que seleccione un país de nacimiento", vbExclamation)
        End If
        Return camposValidos
    End Function

    Private Sub BtAñadirPer_Click(sender As Object, e As EventArgs) Handles BtAñadirPil.Click
        estadoPiloto = 0
        ModoEditarAñadirPil(True)
        BtLimpiarPiloto.PerformClick()
    End Sub

    Private Sub BtEditarPer_Click(sender As Object, e As EventArgs) Handles BtEditarPil.Click
        estadoPiloto = 1
        ModoEditarAñadirPil(True)
    End Sub

    Private Sub ModoEditarAñadirPil(mode As Boolean)
        GBOpcionesPer.Enabled = Not mode
        ListBoxPilotos.Enabled = Not mode
        GBDatosPersonales.Enabled = mode
        GBBotonesEdicionPiloto.Enabled = mode
    End Sub

    Private Sub BtEliminarPil_Click(sender As Object, e As EventArgs) Handles BtEliminarPil.Click
        Dim borrar As Integer
        borrar = MsgBox("¿Estás seguro de que desea eliminar el piloto seleccionado?", +vbYesNo + vbDefaultButton2 + vbQuestion, "Eliminar Persona.")
        If (borrar = vbYes) Then
            Try
                Me.piloto.BorrarPiloto()
                ListBoxPilotos.Items.RemoveAt(ListBoxPilotos.SelectedIndex)
            Catch ex As Exception
                MsgBox("No se pudo borrar el piloto al estar vinculado con otros datos.", vbExclamation)
            End Try

        End If
    End Sub

    Private Sub BtCancelarPiloto_Click(sender As Object, e As EventArgs) Handles BtCancelarPiloto.Click
        Dim volver As Integer
        volver = MsgBox("¿Estas seguro de que desea volver? Se perderán los cambios no guardados.", vbYesNo + vbDefaultButton2 + vbQuestion, "Cerrar modo edición.")
        If (volver = vbYes) Then
            DeshacerCamposPiloto()
        End If
    End Sub

    Private Sub DeshacerCamposPiloto()
        estadoPiloto = -1
        'Undo
        TextBoxNombrePiloto.Undo()
        TextBoxNombrePiloto.ClearUndo()
        TextBoxIDPiloto.Undo()
        TextBoxIDPiloto.ClearUndo()
        ModoEditarAñadirPil(False)
    End Sub

    Private Sub BtLimpiarPiloto_Click(sender As Object, e As EventArgs) Handles BtLimpiarPiloto.Click
        LimpiarTextoFormularioGeneral(GBDatosPersonales)
    End Sub

    Private Sub BtGuardarPiloto_Click(sender As Object, e As EventArgs) Handles BtGuardarPiloto.Click
        Dim indice As Integer
        If comprobarCamposPil() Then
            Dim check As Boolean = True
            Dim myDriver As New Piloto()
            myDriver.LeerTodosPiloto()
            For Each iteradorPil As Piloto In myDriver.PilotoDAO.LeerTodas
                If iteradorPil.idPILOTO = TextBoxIDPiloto.Text() Then
                    check = False
                End If
            Next
            If (Not check And Me.estadoPiloto = 0) Then 'Intento crear un circuito con un ID ya existente
                MsgBox("ID de piloto ya existe.", vbExclamation)
            Else
                Dim piloto As New Piloto()
                Dim pais As New Pais()
                piloto.Nombre = TextBoxNombrePiloto.Text()
                piloto.Fecha_Nac = DateTimeNacimiento.Value
                piloto.idPILOTO = TextBoxIDPiloto.Text
                pais.Nombre = CBPaisPiloto.SelectedItem
                'MessageBox.Show(CBPaisPiloto.SelectedItem)
                piloto.Pais = pais.GetAbreviacion(pais.Nombre)
                'MessageBox.Show(piloto.Pais)
                If (Me.estadoPiloto = 0) Then
                    Try
                        piloto.InsertarPiloto()
                        ListBoxPilotos.Items.Add(piloto.idPILOTO & " - " & piloto.Nombre)
                        MsgBox("Se ha añadido a la base de datos el piloto " & piloto.Nombre & " correctamente.", vbInformation)
                        ModoEditarAñadirPil(False)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, ex.Source)
                    End Try
                ElseIf (Me.estadoPiloto = 1) Then
                    Try
                        Dim actualizar As Integer
                        actualizar = piloto.ActualizarPiloto
                        If (actualizar <> 1) Then
                            MsgBox("Error. No se pudo modificar.", vbCritical)
                        Else
                            MsgBox("Piloto modificado con éxito.", vbInformation)
                            indice = ListBoxPilotos.SelectedIndex
                            ListBoxPilotos.Items.RemoveAt(indice)
                            ListBoxPilotos.Items.Insert(indice, piloto.idPILOTO & " - " & piloto.Nombre)
                        End If
                        estadoCircuito = -1
                        ModoEditarAñadirPil(False)
                        'ListBoxPilotos.SelectedItem = Nothing
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, ex.Source)
                    End Try
                    'estadoCheckAñadir = -1'
                End If
            End If
        End If
    End Sub

    '--------------------------------'
    '        MÉTODOS PARA GP         '
    '--------------------------------'

    Private Sub AnadirEdicion_Click(sender As Object, e As EventArgs) Handles ButtonAnadirEdicion.Click
        ' Agrega las columnas al control DataGridView
        DataGridViewEdicion.Columns.Add("Posición", "Posición")
        DataGridViewEdicion.Columns.Add("Piloto", "Piloto")
        'DataGridViewEdicion.Columns.Add("Dorsal", "Dorsal")
        DataGridViewEdicion.Columns.Add("Pais", "Pais")
        DataGridViewEdicion.Columns.Add("Puntos", "Puntos")

        ' Establecer el color de texto negro y alinear el texto en el centro de las celdas
        DataGridViewEdicion.DefaultCellStyle.ForeColor = Color.Black
        DataGridViewEdicion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' También puedes hacer lo mismo para el otro DataGridView
        DataGridView2.DefaultCellStyle.ForeColor = Color.Black
        DataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        ' Declaración e inicialización de los arrays
        Dim puntos() As Integer = {25, 18, 15, 12, 10, 8, 6, 4, 2, 1}
        Dim dorsales() As Integer
        ' Obtención de los dorsales
        Dim InscripcionMd As New InscripcionMundial()
        dorsales = InscripcionMd.ObtenerDorsalesInscripcion(2023)
        Dim nCorredores As Integer = UBound(dorsales)

        ' Asignación de los dorsales aleatorios al control DataGridView
        Dim dorsalesDisponibles As New List(Of Integer)(dorsales)
        Dim rnd As New Random()
        Dim j As Integer
        Dim driver As Piloto = New Piloto()
        Dim dorsal As Integer
        Dim nombreCorredor As String
        Dim paisCorredor As String

        Dim VMR As Integer = rnd.Next(0, nCorredores)

        If (VMR + 1 <= 10) Then
            puntos(VMR) = puntos(VMR) + 1
        End If

        For i As Integer = 0 To dorsales.Length - 1
            j = rnd.Next(0, dorsalesDisponibles.Count)
            dorsal = dorsalesDisponibles(j)
            driver.idPILOTO = dorsal
            driver.LeerPiloto()
            nombreCorredor = driver.Nombre 'piloto.DevolverNombrePiloto(dorsal)
            paisCorredor = driver.Pais
            If (i < puntos.Length) Then
                DataGridViewEdicion.Rows.Add(i + 1, nombreCorredor, paisCorredor, puntos(i))
            Else
                DataGridViewEdicion.Rows.Add(i + 1, nombreCorredor, paisCorredor, 0) 'Y si haces un ToString tras objeto piloto?
            End If
            dorsalesDisponibles.RemoveAt(j)
        Next i

        DataGridView2.Columns.Add("Piloto", "Piloto")
        DataGridView2.Columns.Add("Pais", "Pais")
        DataGridViewEdicion.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewEdicion.ColumnHeadersDefaultCellStyle.Font = New Font(DataGridViewEdicion.Font, FontStyle.Bold)
        DataGridView2.Rows.Add(DataGridViewEdicion.Rows(VMR).Cells(1).Value, DataGridViewEdicion.Rows(VMR).Cells(2).Value)

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

    Private Sub ListBoxGranPremio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxGranPremio.SelectedIndexChanged
        If ListBoxGranPremio.SelectedItem IsNot Nothing Then
            BtEditarGP.Enabled = True
            BtEliminarGP.Enabled = True
            Dim split As String() = ListBoxGranPremio.SelectedItem.ToString().Split(New [Char]() {" "c})
            Dim id As String
            id = split(0)
            Dim gp As GranPremio
            gp = New GranPremio
            gp.idGRAN_PREMIO = id
            Try
                gp.LeerGP()
                Me.GranPremio = gp
                Me.GranPremioEdi = gp
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
            TextBoxIDGranPremio.Text = gp.idGRAN_PREMIO
            TextBoxNombreGP.Text = gp.NOMBRE
            Dim myPais As New Pais(gp.PAIS)
            myPais.LeerPais()
            Dim textoBuscado As String = myPais.Nombre

            CBPaisGP.Items.Clear()
            For Each pais As Pais In myPais.PaisDAO.LeerTodas
                CBPaisGP.Items.Add(pais.Nombre)
            Next

            For Each item As Object In CBPaisGP.Items
                If item.ToString().Contains(textoBuscado) Then
                    CBPaisGP.SelectedItem = item
                    Exit For
                End If
            Next
        Else
            BtEliminarGP.Enabled = False
            BtEditarGP.Enabled = False
        End If
    End Sub

    Private Function comprobarCamposGP() As Boolean
        Dim camposValidos As Boolean
        camposValidos = True
        If TextBoxNombreGP.Text = "" Or TextBoxIDGranPremio.Text = "" Or CBPaisGP.SelectedItem Is Nothing Then
            camposValidos = False
            MsgBox("Es necesario que rellene todos los campos en Datos Personales", vbExclamation)
        End If
        Return camposValidos
    End Function

    Private Sub ModoEditarAñadirGP(mode As Boolean)
        GBOpcionesGP.Enabled = Not mode
        ListBoxGranPremio.Enabled = Not mode
        GBDatosGranPremio.Enabled = mode
        GBBotonesEdicionGP.Enabled = mode
    End Sub

    Private Sub BtAñadirGP_Click(sender As Object, e As EventArgs) Handles BtAñadirGP.Click
        estadoGP = 0
        ModoEditarAñadirGP(True)
        BtLimpiarGP.PerformClick()
    End Sub

    Private Sub BtEditarGP_Click(sender As Object, e As EventArgs) Handles BtEditarGP.Click
        estadoGP = 1
        ModoEditarAñadirGP(True)
    End Sub

    Private Sub BtEliminarGP_Click(sender As Object, e As EventArgs) Handles BtEliminarGP.Click
        Dim borrar As Integer
        borrar = MsgBox("¿Estás seguro que desea eliminar el Gran Premio seleccionado?", +vbYesNo + vbDefaultButton1 + vbQuestion, "Eliminar Gran Premio")
        If (borrar = vbYes) Then
            Try
                Me.GranPremio.BorrarGP()
                ListBoxGranPremio.Items.RemoveAt(ListBoxGranPremio.SelectedIndex)
            Catch ex As Exception
                MsgBox("No se pudo borrar el Gran Premio al estar vinculado con otros datos.", vbExclamation)
            End Try
        End If
    End Sub

    Private Sub BtCancelarGP_Click(sender As Object, e As EventArgs) Handles BtCancelarGP.Click
        Dim volver As Integer
        volver = MsgBox("¿Estas seguro de que desea volver? Se perderán los cambios no guardados.", vbYesNo + vbDefaultButton2 + vbQuestion, "Cerrar modo edición.")
        If (volver = vbYes) Then
            DeshacerCamposGP()
        End If
    End Sub

    Private Sub DeshacerCamposGP()
        estadoGP = -1
        TextBoxNombreGP.Undo()
        TextBoxNombreGP.ClearUndo()
        TextBoxIDGranPremio.Undo()
        TextBoxIDGranPremio.ClearUndo()
        ModoEditarAñadirGP(False)
    End Sub

    Private Sub BtLimpiarGP_Click(sender As Object, e As EventArgs) Handles BtLimpiarGP.Click
        LimpiarFormEditaGP()
    End Sub

    Private Sub LimpiarFormEditaGP()
        LimpiarTextoFormularioGeneral(GBDatosGranPremio)
    End Sub

    Private Sub BtGuardarGP_Click(sender As Object, e As EventArgs) Handles BtGuardarGP.Click
        Dim indice As Integer
        If comprobarCamposGP() Then
            Dim check As Boolean = True
            Dim myGP As New GranPremio()
            myGP.LeerTodosGP()
            For Each iterGP As GranPremio In myGP.GPDAO.LeerTodas()
                If iterGP.idGRAN_PREMIO = TextBoxIDGranPremio.Text() Then
                    check = False
                End If
            Next
            If (Not check And Me.estadoGP = 0) Then
                MsgBox("ID del Gran Premio ya existente.", vbExclamation)
            Else
                Dim gp As New GranPremio()
                Dim pais As New Pais()
                gp.NOMBRE = TextBoxNombreGP.Text()
                gp.idGRAN_PREMIO = TextBoxIDGranPremio.Text()
                pais.Nombre = CBPaisGP.SelectedItem
                gp.PAIS = pais.GetAbreviacion(pais.Nombre)
                If (Me.estadoGP = 0) Then
                    Try
                        gp.InsertarGP()
                        ListBoxGranPremio.Items.Add(gp.idGRAN_PREMIO & " - " & gp.NOMBRE)
                        MsgBox("Se ha añadido a la base de datos el Gran Premio " & gp.NOMBRE & " correctamente.", vbInformation)
                        ModoEditarAñadirGP(False)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, ex.Source)
                    End Try
                ElseIf (Me.estadoGP = 1) Then
                    Try
                        Dim actualizar As Integer
                        actualizar = gp.ActualizarGP()
                        If (actualizar <> 1) Then
                            MsgBox("Error. No se pudo modificar.", vbCritical)
                        Else
                            MsgBox("Gran Premio modificado con éxito.", vbInformation)
                            indice = ListBoxGranPremio.SelectedIndex
                            ListBoxGranPremio.Items.RemoveAt(indice)
                            ListBoxGranPremio.Items.Insert(indice, gp.idGRAN_PREMIO & " - " & gp.NOMBRE)
                        End If
                        estadoGP = -1
                        ModoEditarAñadirGP(False)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, ex.Source)
                    End Try
                End If
            End If
        End If
    End Sub

    '--------------------------------'
    '   MÉTODOS PARA CONFIGURACION   '
    '   COMO PAISES INFORMES ETC     '
    '--------------------------------'

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
        borrar = MsgBox("¿Estás seguro de que desea eliminar el pais seleccionado?", +vbYesNo + vbDefaultButton2, "Eliminar Persona.")
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

    Private Sub BtAñadirPil_Click(sender As Object, e As EventArgs) Handles BtAñadirPil.Click
        Me.estadoPais = 0
        GBBotonesEdicionPiloto.Enabled = True
        GBDatosPersonales.Enabled = True
        BtAñadirPais.Enabled = False
        BtEditarPais.Enabled = False
        BtBorrarPais.Enabled = False
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
            BtAñadirPais.Enabled = True
            BtBorrarPais.Enabled = True
            ListBoxPaises.Enabled = True
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

    '--------------------------------'
    '        OTROS MÉTODOS           '
    '--------------------------------'
    Private Sub CBPaisPiloto_Click(sender As Object, e As EventArgs) Handles CBPaisPiloto.Click
        Dim myPais As New Pais()
        CBPaisPiloto.Items.Clear()
        For Each pais As Pais In myPais.PaisDAO.LeerTodas
            CBPaisPiloto.Items.Add(pais.Nombre)

        Next
    End Sub
    Private Sub CBPaisCircuito_Click(sender As Object, e As EventArgs) Handles CBPaisCircuito.Click
        Dim myPais As New Pais()
        CBPaisCircuito.Items.Clear()
        For Each pais As Pais In myPais.PaisDAO.LeerTodas
            CBPaisCircuito.Items.Add(pais.Nombre)
        Next
    End Sub

    Private Sub CBPaisGP_Click(sender As Object, e As EventArgs) Handles CBPaisGP.Click
        Dim myPais As New Pais()
        CBPaisCircuito.Items.Clear()
        For Each pais As Pais In myPais.PaisDAO.LeerTodas
            CBPaisCircuito.Items.Add(pais.Nombre)
        Next
    End Sub

    Private Sub ButtonSelectInformePil_Click(sender As Object, e As EventArgs) Handles ButtonSelectInformePil.Click
        GroupBoxInformePil.Visible = True
        GroupBoxInformePil2.Visible = True
        generarFichaPiloto(piloto)

    End Sub

    Private Sub ButtonInformePil2_Click(sender As Object, e As EventArgs) Handles ButtonInformePil2.Click
        Dim myGP As Collection
        Dim edi As New Edicion
        If String.IsNullOrEmpty(ListBoxEdicionGPInforme.Text) Then
            MsgBox("Es necesario que seleccione un Gran Premio", vbExclamation)
        Else
            myGP = edi.EdDAO.GetGPAnio(ListBoxAñoInforme.SelectedItem.ToString())
            Dim mensaje As String
            mensaje = ""


            For Each edi In myGP
                Dim GP As New GranPremio(edi.idGRAN_PREMIO)
                GP.LeerGP()
                Dim clas As New ClasificacionCarrera
                If (Not IsNothing(clas.ResultadoPiloto(piloto.idPILOTO, edi.idEDICION))) And (clas.ResultadoPiloto(piloto.idPILOTO, edi.idEDICION) <> 0) Then
                    mensaje = mensaje & GP.NOMBRE & " | Edición: " & edi.NOMBRE & " | Resultado: " & clas.ResultadoPiloto(piloto.idPILOTO, edi.idEDICION) & " | Vuelta rápida: " & vueltaRapida(edi.PILOTO_VR, piloto.idPILOTO) & vbNewLine
                End If
            Next

            MessageBox.Show(mensaje)
        End If
    End Sub

    Private Sub ButtonInformePil_Click(sender As Object, e As EventArgs) Handles ButtonInformePil.Click

        Dim myGP As Collection
        Dim edi As New Edicion
        If String.IsNullOrEmpty(ListBoxAñoInforme.Text) Then
            MsgBox("Es necesario que seleccione un año", vbExclamation)
        Else
            myGP = edi.EdDAO.GetGPAnio(ListBoxAñoInforme.SelectedItem.ToString())
            Dim mensaje As String
            mensaje = ""


            For Each edi In myGP
                Dim GP As New GranPremio(edi.idGRAN_PREMIO)
                GP.LeerGP()
                Dim clas As New ClasificacionCarrera
                If (Not IsNothing(clas.ResultadoPiloto(piloto.idPILOTO, edi.idEDICION))) And (clas.ResultadoPiloto(piloto.idPILOTO, edi.idEDICION) <> 0) Then
                    Dim posicion As Integer = clas.ResultadoPiloto(piloto.idPILOTO, edi.idEDICION)
                    Dim VR As String = vueltaRapida(edi.PILOTO_VR, piloto.idPILOTO)
                    mensaje = mensaje & GP.NOMBRE & " | Edición: " & edi.NOMBRE & " | Puntos: " & posicion & " | Resultado: " & CalcularPuntuacion(posicion, VR) & " | Vuelta rápida: " & VR & vbNewLine
                End If
            Next

            MessageBox.Show(mensaje)
        End If
    End Sub

    Private Function vueltaRapida(pILOTO_VR As Integer, idPILOTO As String) As String

        If pILOTO_VR = idPILOTO Then
            Return "Sí"
        Else
            Return "No"
        End If
    End Function
End Class
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types

Public Class CONSULTAR
    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnTransmitir.Click
        Using conn As New OracleConnection(My.Settings.conexion)
            Try
                conn.Open()
                Dim fecha As String = Format(CDate(Date.Now), "dd/MM/yyyy")
                Dim respuesta As Boolean = False

                ' Crear el comando para llamar al procedimiento almacenado
                Dim command As New OracleCommand("INSERTAR_CUENTASPAGAR", conn)
                command.CommandType = CommandType.StoredProcedure

                ' Agregar los parámetros al comando
                command.Parameters.Add(New OracleParameter("Acr_Nombre", OracleDbType.Varchar2)).Value = txtNombre.Text
                command.Parameters.Add(New OracleParameter("Acr_Direccion", OracleDbType.Varchar2)).Value = txtDireccion.Text
                command.Parameters.Add(New OracleParameter("Acr_Correo", OracleDbType.Varchar2)).Value = txtcorreo.Text
                command.Parameters.Add(New OracleParameter("Acr_Telefono", OracleDbType.Varchar2)).Value = txtTelefono.Text
                command.Parameters.Add(New OracleParameter("Cpg_NoFactura", OracleDbType.Varchar2)).Value = txtNoFactura.Text
                command.Parameters.Add(New OracleParameter("Cpg_Fecha", OracleDbType.Date)).Value = DateTime.Parse(fecha)
                command.Parameters.Add(New OracleParameter("Cpg_TotalDeuda", OracleDbType.Decimal)).Value = Convert.ToDecimal(txtTotalDeuda.Text)
                command.Parameters.Add(New OracleParameter("Cpg_Descripcion", OracleDbType.Varchar2)).Value = txtDescripcion.Text

                ' Agregar el parámetro de salida para filas afectadas
                Dim filasAfectadasParam As New OracleParameter("filas_afectadas", OracleDbType.Int32)
                filasAfectadasParam.Direction = ParameterDirection.Output
                command.Parameters.Add(filasAfectadasParam)

                ' Ejecutar el comando
                command.ExecuteNonQuery()

                ' Obtener el valor del parámetro de salida
                Dim filasAfectadas As Integer = CType(filasAfectadasParam.Value, Oracle.ManagedDataAccess.Types.OracleDecimal).ToInt32()

                ' Verificar si se afectaron filas
                respuesta = filasAfectadas > 0

                ' Mostrar mensaje de éxito o error
                If respuesta Then
                    MessageBox.Show("Datos guardados correctamente.")
                    btnTransmitir.Enabled = False
                    txtNombre.Enabled = False
                    txtDireccion.Enabled = False
                    txtcorreo.Enabled = False
                    txtTelefono.Enabled = False
                    txtNoFactura.Enabled = False
                    txtTotalDeuda.Enabled = False
                    txtDescripcion.Enabled = False
                Else
                    MessageBox.Show("Error al guardar los datos en la tabla CPagar.")
                End If

            Catch ex As Exception
                MessageBox.Show("Error al conectar a la base de datos: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles txtTotalDeuda.TextChanged
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click


        txtNombre.Clear()
        txtDireccion.Clear()
        txtcorreo.Clear()
        txtTelefono.Clear()
        txtNoFactura.Clear()
        txtFecha.Clear()
        txtTotalDeuda.Clear()
        txtDescripcion.Clear()


    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            MsgBox("Ingresar solo datos numericos")
        End If
    End Sub

    Private Sub TxtIDdos_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TxtIDdos_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar)
        If Not IsNumeric(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            MsgBox("Ingresar solo datos numericos")
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub txtDireccion_TextChanged(sender As Object, e As EventArgs) Handles txtDireccion.TextChanged

    End Sub

    ' Private Sub txtTelefono_TextChanged(sender As Object, e As EventArgs) Handles txtTelefono.TextChanged

    ' End Sub

    Private Sub txtTelefono_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTelefono.KeyPress
        ' Permitir solo la entrada de números y teclas de control (como Backspace)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MsgBox("Ingresar solo datos numéricos")
        End If

        ' Evitar la entrada de más de 8 dígitos
        If Char.IsDigit(e.KeyChar) AndAlso txtTelefono.Text.Length >= 8 Then
            e.Handled = True
            MsgBox("Solo se permiten 8 dígitos")
        End If
    End Sub

    'RESTRICCIONES PARA INGRESAR UN NUMERO TELEFONICO
    Private Sub txtTelefono_TextChanged(sender As Object, e As EventArgs) Handles txtTelefono.TextChanged
        ' Si la longitud del texto es mayor a 8, truncar el texto a los primeros 8 dígitos
        If txtTelefono.Text.Length > 8 Then
            txtTelefono.Text = txtTelefono.Text.Substring(0, 8)
            ' Colocar el cursor al final del texto después de truncarlo
            txtTelefono.SelectionStart = txtTelefono.Text.Length
        End If
    End Sub

    'RESTRICCIONES PARA INGRESAR EL CORREO
    Private Sub txtcorreo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcorreo.KeyPress
        ' Permitir solo letras, números, @, ., y controles como backspace
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso e.KeyChar <> "@"c AndAlso e.KeyChar <> "."c AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub




    'RESTRICCIONES PARA INGRESAR LA FECHA
    Private Sub txtFecha_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFecha.KeyPress
        ' Permitir solo números, barra y teclas de control (como Backspace)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "/" Then
            e.Handled = True
            MsgBox("Solo se permiten números y el carácter '/'.")
        End If
    End Sub


    Private Function EsFechaValida(fecha As String) As Boolean
        ' Verificar si el formato es correcto usando DateTime.TryParseExact
        Dim formato As String = "dd/MM/yyyy"
        Dim fechaTemp As DateTime
        Return DateTime.TryParseExact(fecha, formato, System.Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, fechaTemp)
    End Function

    Private Sub txtDescripcion_TextChanged(sender As Object, e As EventArgs) Handles txtDescripcion.TextChanged

    End Sub



    Private Sub txtNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNombre.KeyPress
        ' Permitir solo letras y teclas de control (como Backspace)
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MsgBox("Solo se permiten letras.")
        End If
    End Sub

    Private Sub txtNoFactura_TextChanged(sender As Object, e As EventArgs) Handles txtNoFactura.TextChanged

    End Sub

    Private Sub txtNoFactura_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoFactura.KeyPress
        ' Permitir solo la entrada de números y teclas de control (como Backspace)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MsgBox("Solo se permiten números.")
        End If

        ' Limitar a 6 dígitos
        If Char.IsDigit(e.KeyChar) AndAlso txtNoFactura.Text.Length >= 6 Then
            e.Handled = True
            MsgBox("Solo se permiten hasta 6 dígitos.")
        End If
    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles Label20.Click

    End Sub
End Class

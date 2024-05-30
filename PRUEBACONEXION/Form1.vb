Imports Oracle.ManagedDataAccess.Client
Imports System.EnterpriseServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Apertura_Cuenta.Click
        Dim ID As String = textiden.Text.Trim()

        If ID <> "" Then
            Using conn As New OracleConnection(My.Settings.conexion)
                Try
                    conn.Open()

                    ' Crear el comando para llamar al procedimiento almacenado
                    Dim command As New OracleCommand("obtener_datos_cpagar", conn)
                    command.CommandType = CommandType.StoredProcedure

                    ' Agregar el parámetro de entrada para el ID
                    command.Parameters.Add(New OracleParameter("p_id", OracleDbType.Int32)).Value = Convert.ToInt32(ID)

                    ' Agregar el parámetro de salida para el cursor
                    Dim cursorParam As New OracleParameter("p_cursor", OracleDbType.RefCursor)
                    cursorParam.Direction = ParameterDirection.Output
                    command.Parameters.Add(cursorParam)

                    ' Ejecutar el comando
                    Dim reader As OracleDataReader = command.ExecuteReader()

                    ' Leer los datos devueltos por el cursor
                    If reader.Read() Then
                        Dim descripcion As String = reader("DESCRIPCION").ToString()
                        Dim fecha As String = reader("FECHA").ToString()
                        Dim acreedor As String = reader("ACREEDOR").ToString()
                        Dim factura As String = reader("NOFACTURA").ToString()
                        Dim totaldeuda As String = reader("TOTALDEUDA").ToString()

                        ' Actualizar los controles del formulario con los datos obtenidos
                        textBox6.Text = descripcion
                        textBox5.Text = fecha
                        textBox4.Text = acreedor
                        textBox2.Text = factura
                        textBox10.Text = totaldeuda

                        If totaldeuda = "0" Then
                            textBox7.Text = "CANCELADA"
                        Else
                            textBox7.Text = "EN PROCESO"
                        End If
                    Else
                        MessageBox.Show("No se encontró una factura con ese ID.")
                    End If

                    ' Cerrar el lector
                    reader.Close()

                Catch ex As Exception
                    MessageBox.Show("Error al buscar la factura: " & ex.Message)
                End Try
            End Using
        Else
            MessageBox.Show("Por favor, ingrese un ID válido.")
        End If
    End Sub





    Private Sub panel1_Paint(sender As Object, e As PaintEventArgs) Handles panel1.Paint

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub pictureBox3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub INICIOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles INICIOToolStripMenuItem.Click

    End Sub

    Private Sub button3_Click(sender As Object, e As EventArgs) Handles button3.Click
        ' Me.Close()

        textiden.Text = ""
        textBox2.Text = ""
        textBox10.Text = ""
        textBox4.Text = ""
        textBox5.Text = ""
        textBox6.Text = ""
        textBox7.Text = ""


    End Sub

    Private Sub label9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CONSULTARCUENTAToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CONSULTARCUENTAToolStripMenuItem.Click
        CONSULTAR.Show()
    End Sub

    Private Sub ELIMINARCUENTAToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub textBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox2.KeyPress
        ' Suprimir cualquier entrada
        e.Handled = True
        MsgBox("Este campo no permite la entrada de datos.")
    End Sub

    Private Sub textBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox4.KeyPress
        ' Suprimir cualquier entrada
        e.Handled = True
        MsgBox("Este campo no permite la entrada de datos.")
    End Sub

    Private Sub textBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox7.KeyPress
        ' Suprimir cualquier entrada
        e.Handled = True
        MsgBox("Este campo no permite la entrada de datos.")
    End Sub

    Private Sub textBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox5.KeyPress
        ' Suprimir cualquier entrada
        e.Handled = True
        MsgBox("Este campo no permite la entrada de datos.")
    End Sub

    Private Sub textBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox6.KeyPress
        ' Suprimir cualquier entrada
        e.Handled = True
        MsgBox("Este campo no permite la entrada de datos.")
    End Sub

    Private Sub textBox10_KeyPress(sender As Object, e As KeyPressEventArgs) Handles textBox10.KeyPress
        ' Suprimir cualquier entrada
        e.Handled = True
        MsgBox("Este campo no permite la entrada de datos.")
    End Sub

    Private Sub TRANSACCIONESToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TRANSACCIONESToolStripMenuItem.Click
        TRANSACCIONES.Show()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub textBox5_TextChanged(sender As Object, e As EventArgs) Handles textBox5.TextChanged

    End Sub
End Class

Public Class FrmLogin

    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        If txtUsuario.Text.Trim() = "" OrElse txtClave.Text = "" Then
            MessageBox.Show("Ingrese usuario y contraseña.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim estado As String = ""
            Dim turno As String = ""
            If Not Seguridad.Validar(txtUsuario.Text.Trim(), txtClave.Text, estado, turno) Then
                MessageBox.Show("Usuario o contraseña incorrectos (o usuario inactivo).", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtClave.Clear()
                txtClave.Focus()
                Return
            End If

            ' El cajero solo puede entrar durante su turno
            If estado = "Cajero" AndAlso turno <> Turnos.Actual() Then
                MessageBox.Show("Su turno es " & Turnos.Mostrar(turno) & " (" & Turnos.Horario(turno) & ")." & vbCrLf &
                                "Ahora corresponde el turno de " & Turnos.Mostrar(Turnos.Actual()) & "." & vbCrLf &
                                "No puede iniciar sesión fuera de su turno.", "Fuera de turno", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtClave.Clear()
                Return
            End If

            Variable.Usuario = txtUsuario.Text.Trim()
            Variable.Estado = estado
            Variable.Turno = turno

            Dim f As New FrmProductos()
            f.Show()
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Application.Exit()
    End Sub
End Class

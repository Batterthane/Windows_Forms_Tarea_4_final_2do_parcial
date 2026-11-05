Public Class FrmCajeros

    Private Sub FrmCajeros_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboTurno.Items.AddRange(New Object() {"Mañana", "Tarde", "Noche"})
        CargarLista()
    End Sub

    Private Sub FrmCajeros_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If Not Variable.EsAdmin Then
            MessageBox.Show("Solo el administrador puede gestionar cajeros.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
        End If
    End Sub

    Private Sub CargarLista()
        Try
            dgvCajeros.DataSource = MetodosUsuarios.ListarCajeros()
            If dgvCajeros.Columns.Contains("Id") Then dgvCajeros.Columns("Id").Visible = False
        Catch ex As Exception
            MessageBox.Show("Error al cargar cajeros: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        Dim usuario As String = txtUsuarioN.Text.Trim()
        If usuario.Length < 3 OrElse usuario.Contains(" ") Then
            MessageBox.Show("El usuario debe tener al menos 3 caracteres y no puede llevar espacios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtUsuarioN.Focus()
            Return
        End If
        If txtClave.Text.Length < 6 Then
            MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtClave.Focus()
            Return
        End If
        If txtClave.Text <> txtConfirmar.Text Then
            MessageBox.Show("La contraseña y su confirmación no coinciden.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtConfirmar.Focus()
            Return
        End If
        If cboTurno.SelectedIndex < 0 Then
            MessageBox.Show("Seleccione el turno del cajero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboTurno.Focus()
            Return
        End If

        Dim turno As String = cboTurno.Text.Replace("ñ", "n")      ' Mañana -> Manana (valor guardado)
        Try
            MetodosUsuarios.CrearCajero(usuario, Seguridad.Hash(txtClave.Text), turno)
            MessageBox.Show("Cajero '" & usuario & "' creado para el turno de " & cboTurno.Text & ".", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtUsuarioN.Clear()
            txtClave.Clear()
            txtConfirmar.Clear()
            cboTurno.SelectedIndex = -1
            CargarLista()
        Catch ex As Exception
            MessageBox.Show("No se pudo crear el cajero: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnActivar_Click(sender As Object, e As EventArgs) Handles btnActivar.Click
        If dgvCajeros.CurrentRow Is Nothing Then
            MessageBox.Show("Seleccione un cajero de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim drv As DataRowView = TryCast(dgvCajeros.CurrentRow.DataBoundItem, DataRowView)
        If drv Is Nothing Then Return

        Dim id As Integer = Convert.ToInt32(drv("Id"))
        Dim estaActivo As Boolean = (drv("Situacion").ToString() = "Activo")
        Dim accion As String = If(estaActivo, "desactivar", "activar")

        If MessageBox.Show("¿Desea " & accion & " al cajero '" & drv("Usuario").ToString() & "'?", "Confirmar",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        Try
            MetodosUsuarios.CambiarActivo(id, Not estaActivo)
            CargarLista()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class

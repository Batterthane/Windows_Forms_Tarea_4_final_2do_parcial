Imports System.Data.SqlClient

Public Class FrmClientes

    Private Sub FrmClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarLista()
    End Sub

    Private Sub FrmClientes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If Not Variable.PuedeFacturar Then
            MessageBox.Show("Su usuario no puede gestionar clientes.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
        End If
    End Sub

    Private Sub CargarLista()
        Try
            dgvClientes.DataSource = MetodosClientes.Listar()
            If dgvClientes.Columns.Contains("Id") Then dgvClientes.Columns("Id").Visible = False
            If dgvClientes.Columns.Contains("Cedula") Then dgvClientes.Columns("Cedula").HeaderText = "Cédula"
            If dgvClientes.Columns.Contains("Telefono") Then dgvClientes.Columns("Telefono").HeaderText = "Teléfono"
        Catch ex As Exception
            MessageBox.Show("Error al cargar clientes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If txtNombre.Text.Trim() = "" Then
            MessageBox.Show("El nombre del cliente es obligatorio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNombre.Focus()
            Return
        End If

        Try
            MetodosClientes.Crear(txtNombre.Text.Trim(), txtCedula.Text.Trim(), txtTelefono.Text.Trim())
            txtNombre.Clear()
            txtCedula.Clear()
            txtTelefono.Clear()
            CargarLista()
            MessageBox.Show("Cliente registrado.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNombre.Focus()
        Catch ex As SqlException When ex.Number = 2601 OrElse ex.Number = 2627
            MessageBox.Show("Ya existe un cliente con esa cédula.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar el cliente: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class

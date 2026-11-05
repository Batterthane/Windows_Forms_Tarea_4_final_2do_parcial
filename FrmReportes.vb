Imports System.Globalization
Imports System.IO

Public Class FrmReportes

    Private ReadOnly Cult As CultureInfo = CultureInfo.InvariantCulture
    Private ultimaTabla As DataTable
    Private ultimoTitulo As String = ""
    Private ultimoFiltro As String = ""

    Private Sub FrmReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboTipo.Items.AddRange(New Object() {"Todas", "Contado", "Crédito"})
        cboTipo.SelectedIndex = 0

        cboCajero.Items.Add("Todos")
        Try
            For Each r As DataRow In MetodosUsuarios.ListarUsuariosVenta().Rows
                cboCajero.Items.Add(r("NombreUsuario").ToString())
            Next
        Catch ex As Exception
            MessageBox.Show("Error al cargar cajeros: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        cboCajero.SelectedIndex = 0

        cboReporte.Items.AddRange(New Object() {"Cuadre del día", "Ventas por cajero", "Inventario en stock",
                                                "Facturas al contado / a crédito", "5 clientes con más ventas"})
        cboReporte.SelectedIndex = 0        ' dispara ConfigurarFiltros
    End Sub

    Private Sub FrmReportes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If Not Variable.EsAdmin Then
            MessageBox.Show("Solo el administrador puede ver los reportes.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
        End If
    End Sub

    ' Activa solo los filtros que usa cada reporte
    Private Sub ConfigurarFiltros(sender As Object, e As EventArgs) Handles cboReporte.SelectedIndexChanged
        Dim idx As Integer = cboReporte.SelectedIndex
        lblDesde.Text = If(idx = 0, "Fecha", "Desde")
        dtpDesde.Enabled = (idx <> 2)
        lblHasta.Enabled = (idx = 1 OrElse idx = 3 OrElse idx = 4)
        dtpHasta.Enabled = lblHasta.Enabled
        cboCajero.Enabled = (idx = 0 OrElse idx = 1)
        lblCajero.Enabled = cboCajero.Enabled
        cboTipo.Enabled = (idx = 3)
        lblTipo.Enabled = cboTipo.Enabled
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Dim idx As Integer = cboReporte.SelectedIndex
        If idx < 0 Then
            MessageBox.Show("Seleccione un reporte.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim d As Date = dtpDesde.Value.Date
        Dim h As Date = dtpHasta.Value.Date
        If dtpHasta.Enabled AndAlso h < d Then
            MessageBox.Show("La fecha 'Hasta' no puede ser menor que 'Desde'.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim cajero As String = Nothing
        If cboCajero.Enabled AndAlso cboCajero.SelectedIndex > 0 Then cajero = cboCajero.Text
        Dim tipo As String = Nothing
        If cboTipo.SelectedIndex = 1 Then tipo = "Contado"
        If cboTipo.SelectedIndex = 2 Then tipo = "Credito"

        Dim periodo As String = "Desde: " & d.ToString("dd/MM/yyyy", Cult) & "   Hasta: " & h.ToString("dd/MM/yyyy", Cult)
        Dim t As DataTable = Nothing
        Dim titulo As String = ""
        Dim filtro As String = ""

        Try
            Select Case idx
                Case 0
                    t = MetodosReportes.Cuadre(d, cajero)
                    titulo = "CUADRE DEL DÍA"
                    filtro = "Fecha: " & d.ToString("dd/MM/yyyy", Cult) & "   Cajero: " & If(cajero Is Nothing, "Todos", cajero)
                Case 1
                    t = MetodosReportes.PorCajero(d, h, cajero)
                    titulo = "VENTAS POR CAJERO"
                    filtro = periodo & "   Cajero: " & If(cajero Is Nothing, "Todos", cajero)
                Case 2
                    t = MetodosReportes.Inventario()
                    titulo = "INVENTARIO EN STOCK"
                    filtro = "Existencias actuales"
                Case 3
                    t = MetodosReportes.Facturas(d, h, tipo)
                    titulo = "FACTURAS AL CONTADO / A CRÉDITO"
                    filtro = periodo & "   Condición: " & cboTipo.Text
                Case 4
                    t = MetodosReportes.TopClientes(d, h)
                    titulo = "5 CLIENTES CON MÁS VENTAS"
                    filtro = periodo
            End Select
        Catch ex As Exception
            MessageBox.Show("No se pudo generar el reporte: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        dgvReporte.DataSource = t
        DarFormato()
        ultimaTabla = t
        ultimoTitulo = titulo
        ultimoFiltro = filtro
        lblResumen.Text = titulo & ": " & t.Rows.Count.ToString() & " registro(s)"
        If t.Rows.Count = 0 Then
            MessageBox.Show("No hay datos para ese filtro.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub DarFormato()
        For Each col As DataGridViewColumn In dgvReporte.Columns
            If col.ValueType Is GetType(Decimal) Then
                col.DefaultCellStyle.Format = "N2"
                col.DefaultCellStyle.FormatProvider = Cult
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            ElseIf col.ValueType Is GetType(Integer) OrElse col.ValueType Is GetType(Long) Then
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            ElseIf col.ValueType Is GetType(DateTime) Then
                col.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
            End If
        Next
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        If ultimaTabla Is Nothing Then
            MessageBox.Show("Primero genere un reporte.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using sfd As New SaveFileDialog()
                sfd.Title = "Guardar reporte"
                sfd.Filter = "Documento PDF (*.pdf)|*.pdf|Archivo de texto (*.txt)|*.txt"
                sfd.FileName = "Reporte-" & DateTime.Now.ToString("yyyyMMdd-HHmm", Cult)
                If sfd.ShowDialog(Me) <> DialogResult.OK Then Return

                Dim ruta As String = sfd.FileName
                Dim ext As String = Path.GetExtension(ruta).ToLowerInvariant()
                If ext <> ".pdf" AndAlso ext <> ".txt" Then
                    ruta &= If(sfd.FilterIndex = 1, ".pdf", ".txt")
                End If

                ExportadorReporte.Guardar(ruta, ultimoTitulo, ultimoFiltro, ultimaTabla)

                If MessageBox.Show("Reporte guardado en:" & vbCrLf & ruta & vbCrLf & vbCrLf & "¿Desea abrirlo?", "Listo",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start(ruta)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("No se pudo exportar el reporte: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
End Class

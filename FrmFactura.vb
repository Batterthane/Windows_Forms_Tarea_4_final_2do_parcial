Imports System.Globalization
Imports System.IO

Public Class FrmFactura

    Private ReadOnly Cult As CultureInfo = CultureInfo.InvariantCulture
    Private ReadOnly dtDetalle As New DataTable()
    Private dtProductos As DataTable
    Private facturaId As Integer = 0          ' 0 = factura nueva (sin guardar)
    Private totalFactura As Decimal = 0D      ' total a pagar mostrado en pantalla

    ' ---------- Carga ----------
    Private Sub FrmFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtDetalle.Columns.Add("Codigo", GetType(Integer))
        dtDetalle.Columns.Add("Nombre", GetType(String))
        dtDetalle.Columns.Add("Cantidad", GetType(Integer))
        dtDetalle.Columns.Add("Precio", GetType(Decimal))
        dtDetalle.Columns.Add("Itbis", GetType(Decimal))
        dtDetalle.Columns.Add("Importe", GetType(Decimal))
        dgvDetalle.DataSource = dtDetalle
        dgvDetalle.Columns("Precio").HeaderText = "Valor"

        For Each nombre As String In New String() {"Precio", "Itbis", "Importe"}
            With dgvDetalle.Columns(nombre).DefaultCellStyle
                .Format = "N2"
                .FormatProvider = Cult
                .Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Next
        dgvDetalle.Columns("Cantidad").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        txtUsuario.Text = Variable.Usuario & If(Variable.EsCajero, " (" & Turnos.Mostrar(Variable.Turno) & ")", "")
        lblItbisT.Text = "Total ITBIS (" & (MetodosFacturas.TasaItbis * 100D).ToString("0", Cult) & "%):"
        CargarProductos()
        CargarClientes(0)
        NuevaFactura()
    End Sub

    ' Seguridad extra: solo administrador y cajeros facturan
    Private Sub FrmFactura_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If Not Variable.PuedeFacturar Then
            MessageBox.Show("Su usuario no puede crear facturas.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Close()
        End If
    End Sub

    Private Sub FrmFactura_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If facturaId = 0 AndAlso dtDetalle.Rows.Count > 0 Then
            If MessageBox.Show("Hay una factura sin guardar. ¿Desea salir y perderla?", "Factura sin guardar",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

    ' ---------- Atajo F5: imprimir ----------
    ' ProcessCmdKey recibe la tecla antes que cualquier control (lista, cantidad, tabla),
    ' así F5 funciona sin importar dónde esté el cursor.
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.F5 Then
            Exportar()
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    ' ---------- Productos ----------
    Private Sub CargarProductos()
        Try
            Dim t As DataTable = MetodosProductos.Listar()
            t.Columns.Add("Texto", GetType(String))
            For Each r As DataRow In t.Rows
                r("Texto") = r("Codigo").ToString() & " - " & r("Nombre").ToString() & " (" & r("Marca").ToString() & ")"
            Next
            dtProductos = t
            t.DefaultView.RowFilter = "Stock > 0"          ' solo productos con existencias
            cboProducto.DataSource = Nothing
            cboProducto.DisplayMember = "Texto"
            cboProducto.ValueMember = "Codigo"
            cboProducto.DataSource = t.DefaultView
            cboProducto.SelectedIndex = -1
        Catch ex As Exception
            MessageBox.Show("Error al cargar productos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ProductoSeleccionado() As DataRow
        If cboProducto.SelectedIndex < 0 AndAlso cboProducto.Text.Trim() <> "" Then
            Dim idx As Integer = cboProducto.FindStringExact(cboProducto.Text)
            If idx >= 0 Then cboProducto.SelectedIndex = idx
        End If
        If cboProducto.SelectedIndex < 0 Then Return Nothing
        Dim drv As DataRowView = TryCast(cboProducto.SelectedItem, DataRowView)
        If drv Is Nothing Then Return Nothing
        Return drv.Row
    End Function

    Private Sub cboProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProducto.SelectedIndexChanged
        Dim r As DataRow = Nothing
        Dim drv As DataRowView = TryCast(cboProducto.SelectedItem, DataRowView)
        If drv IsNot Nothing Then r = drv.Row
        If r Is Nothing Then
            lblInfo.Text = "Precio: -   Stock disponible: -"
        Else
            lblInfo.Text = "Precio: " & Convert.ToDecimal(r("Precio")).ToString("N2", Cult) & "   Stock disponible: " & r("Stock").ToString()
        End If
    End Sub

    ' ---------- Clientes ----------
    Private Sub CargarClientes(seleccionarId As Integer)
        Try
            Dim t As DataTable = MetodosClientes.Listar()
            Dim v As New DataTable()
            v.Columns.Add("Id", GetType(Integer))
            v.Columns.Add("Texto", GetType(String))
            v.Rows.Add(0, "Consumidor final")
            For Each r As DataRow In t.Rows
                Dim texto As String = r("Nombre").ToString()
                If r("Cedula").ToString() <> "" Then texto &= " - " & r("Cedula").ToString()
                v.Rows.Add(Convert.ToInt32(r("Id")), texto)
            Next
            cboCliente.DataSource = Nothing
            cboCliente.DisplayMember = "Texto"
            cboCliente.ValueMember = "Id"
            cboCliente.DataSource = v
            Dim idx As Integer = 0
            For i As Integer = 0 To v.Rows.Count - 1
                If Convert.ToInt32(v.Rows(i)("Id")) = seleccionarId Then idx = i
            Next
            cboCliente.SelectedIndex = idx
        Catch ex As Exception
            MessageBox.Show("Error al cargar clientes: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ClienteId() As Integer
        If cboCliente.SelectedIndex < 0 Then Return 0
        Dim drv As DataRowView = TryCast(cboCliente.SelectedItem, DataRowView)
        If drv Is Nothing Then Return 0
        Return Convert.ToInt32(drv("Id"))
    End Function

    Private Function ClienteNombre() As String
        Dim drv As DataRowView = TryCast(cboCliente.SelectedItem, DataRowView)
        If drv Is Nothing Then Return "Consumidor final"
        Return drv("Texto").ToString()
    End Function

    Private Sub btnClientes_Click(sender As Object, e As EventArgs) Handles btnClientes.Click
        Dim actual As Integer = ClienteId()
        Using f As New FrmClientes()
            f.ShowDialog(Me)
        End Using
        CargarClientes(actual)
    End Sub

    ' ---------- Detalle ----------
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If facturaId <> 0 Then Return

        Dim p As DataRow = ProductoSeleccionado()
        If p Is Nothing Then
            MessageBox.Show("Seleccione un producto de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboProducto.Focus()
            Return
        End If

        Dim cant As Integer = CInt(nudCantidad.Value)
        Dim codigo As Integer = Convert.ToInt32(p("Codigo"))
        Dim stock As Integer = Convert.ToInt32(p("Stock"))
        Dim precio As Decimal = Convert.ToDecimal(p("Precio"))

        Dim existentes As DataRow() = dtDetalle.Select("Codigo = " & codigo.ToString())
        Dim yaEnFactura As Integer = 0
        If existentes.Length > 0 Then yaEnFactura = Convert.ToInt32(existentes(0)("Cantidad"))

        If cant + yaEnFactura > stock Then
            MessageBox.Show("Stock insuficiente de """ & p("Nombre").ToString() & """." & vbCrLf &
                            "Disponible: " & stock & "   Ya en la factura: " & yaEnFactura,
                            "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim importe As Decimal = (cant + yaEnFactura) * precio
        If existentes.Length > 0 Then
            existentes(0)("Cantidad") = cant + yaEnFactura
            existentes(0)("Itbis") = MetodosFacturas.ItbisLinea(importe)
            existentes(0)("Importe") = importe
        Else
            dtDetalle.Rows.Add(codigo, p("Nombre").ToString(), cant, precio, MetodosFacturas.ItbisLinea(importe), importe)
        End If

        Recalcular()
        nudCantidad.Value = 1
        cboProducto.SelectedIndex = -1
        cboProducto.Focus()
    End Sub

    Private Sub btnQuitar_Click(sender As Object, e As EventArgs) Handles btnQuitar.Click
        If facturaId <> 0 OrElse dgvDetalle.CurrentRow Is Nothing Then Return
        Dim drv As DataRowView = TryCast(dgvDetalle.CurrentRow.DataBoundItem, DataRowView)
        If drv Is Nothing Then Return
        dtDetalle.Rows.Remove(drv.Row)
        Recalcular()
    End Sub

    Private Sub Recalcular()
        Dim subtotal As Decimal = 0D
        Dim itbis As Decimal = 0D
        For Each r As DataRow In dtDetalle.Rows
            subtotal += Convert.ToDecimal(r("Importe"))
            itbis += Convert.ToDecimal(r("Itbis"))
        Next
        totalFactura = subtotal + itbis
        lblSubtotal.Text = subtotal.ToString("N2", Cult)
        lblItbis.Text = itbis.ToString("N2", Cult)
        lblTotal.Text = totalFactura.ToString("N2", Cult)
    End Sub

    ' ---------- Cobrar y guardar ----------
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        CobrarYGuardar()
    End Sub

    Private Sub CobrarYGuardar()
        If facturaId <> 0 Then
            MessageBox.Show("Esta factura ya fue guardada. Pulse 'Nueva factura' para otra venta.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If dtDetalle.Rows.Count = 0 Then
            MessageBox.Show("Agregue al menos un artículo a la factura.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If Variable.EsCajero AndAlso Turnos.Actual() <> Variable.Turno Then
            MessageBox.Show("Su turno (" & Turnos.Mostrar(Variable.Turno) & ") ya terminó. No puede facturar fuera de su turno.", "Fuera de turno", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim esCredito As Boolean = rbCredito.Checked
        Dim cliId As Integer = ClienteId()
        If esCredito AndAlso cliId = 0 Then
            MessageBox.Show("Una venta a crédito requiere un cliente registrado." & vbCrLf & "Seleccione uno o pulse 'Clientes...' para crearlo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Pantalla de cobro: total a pagar, monto pagado y devuelta
        Dim pagado As Decimal = 0D
        Using f As New FrmCobro()
            f.TotalAPagar = totalFactura
            f.EsCredito = esCredito
            f.Cliente = ClienteNombre()
            If f.ShowDialog(Me) <> DialogResult.OK Then Return
            pagado = f.MontoPagado
        End Using

        If Guardar(esCredito, cliId, pagado) Then
            MessageBox.Show("Factura " & txtNumero.Text & " guardada." & vbCrLf & "Total: " & lblTotal.Text,
                            "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exportar()         ' luego se imprime la factura (PDF o TXT)
        End If
    End Sub

    Private Function Guardar(esCredito As Boolean, cliId As Integer, pagado As Decimal) As Boolean
        ' La base de datos solo recibe código y cantidad; el precio lo toma ella misma
        Dim tvp As New DataTable()
        tvp.Columns.Add("Codigo", GetType(Integer))
        tvp.Columns.Add("Cantidad", GetType(Integer))
        For Each r As DataRow In dtDetalle.Rows
            tvp.Rows.Add(Convert.ToInt32(r("Codigo")), Convert.ToInt32(r("Cantidad")))
        Next

        Dim cli As Object = Nothing
        If cliId <> 0 Then cli = cliId
        Dim pag As Object = Nothing
        If Not esCredito Then pag = pagado

        Try
            Dim id As Integer = MetodosFacturas.Crear(If(esCredito, "Credito", "Contado"), cli, Variable.Usuario, pag, tvp)
            facturaId = id
            CargarFacturaGuardada(id)
            Bloquear(True)
            CargarProductos()          ' el stock cambió
            Return True
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar la factura: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            CargarProductos()
            Return False
        End Try
    End Function

    ' Muestra los datos tal como quedaron en la base de datos
    Private Sub CargarFacturaGuardada(id As Integer)
        Dim ds As DataSet = MetodosFacturas.Obtener(id)
        Dim cab As DataRow = ds.Tables(0).Rows(0)
        txtNumero.Text = cab("Numero").ToString()
        txtFecha.Text = Convert.ToDateTime(cab("Fecha")).ToString("dd/MM/yyyy HH:mm", Cult)

        dtDetalle.Rows.Clear()
        For Each r As DataRow In ds.Tables(1).Rows
            dtDetalle.Rows.Add(Convert.ToInt32(r("Codigo")), r("Nombre").ToString(), Convert.ToInt32(r("Cantidad")),
                               Convert.ToDecimal(r("Precio")), Convert.ToDecimal(r("Itbis")), Convert.ToDecimal(r("Importe")))
        Next
        totalFactura = Convert.ToDecimal(cab("Total"))
        lblSubtotal.Text = Convert.ToDecimal(cab("Subtotal")).ToString("N2", Cult)
        lblItbis.Text = Convert.ToDecimal(cab("Itbis")).ToString("N2", Cult)
        lblTotal.Text = totalFactura.ToString("N2", Cult)
    End Sub

    Private Sub Bloquear(valor As Boolean)
        cboProducto.Enabled = Not valor
        nudCantidad.Enabled = Not valor
        btnAgregar.Enabled = Not valor
        btnQuitar.Enabled = Not valor
        btnGuardar.Enabled = Not valor
        rbContado.Enabled = Not valor
        rbCredito.Enabled = Not valor
        cboCliente.Enabled = Not valor
        btnClientes.Enabled = Not valor
    End Sub

    ' ---------- Nueva / Cerrar ----------
    Private Sub NuevaFactura()
        facturaId = 0
        dtDetalle.Rows.Clear()
        txtNumero.Text = "(nueva)"
        txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm", Cult)
        rbContado.Checked = True
        If cboCliente.Items.Count > 0 Then cboCliente.SelectedIndex = 0
        nudCantidad.Value = 1
        Bloquear(False)
        Recalcular()
        cboProducto.SelectedIndex = -1
    End Sub

    Private Sub btnNueva_Click(sender As Object, e As EventArgs) Handles btnNueva.Click
        If facturaId = 0 AndAlso dtDetalle.Rows.Count > 0 Then
            If MessageBox.Show("La factura actual no está guardada. ¿Descartarla?", "Nueva factura",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return
        End If
        NuevaFactura()
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    ' ---------- Imprimir (botón o F5) a .pdf o .txt ----------
    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Exportar()
    End Sub

    Private Sub Exportar()
        ' Una factura sin guardar primero se cobra y se guarda (y al terminar se imprime)
        If facturaId = 0 Then
            CobrarYGuardar()
            Return
        End If

        Try
            Dim ds As DataSet = MetodosFacturas.Obtener(facturaId)
            Dim cab As DataRow = ds.Tables(0).Rows(0)

            Using sfd As New SaveFileDialog()
                sfd.Title = "Guardar factura"
                sfd.Filter = "Documento PDF (*.pdf)|*.pdf|Archivo de texto (*.txt)|*.txt"
                sfd.FileName = "Factura-" & cab("Numero").ToString()
                If sfd.ShowDialog(Me) <> DialogResult.OK Then Return

                Dim ruta As String = sfd.FileName
                Dim ext As String = Path.GetExtension(ruta).ToLowerInvariant()
                If ext <> ".pdf" AndAlso ext <> ".txt" Then
                    ruta &= If(sfd.FilterIndex = 1, ".pdf", ".txt")
                End If

                ExportadorFactura.Guardar(ruta, cab, ds.Tables(1))

                If MessageBox.Show("Factura guardada en:" & vbCrLf & ruta & vbCrLf & vbCrLf & "¿Desea abrirla?", "Listo",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                    System.Diagnostics.Process.Start(ruta)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("No se pudo exportar la factura: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class

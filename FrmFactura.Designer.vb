<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFactura
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.gbFactura = New System.Windows.Forms.GroupBox()
        Me.lblNumero = New System.Windows.Forms.Label()
        Me.txtNumero = New System.Windows.Forms.TextBox()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.txtUsuario = New System.Windows.Forms.TextBox()
        Me.rbContado = New System.Windows.Forms.RadioButton()
        Me.rbCredito = New System.Windows.Forms.RadioButton()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.cboCliente = New System.Windows.Forms.ComboBox()
        Me.btnClientes = New System.Windows.Forms.Button()
        Me.gbArticulo = New System.Windows.Forms.GroupBox()
        Me.lblProducto = New System.Windows.Forms.Label()
        Me.cboProducto = New System.Windows.Forms.ComboBox()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.lblCantidad = New System.Windows.Forms.Label()
        Me.nudCantidad = New System.Windows.Forms.NumericUpDown()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.dgvDetalle = New System.Windows.Forms.DataGridView()
        Me.btnQuitar = New System.Windows.Forms.Button()
        Me.lblSubtotalT = New System.Windows.Forms.Label()
        Me.lblSubtotal = New System.Windows.Forms.Label()
        Me.lblItbisT = New System.Windows.Forms.Label()
        Me.lblItbis = New System.Windows.Forms.Label()
        Me.lblTotalT = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnNueva = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        CType(Me.nudCantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbFactura.SuspendLayout()
        Me.gbArticulo.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153)
        Me.lblTitulo.Location = New System.Drawing.Point(285, 15)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Text = "FACTURACIÓN"
        '
        'gbFactura
        '
        Me.gbFactura.Controls.Add(Me.lblNumero)
        Me.gbFactura.Controls.Add(Me.txtNumero)
        Me.gbFactura.Controls.Add(Me.lblFecha)
        Me.gbFactura.Controls.Add(Me.txtFecha)
        Me.gbFactura.Controls.Add(Me.lblUsuario)
        Me.gbFactura.Controls.Add(Me.txtUsuario)
        Me.gbFactura.Controls.Add(Me.rbContado)
        Me.gbFactura.Controls.Add(Me.rbCredito)
        Me.gbFactura.Controls.Add(Me.lblCliente)
        Me.gbFactura.Controls.Add(Me.cboCliente)
        Me.gbFactura.Controls.Add(Me.btnClientes)
        Me.gbFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular)
        Me.gbFactura.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204)
        Me.gbFactura.Location = New System.Drawing.Point(20, 55)
        Me.gbFactura.Name = "gbFactura"
        Me.gbFactura.Size = New System.Drawing.Size(720, 100)
        Me.gbFactura.Text = "Datos de Factura"
        '
        'lblNumero
        '
        Me.lblNumero.AutoSize = True
        Me.lblNumero.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblNumero.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblNumero.Location = New System.Drawing.Point(15, 31)
        Me.lblNumero.Name = "lblNumero"
        Me.lblNumero.Text = "Número"
        '
        'txtNumero
        '
        Me.txtNumero.Location = New System.Drawing.Point(75, 28)
        Me.txtNumero.Name = "txtNumero"
        Me.txtNumero.Size = New System.Drawing.Size(110, 20)
        Me.txtNumero.TabIndex = 0
        Me.txtNumero.ReadOnly = True
        Me.txtNumero.TabStop = False
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblFecha.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblFecha.Location = New System.Drawing.Point(205, 31)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Text = "Fecha"
        '
        'txtFecha
        '
        Me.txtFecha.Location = New System.Drawing.Point(250, 28)
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.Size = New System.Drawing.Size(130, 20)
        Me.txtFecha.TabIndex = 1
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.TabStop = False
        '
        'lblUsuario
        '
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblUsuario.Location = New System.Drawing.Point(400, 31)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Text = "Cajero"
        '
        'txtUsuario
        '
        Me.txtUsuario.Location = New System.Drawing.Point(450, 28)
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(250, 20)
        Me.txtUsuario.TabIndex = 2
        Me.txtUsuario.ReadOnly = True
        Me.txtUsuario.TabStop = False
        '
        'rbContado
        '
        Me.rbContado.AutoSize = True
        Me.rbContado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rbContado.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.rbContado.Checked = True
        Me.rbContado.Location = New System.Drawing.Point(15, 64)
        Me.rbContado.Name = "rbContado"
        Me.rbContado.TabIndex = 3
        Me.rbContado.Text = "Contado"
        Me.rbContado.UseVisualStyleBackColor = True
        '
        'rbCredito
        '
        Me.rbCredito.AutoSize = True
        Me.rbCredito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.rbCredito.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.rbCredito.Location = New System.Drawing.Point(95, 64)
        Me.rbCredito.Name = "rbCredito"
        Me.rbCredito.TabIndex = 4
        Me.rbCredito.Text = "Crédito"
        Me.rbCredito.UseVisualStyleBackColor = True
        '
        'lblCliente
        '
        Me.lblCliente.AutoSize = True
        Me.lblCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCliente.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblCliente.Location = New System.Drawing.Point(190, 66)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Text = "Cliente"
        '
        'cboCliente
        '
        Me.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCliente.FormattingEnabled = True
        Me.cboCliente.Location = New System.Drawing.Point(245, 62)
        Me.cboCliente.Name = "cboCliente"
        Me.cboCliente.Size = New System.Drawing.Size(330, 21)
        Me.cboCliente.TabIndex = 5
        '
        'btnClientes
        '
        Me.btnClientes.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnClientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnClientes.ForeColor = System.Drawing.Color.Black
        Me.btnClientes.Location = New System.Drawing.Point(590, 59)
        Me.btnClientes.Name = "btnClientes"
        Me.btnClientes.Size = New System.Drawing.Size(110, 26)
        Me.btnClientes.TabIndex = 6
        Me.btnClientes.Text = "Clientes..."
        Me.btnClientes.UseVisualStyleBackColor = False
        '
        'gbArticulo
        '
        Me.gbArticulo.Controls.Add(Me.lblProducto)
        Me.gbArticulo.Controls.Add(Me.cboProducto)
        Me.gbArticulo.Controls.Add(Me.lblInfo)
        Me.gbArticulo.Controls.Add(Me.lblCantidad)
        Me.gbArticulo.Controls.Add(Me.nudCantidad)
        Me.gbArticulo.Controls.Add(Me.btnAgregar)
        Me.gbArticulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular)
        Me.gbArticulo.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204)
        Me.gbArticulo.Location = New System.Drawing.Point(20, 165)
        Me.gbArticulo.Name = "gbArticulo"
        Me.gbArticulo.Size = New System.Drawing.Size(720, 95)
        Me.gbArticulo.Text = "Agregar artículo"
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblProducto.Location = New System.Drawing.Point(15, 31)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Text = "Producto"
        '
        'cboProducto
        '
        Me.cboProducto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboProducto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboProducto.FormattingEnabled = True
        Me.cboProducto.Location = New System.Drawing.Point(80, 28)
        Me.cboProducto.Name = "cboProducto"
        Me.cboProducto.Size = New System.Drawing.Size(380, 21)
        Me.cboProducto.TabIndex = 0
        '
        'lblInfo
        '
        Me.lblInfo.AutoSize = False
        Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular)
        Me.lblInfo.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblInfo.Location = New System.Drawing.Point(80, 58)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(380, 22)
        Me.lblInfo.Text = "Precio: -   Stock disponible: -"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCantidad
        '
        Me.lblCantidad.AutoSize = True
        Me.lblCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCantidad.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblCantidad.Location = New System.Drawing.Point(480, 31)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Text = "Cantidad"
        '
        'nudCantidad
        '
        Me.nudCantidad.Location = New System.Drawing.Point(540, 28)
        Me.nudCantidad.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.nudCantidad.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudCantidad.Name = "nudCantidad"
        Me.nudCantidad.Size = New System.Drawing.Size(70, 20)
        Me.nudCantidad.TabIndex = 1
        Me.nudCantidad.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnAgregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnAgregar.ForeColor = System.Drawing.Color.Black
        Me.btnAgregar.Location = New System.Drawing.Point(625, 24)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(80, 28)
        Me.btnAgregar.TabIndex = 2
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'dgvDetalle
        '
        Me.dgvDetalle.AllowUserToAddRows = False
        Me.dgvDetalle.AllowUserToDeleteRows = False
        Me.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDetalle.BackgroundColor = System.Drawing.Color.White
        Me.dgvDetalle.Location = New System.Drawing.Point(20, 270)
        Me.dgvDetalle.MultiSelect = False
        Me.dgvDetalle.Name = "dgvDetalle"
        Me.dgvDetalle.ReadOnly = True
        Me.dgvDetalle.RowHeadersVisible = False
        Me.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDetalle.Size = New System.Drawing.Size(720, 220)
        Me.dgvDetalle.TabIndex = 0
        '
        'btnQuitar
        '
        Me.btnQuitar.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnQuitar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnQuitar.ForeColor = System.Drawing.Color.Black
        Me.btnQuitar.Location = New System.Drawing.Point(20, 498)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(130, 30)
        Me.btnQuitar.TabIndex = 1
        Me.btnQuitar.Text = "Quitar línea"
        Me.btnQuitar.UseVisualStyleBackColor = False
        '
        'lblSubtotalT
        '
        Me.lblSubtotalT.AutoSize = False
        Me.lblSubtotalT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular)
        Me.lblSubtotalT.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblSubtotalT.Location = New System.Drawing.Point(470, 495)
        Me.lblSubtotalT.Name = "lblSubtotalT"
        Me.lblSubtotalT.Size = New System.Drawing.Size(130, 22)
        Me.lblSubtotalT.Text = "Total bruto:"
        Me.lblSubtotalT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSubtotal
        '
        Me.lblSubtotal.AutoSize = False
        Me.lblSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular)
        Me.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblSubtotal.Location = New System.Drawing.Point(605, 495)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(135, 22)
        Me.lblSubtotal.Text = "0.00"
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblItbisT
        '
        Me.lblItbisT.AutoSize = False
        Me.lblItbisT.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular)
        Me.lblItbisT.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblItbisT.Location = New System.Drawing.Point(470, 519)
        Me.lblItbisT.Name = "lblItbisT"
        Me.lblItbisT.Size = New System.Drawing.Size(130, 22)
        Me.lblItbisT.Text = "Total ITBIS:"
        Me.lblItbisT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblItbis
        '
        Me.lblItbis.AutoSize = False
        Me.lblItbis.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular)
        Me.lblItbis.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblItbis.Location = New System.Drawing.Point(605, 519)
        Me.lblItbis.Name = "lblItbis"
        Me.lblItbis.Size = New System.Drawing.Size(135, 22)
        Me.lblItbis.Text = "0.00"
        Me.lblItbis.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalT
        '
        Me.lblTotalT.AutoSize = False
        Me.lblTotalT.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalT.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153)
        Me.lblTotalT.Location = New System.Drawing.Point(450, 545)
        Me.lblTotalT.Name = "lblTotalT"
        Me.lblTotalT.Size = New System.Drawing.Size(150, 26)
        Me.lblTotalT.Text = "TOTAL A PAGAR:"
        Me.lblTotalT.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = False
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153)
        Me.lblTotal.Location = New System.Drawing.Point(605, 545)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(135, 26)
        Me.lblTotal.Text = "0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnGuardar
        '
        Me.btnGuardar.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnGuardar.ForeColor = System.Drawing.Color.Black
        Me.btnGuardar.Location = New System.Drawing.Point(20, 595)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(170, 38)
        Me.btnGuardar.TabIndex = 2
        Me.btnGuardar.Text = "Cobrar y guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'btnImprimir
        '
        Me.btnImprimir.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnImprimir.ForeColor = System.Drawing.Color.Black
        Me.btnImprimir.Location = New System.Drawing.Point(205, 595)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(170, 38)
        Me.btnImprimir.TabIndex = 3
        Me.btnImprimir.Text = "Imprimir (F5)"
        Me.btnImprimir.UseVisualStyleBackColor = False
        '
        'btnNueva
        '
        Me.btnNueva.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnNueva.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnNueva.ForeColor = System.Drawing.Color.Black
        Me.btnNueva.Location = New System.Drawing.Point(390, 595)
        Me.btnNueva.Name = "btnNueva"
        Me.btnNueva.Size = New System.Drawing.Size(170, 38)
        Me.btnNueva.TabIndex = 4
        Me.btnNueva.Text = "Nueva factura"
        Me.btnNueva.UseVisualStyleBackColor = False
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnCerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCerrar.ForeColor = System.Drawing.Color.Black
        Me.btnCerrar.Location = New System.Drawing.Point(575, 595)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(165, 38)
        Me.btnCerrar.TabIndex = 5
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'FrmFactura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 650)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.gbFactura)
        Me.Controls.Add(Me.gbArticulo)
        Me.Controls.Add(Me.dgvDetalle)
        Me.Controls.Add(Me.btnQuitar)
        Me.Controls.Add(Me.lblSubtotalT)
        Me.Controls.Add(Me.lblSubtotal)
        Me.Controls.Add(Me.lblItbisT)
        Me.Controls.Add(Me.lblItbis)
        Me.Controls.Add(Me.lblTotalT)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnNueva)
        Me.Controls.Add(Me.btnCerrar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmFactura"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Facturación"
        CType(Me.nudCantidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbFactura.ResumeLayout(False)
        Me.gbFactura.PerformLayout()
        Me.gbArticulo.ResumeLayout(False)
        Me.gbArticulo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents gbFactura As System.Windows.Forms.GroupBox
    Friend WithEvents lblNumero As System.Windows.Forms.Label
    Friend WithEvents txtNumero As System.Windows.Forms.TextBox
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents txtFecha As System.Windows.Forms.TextBox
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents rbContado As System.Windows.Forms.RadioButton
    Friend WithEvents rbCredito As System.Windows.Forms.RadioButton
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents cboCliente As System.Windows.Forms.ComboBox
    Friend WithEvents btnClientes As System.Windows.Forms.Button
    Friend WithEvents gbArticulo As System.Windows.Forms.GroupBox
    Friend WithEvents lblProducto As System.Windows.Forms.Label
    Friend WithEvents cboProducto As System.Windows.Forms.ComboBox
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents lblCantidad As System.Windows.Forms.Label
    Friend WithEvents nudCantidad As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents dgvDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents btnQuitar As System.Windows.Forms.Button
    Friend WithEvents lblSubtotalT As System.Windows.Forms.Label
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents lblItbisT As System.Windows.Forms.Label
    Friend WithEvents lblItbis As System.Windows.Forms.Label
    Friend WithEvents lblTotalT As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnNueva As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCobro
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
        Me.lblCondicion = New System.Windows.Forms.Label()
        Me.lblClienteInfo = New System.Windows.Forms.Label()
        Me.lblAPagarT = New System.Windows.Forms.Label()
        Me.lblAPagar = New System.Windows.Forms.Label()
        Me.lblPagadoT = New System.Windows.Forms.Label()
        Me.txtPagado = New System.Windows.Forms.TextBox()
        Me.lblDevueltaT = New System.Windows.Forms.Label()
        Me.lblDevuelta = New System.Windows.Forms.Label()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153)
        Me.lblTitulo.Location = New System.Drawing.Point(140, 12)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Text = "COBRO"
        '
        'lblCondicion
        '
        Me.lblCondicion.AutoSize = False
        Me.lblCondicion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblCondicion.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204)
        Me.lblCondicion.Location = New System.Drawing.Point(15, 58)
        Me.lblCondicion.Name = "lblCondicion"
        Me.lblCondicion.Size = New System.Drawing.Size(370, 22)
        Me.lblCondicion.Text = "VENTA AL CONTADO"
        Me.lblCondicion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblClienteInfo
        '
        Me.lblClienteInfo.AutoSize = False
        Me.lblClienteInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular)
        Me.lblClienteInfo.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblClienteInfo.Location = New System.Drawing.Point(15, 82)
        Me.lblClienteInfo.Name = "lblClienteInfo"
        Me.lblClienteInfo.Size = New System.Drawing.Size(370, 38)
        Me.lblClienteInfo.Text = "Cliente:"
        Me.lblClienteInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAPagarT
        '
        Me.lblAPagarT.AutoSize = True
        Me.lblAPagarT.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblAPagarT.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblAPagarT.Location = New System.Drawing.Point(30, 138)
        Me.lblAPagarT.Name = "lblAPagarT"
        Me.lblAPagarT.Text = "Total a pagar:"
        '
        'lblAPagar
        '
        Me.lblAPagar.AutoSize = False
        Me.lblAPagar.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblAPagar.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153)
        Me.lblAPagar.Location = New System.Drawing.Point(180, 132)
        Me.lblAPagar.Name = "lblAPagar"
        Me.lblAPagar.Size = New System.Drawing.Size(190, 30)
        Me.lblAPagar.Text = "0.00"
        Me.lblAPagar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPagadoT
        '
        Me.lblPagadoT.AutoSize = True
        Me.lblPagadoT.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblPagadoT.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblPagadoT.Location = New System.Drawing.Point(30, 185)
        Me.lblPagadoT.Name = "lblPagadoT"
        Me.lblPagadoT.Text = "Monto pagado:"
        '
        'txtPagado
        '
        Me.txtPagado.Location = New System.Drawing.Point(180, 181)
        Me.txtPagado.Name = "txtPagado"
        Me.txtPagado.Size = New System.Drawing.Size(190, 26)
        Me.txtPagado.TabIndex = 0
        Me.txtPagado.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular)
        '
        'lblDevueltaT
        '
        Me.lblDevueltaT.AutoSize = True
        Me.lblDevueltaT.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDevueltaT.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblDevueltaT.Location = New System.Drawing.Point(30, 232)
        Me.lblDevueltaT.Name = "lblDevueltaT"
        Me.lblDevueltaT.Text = "Devuelta:"
        '
        'lblDevuelta
        '
        Me.lblDevuelta.AutoSize = False
        Me.lblDevuelta.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblDevuelta.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153)
        Me.lblDevuelta.Location = New System.Drawing.Point(180, 226)
        Me.lblDevuelta.Name = "lblDevuelta"
        Me.lblDevuelta.Size = New System.Drawing.Size(190, 30)
        Me.lblDevuelta.Text = "0.00"
        Me.lblDevuelta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnAceptar.ForeColor = System.Drawing.Color.Black
        Me.btnAceptar.Location = New System.Drawing.Point(60, 280)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(130, 38)
        Me.btnAceptar.TabIndex = 1
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCancelar.ForeColor = System.Drawing.Color.Black
        Me.btnCancelar.Location = New System.Drawing.Point(210, 280)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(130, 38)
        Me.btnCancelar.TabIndex = 2
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'FrmCobro
        '
        Me.AcceptButton = Me.btnAceptar
        Me.CancelButton = Me.btnCancelar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 335)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.lblCondicion)
        Me.Controls.Add(Me.lblClienteInfo)
        Me.Controls.Add(Me.lblAPagarT)
        Me.Controls.Add(Me.lblAPagar)
        Me.Controls.Add(Me.lblPagadoT)
        Me.Controls.Add(Me.txtPagado)
        Me.Controls.Add(Me.lblDevueltaT)
        Me.Controls.Add(Me.lblDevuelta)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.btnCancelar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCobro"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cobro"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblCondicion As System.Windows.Forms.Label
    Friend WithEvents lblClienteInfo As System.Windows.Forms.Label
    Friend WithEvents lblAPagarT As System.Windows.Forms.Label
    Friend WithEvents lblAPagar As System.Windows.Forms.Label
    Friend WithEvents lblPagadoT As System.Windows.Forms.Label
    Friend WithEvents txtPagado As System.Windows.Forms.TextBox
    Friend WithEvents lblDevueltaT As System.Windows.Forms.Label
    Friend WithEvents lblDevuelta As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
End Class

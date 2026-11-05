<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmCajeros
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
        Me.gbNuevo = New System.Windows.Forms.GroupBox()
        Me.lblUsuarioN = New System.Windows.Forms.Label()
        Me.txtUsuarioN = New System.Windows.Forms.TextBox()
        Me.lblClave = New System.Windows.Forms.Label()
        Me.txtClave = New System.Windows.Forms.TextBox()
        Me.lblConfirmar = New System.Windows.Forms.Label()
        Me.txtConfirmar = New System.Windows.Forms.TextBox()
        Me.lblTurno = New System.Windows.Forms.Label()
        Me.cboTurno = New System.Windows.Forms.ComboBox()
        Me.lblHorarios = New System.Windows.Forms.Label()
        Me.btnCrear = New System.Windows.Forms.Button()
        Me.dgvCajeros = New System.Windows.Forms.DataGridView()
        Me.btnActivar = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        CType(Me.dgvCajeros, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbNuevo.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(0, 51, 153)
        Me.lblTitulo.Location = New System.Drawing.Point(235, 15)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Text = "CAJEROS"
        '
        'gbNuevo
        '
        Me.gbNuevo.Controls.Add(Me.lblUsuarioN)
        Me.gbNuevo.Controls.Add(Me.txtUsuarioN)
        Me.gbNuevo.Controls.Add(Me.lblClave)
        Me.gbNuevo.Controls.Add(Me.txtClave)
        Me.gbNuevo.Controls.Add(Me.lblConfirmar)
        Me.gbNuevo.Controls.Add(Me.txtConfirmar)
        Me.gbNuevo.Controls.Add(Me.lblTurno)
        Me.gbNuevo.Controls.Add(Me.cboTurno)
        Me.gbNuevo.Controls.Add(Me.lblHorarios)
        Me.gbNuevo.Controls.Add(Me.btnCrear)
        Me.gbNuevo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular)
        Me.gbNuevo.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204)
        Me.gbNuevo.Location = New System.Drawing.Point(20, 60)
        Me.gbNuevo.Name = "gbNuevo"
        Me.gbNuevo.Size = New System.Drawing.Size(560, 155)
        Me.gbNuevo.Text = "Nuevo cajero"
        '
        'lblUsuarioN
        '
        Me.lblUsuarioN.AutoSize = True
        Me.lblUsuarioN.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblUsuarioN.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblUsuarioN.Location = New System.Drawing.Point(15, 35)
        Me.lblUsuarioN.Name = "lblUsuarioN"
        Me.lblUsuarioN.Text = "Usuario"
        '
        'txtUsuarioN
        '
        Me.txtUsuarioN.Location = New System.Drawing.Point(105, 32)
        Me.txtUsuarioN.Name = "txtUsuarioN"
        Me.txtUsuarioN.Size = New System.Drawing.Size(165, 20)
        Me.txtUsuarioN.TabIndex = 0
        Me.txtUsuarioN.MaxLength = 50
        '
        'lblClave
        '
        Me.lblClave.AutoSize = True
        Me.lblClave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblClave.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblClave.Location = New System.Drawing.Point(15, 70)
        Me.lblClave.Name = "lblClave"
        Me.lblClave.Text = "Contraseña"
        '
        'txtClave
        '
        Me.txtClave.Location = New System.Drawing.Point(105, 67)
        Me.txtClave.Name = "txtClave"
        Me.txtClave.Size = New System.Drawing.Size(165, 20)
        Me.txtClave.TabIndex = 1
        Me.txtClave.MaxLength = 50
        Me.txtClave.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        '
        'lblConfirmar
        '
        Me.lblConfirmar.AutoSize = True
        Me.lblConfirmar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblConfirmar.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblConfirmar.Location = New System.Drawing.Point(15, 105)
        Me.lblConfirmar.Name = "lblConfirmar"
        Me.lblConfirmar.Text = "Confirmar"
        '
        'txtConfirmar
        '
        Me.txtConfirmar.Location = New System.Drawing.Point(105, 102)
        Me.txtConfirmar.Name = "txtConfirmar"
        Me.txtConfirmar.Size = New System.Drawing.Size(165, 20)
        Me.txtConfirmar.TabIndex = 2
        Me.txtConfirmar.MaxLength = 50
        Me.txtConfirmar.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        '
        'lblTurno
        '
        Me.lblTurno.AutoSize = True
        Me.lblTurno.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTurno.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblTurno.Location = New System.Drawing.Point(300, 35)
        Me.lblTurno.Name = "lblTurno"
        Me.lblTurno.Text = "Turno"
        '
        'cboTurno
        '
        Me.cboTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTurno.FormattingEnabled = True
        Me.cboTurno.Location = New System.Drawing.Point(350, 32)
        Me.cboTurno.Name = "cboTurno"
        Me.cboTurno.Size = New System.Drawing.Size(170, 21)
        Me.cboTurno.TabIndex = 3
        '
        'lblHorarios
        '
        Me.lblHorarios.AutoSize = False
        Me.lblHorarios.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular)
        Me.lblHorarios.ForeColor = System.Drawing.Color.FromArgb(89, 89, 140)
        Me.lblHorarios.Location = New System.Drawing.Point(300, 62)
        Me.lblHorarios.Name = "lblHorarios"
        Me.lblHorarios.Size = New System.Drawing.Size(250, 36)
        Me.lblHorarios.Text = "Mañana 06:00-14:00 | Tarde 14:00-22:00 | Noche 22:00-06:00"
        Me.lblHorarios.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'btnCrear
        '
        Me.btnCrear.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnCrear.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCrear.ForeColor = System.Drawing.Color.Black
        Me.btnCrear.Location = New System.Drawing.Point(300, 108)
        Me.btnCrear.Name = "btnCrear"
        Me.btnCrear.Size = New System.Drawing.Size(220, 34)
        Me.btnCrear.TabIndex = 4
        Me.btnCrear.Text = "Crear cajero"
        Me.btnCrear.UseVisualStyleBackColor = False
        '
        'dgvCajeros
        '
        Me.dgvCajeros.AllowUserToAddRows = False
        Me.dgvCajeros.AllowUserToDeleteRows = False
        Me.dgvCajeros.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvCajeros.BackgroundColor = System.Drawing.Color.White
        Me.dgvCajeros.Location = New System.Drawing.Point(20, 230)
        Me.dgvCajeros.MultiSelect = False
        Me.dgvCajeros.Name = "dgvCajeros"
        Me.dgvCajeros.ReadOnly = True
        Me.dgvCajeros.RowHeadersVisible = False
        Me.dgvCajeros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCajeros.Size = New System.Drawing.Size(560, 250)
        Me.dgvCajeros.TabIndex = 0
        '
        'btnActivar
        '
        Me.btnActivar.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnActivar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnActivar.ForeColor = System.Drawing.Color.Black
        Me.btnActivar.Location = New System.Drawing.Point(20, 490)
        Me.btnActivar.Name = "btnActivar"
        Me.btnActivar.Size = New System.Drawing.Size(200, 34)
        Me.btnActivar.TabIndex = 1
        Me.btnActivar.Text = "Activar / Desactivar"
        Me.btnActivar.UseVisualStyleBackColor = False
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.Color.FromArgb(143, 170, 204)
        Me.btnCerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.btnCerrar.ForeColor = System.Drawing.Color.Black
        Me.btnCerrar.Location = New System.Drawing.Point(450, 490)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(130, 34)
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'FrmCajeros
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 545)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.gbNuevo)
        Me.Controls.Add(Me.dgvCajeros)
        Me.Controls.Add(Me.btnActivar)
        Me.Controls.Add(Me.btnCerrar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmCajeros"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Cajeros"
        CType(Me.dgvCajeros, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbNuevo.ResumeLayout(False)
        Me.gbNuevo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents gbNuevo As System.Windows.Forms.GroupBox
    Friend WithEvents lblUsuarioN As System.Windows.Forms.Label
    Friend WithEvents txtUsuarioN As System.Windows.Forms.TextBox
    Friend WithEvents lblClave As System.Windows.Forms.Label
    Friend WithEvents txtClave As System.Windows.Forms.TextBox
    Friend WithEvents lblConfirmar As System.Windows.Forms.Label
    Friend WithEvents txtConfirmar As System.Windows.Forms.TextBox
    Friend WithEvents lblTurno As System.Windows.Forms.Label
    Friend WithEvents cboTurno As System.Windows.Forms.ComboBox
    Friend WithEvents lblHorarios As System.Windows.Forms.Label
    Friend WithEvents btnCrear As System.Windows.Forms.Button
    Friend WithEvents dgvCajeros As System.Windows.Forms.DataGridView
    Friend WithEvents btnActivar As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
End Class

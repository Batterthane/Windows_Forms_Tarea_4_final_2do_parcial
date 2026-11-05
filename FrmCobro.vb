Imports System.Globalization

Public Class FrmCobro

    ' Se asignan antes de ShowDialog
    Public Property TotalAPagar As Decimal
    Public Property EsCredito As Boolean
    Public Property Cliente As String = ""
    ' Resultado
    Public Property MontoPagado As Decimal

    Private ReadOnly Cult As CultureInfo = CultureInfo.InvariantCulture

    Private Sub FrmCobro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblAPagar.Text = TotalAPagar.ToString("N2", Cult)
        If EsCredito Then
            lblCondicion.Text = "VENTA A CRÉDITO"
            lblClienteInfo.Text = "Se registrará en cuentas por cobrar de:" & vbCrLf & Cliente
            lblPagadoT.Visible = False
            txtPagado.Visible = False
            lblDevueltaT.Visible = False
            lblDevuelta.Visible = False
            btnAceptar.Enabled = True
        Else
            lblCondicion.Text = "VENTA AL CONTADO"
            lblClienteInfo.Text = "Cliente: " & Cliente
            btnAceptar.Enabled = False
        End If
    End Sub

    Private Function LeerPagado(ByRef valor As Decimal) As Boolean
        Return Decimal.TryParse(txtPagado.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, valor)
    End Function

    Private Sub txtPagado_TextChanged(sender As Object, e As EventArgs) Handles txtPagado.TextChanged
        Dim pagado As Decimal = 0D
        If Not LeerPagado(pagado) Then
            lblDevuelta.Text = "0.00"
            btnAceptar.Enabled = False
        ElseIf pagado >= TotalAPagar Then
            lblDevuelta.Text = (pagado - TotalAPagar).ToString("N2", Cult)
            btnAceptar.Enabled = True
        Else
            lblDevuelta.Text = "Falta " & (TotalAPagar - pagado).ToString("N2", Cult)
            btnAceptar.Enabled = False
        End If
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If EsCredito Then
            MontoPagado = 0D
        Else
            Dim pagado As Decimal = 0D
            If Not LeerPagado(pagado) OrElse pagado < TotalAPagar Then Return
            MontoPagado = pagado
        End If
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub
End Class

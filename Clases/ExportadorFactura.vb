Imports System.Globalization
Imports System.IO
Imports System.Text

' Genera la factura como .txt o .pdf (letra de ancho fijo, 64 columnas).
Public Class ExportadorFactura

    Private Const Ancho As Integer = 64
    Private Shared ReadOnly Cult As CultureInfo = CultureInfo.InvariantCulture

    ' Guarda según la extensión de la ruta (.pdf o .txt)
    Public Shared Sub Guardar(ruta As String, cab As DataRow, det As DataTable)
        Dim lineas As List(Of String) = Construir(cab, det)
        If Path.GetExtension(ruta).ToLowerInvariant() = ".pdf" Then
            PdfSimple.Guardar(ruta, lineas, 10, 12, 58)
        Else
            File.WriteAllLines(ruta, lineas.ToArray(), New UTF8Encoding(True))
        End If
    End Sub

    Public Shared Function Construir(cab As DataRow, det As DataTable) As List(Of String)
        Dim L As New List(Of String)()
        Dim fecha As DateTime = Convert.ToDateTime(cab("Fecha"))
        Dim esCredito As Boolean = (cab("Tipo").ToString() = "Credito")

        Dim cajero As String = cab("Cajero").ToString()
        If Not IsDBNull(cab("Turno")) AndAlso cab("Turno").ToString() <> "" Then
            cajero &= " (" & Turnos.Mostrar(cab("Turno").ToString()) & ")"
        End If

        L.Add(Centrar("ALMACÉN"))
        L.Add(Centrar("FACTURA DE VENTA"))
        L.Add(New String("="c, Ancho))
        L.Add("Factura No.: " & cab("Numero").ToString())
        L.Add("Fecha      : " & fecha.ToString("dd/MM/yyyy", Cult))
        L.Add("Hora       : " & fecha.ToString("HH:mm", Cult))
        L.Add("Condición  : " & If(esCredito, "CRÉDITO", "CONTADO"))
        L.Add("Cajero     : " & cajero)
        L.Add("Cliente    : " & cab("Cliente").ToString())
        L.Add(New String("-"c, Ancho))
        L.Add("Cod".PadLeft(4) & " " & "Descripcion".PadRight(20) & " " & "Cant".PadLeft(4) & " " &
              "Valor".PadLeft(10) & " " & "ITBIS".PadLeft(9) & " " & "Importe".PadLeft(12))
        L.Add(New String("-"c, Ancho))

        For Each r As DataRow In det.Rows
            Dim nombre As String = r("Nombre").ToString()
            Dim primera As String = nombre.Substring(0, Math.Min(20, nombre.Length))
            L.Add(r("Codigo").ToString().PadLeft(4) & " " &
                  primera.PadRight(20) & " " &
                  r("Cantidad").ToString().PadLeft(4) & " " &
                  Convert.ToDecimal(r("Precio")).ToString("N2", Cult).PadLeft(10) & " " &
                  Convert.ToDecimal(r("Itbis")).ToString("N2", Cult).PadLeft(9) & " " &
                  Convert.ToDecimal(r("Importe")).ToString("N2", Cult).PadLeft(12))
            ' Si el nombre es largo, continúa en las líneas siguientes
            Dim pos As Integer = 20
            Do While pos < nombre.Length
                L.Add(New String(" "c, 5) & nombre.Substring(pos, Math.Min(20, nombre.Length - pos)))
                pos += 20
            Loop
        Next

        L.Add(New String("-"c, Ancho))
        L.Add(Total("Total bruto:", Convert.ToDecimal(cab("Subtotal"))))
        L.Add(Total("Total ITBIS (" & (MetodosFacturas.TasaItbis * 100D).ToString("0", Cult) & "%):", Convert.ToDecimal(cab("Itbis"))))
        L.Add(Total("TOTAL A PAGAR:", Convert.ToDecimal(cab("Total"))))
        L.Add(New String("-"c, Ancho))
        If esCredito Then
            L.Add(Centrar("VENTA A CRÉDITO - Pendiente de cobro"))
        Else
            L.Add(Total("Monto pagado:", Convert.ToDecimal(cab("MontoPagado"))))
            L.Add(Total("Devuelta:", Convert.ToDecimal(cab("Devuelta"))))
        End If
        L.Add(New String("="c, Ancho))
        L.Add(Centrar("Gracias por su compra"))
        Return L
    End Function

    Private Shared Function Centrar(texto As String) As String
        Return texto.PadLeft((Ancho + texto.Length) \ 2).PadRight(Ancho)
    End Function

    Private Shared Function Total(etiqueta As String, valor As Decimal) As String
        Return etiqueta.PadLeft(44) & valor.ToString("N2", Cult).PadLeft(20)
    End Function
End Class

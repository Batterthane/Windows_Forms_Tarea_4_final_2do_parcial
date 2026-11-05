Imports System.Globalization
Imports System.IO
Imports System.Text

' Exporta cualquier tabla de reporte a .txt o .pdf (letra de ancho fijo).
Public Class ExportadorReporte

    Private Const AnchoMaximo As Integer = 100
    Private Shared ReadOnly Cult As CultureInfo = CultureInfo.InvariantCulture

    Public Shared Sub Guardar(ruta As String, titulo As String, filtros As String, tabla As DataTable)
        Dim lineas As List(Of String) = Construir(titulo, filtros, tabla)
        If Path.GetExtension(ruta).ToLowerInvariant() = ".pdf" Then
            PdfSimple.Guardar(ruta, lineas, 8, 10, 70)
        Else
            File.WriteAllLines(ruta, lineas.ToArray(), New UTF8Encoding(True))
        End If
    End Sub

    Private Shared Function EsNumerica(c As DataColumn) As Boolean
        Dim t As Type = c.DataType
        Return t Is GetType(Decimal) OrElse t Is GetType(Integer) OrElse t Is GetType(Long) OrElse
               t Is GetType(Short) OrElse t Is GetType(Double)
    End Function

    Private Shared Function Texto(valor As Object, c As DataColumn) As String
        If IsDBNull(valor) Then Return ""
        If c.DataType Is GetType(Decimal) Then Return Convert.ToDecimal(valor).ToString("N2", Cult)
        If c.DataType Is GetType(DateTime) Then Return Convert.ToDateTime(valor).ToString("dd/MM/yyyy HH:mm", Cult)
        Return valor.ToString()
    End Function

    Public Shared Function Construir(titulo As String, filtros As String, t As DataTable) As List(Of String)
        Dim nc As Integer = t.Columns.Count
        Dim anchos(nc - 1) As Integer
        For j As Integer = 0 To nc - 1
            anchos(j) = Math.Max(t.Columns(j).ColumnName.Length, 4)
            For Each r As DataRow In t.Rows
                anchos(j) = Math.Max(anchos(j), Texto(r(j), t.Columns(j)).Length)
            Next
            anchos(j) = Math.Min(anchos(j), 30)
        Next

        ' Si no cabe en la hoja, se reduce la columna más ancha
        Dim total As Integer = anchos.Sum() + 2 * (nc - 1)
        Do While total > AnchoMaximo
            Dim mayor As Integer = 0
            For j As Integer = 1 To nc - 1
                If anchos(j) > anchos(mayor) Then mayor = j
            Next
            If anchos(mayor) <= 8 Then Exit Do
            anchos(mayor) -= 1
            total -= 1
        Loop

        Dim L As New List(Of String)()
        Dim ancho As Integer = Math.Max(total, 40)
        L.Add(Centrar("ALMACÉN", ancho))
        L.Add(Centrar(titulo, ancho))
        L.Add(New String("="c, ancho))
        L.Add("Generado: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm", Cult) & "   Por: " & Variable.Usuario)
        If filtros <> "" Then L.Add(filtros)
        L.Add(New String("-"c, ancho))

        Dim cab As New List(Of String)()
        For j As Integer = 0 To nc - 1
            cab.Add(Ajustar(t.Columns(j).ColumnName, anchos(j), EsNumerica(t.Columns(j))))
        Next
        L.Add(String.Join("  ", cab.ToArray()))
        L.Add(New String("-"c, ancho))

        For Each r As DataRow In t.Rows
            Dim celdas As New List(Of String)()
            For j As Integer = 0 To nc - 1
                celdas.Add(Ajustar(Texto(r(j), t.Columns(j)), anchos(j), EsNumerica(t.Columns(j))))
            Next
            L.Add(String.Join("  ", celdas.ToArray()))
        Next

        L.Add(New String("-"c, ancho))
        L.Add("Registros: " & t.Rows.Count.ToString(Cult))
        Return L
    End Function

    ' Recorta o rellena; los números van alineados a la derecha
    Private Shared Function Ajustar(texto As String, ancho As Integer, derecha As Boolean) As String
        If texto.Length > ancho Then texto = texto.Substring(0, ancho)
        If derecha Then Return texto.PadLeft(ancho)
        Return texto.PadRight(ancho)
    End Function

    Private Shared Function Centrar(texto As String, ancho As Integer) As String
        If texto.Length >= ancho Then Return texto
        Return texto.PadLeft((ancho + texto.Length) \ 2).PadRight(ancho)
    End Function
End Class

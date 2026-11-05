Imports System.Globalization
Imports System.IO
Imports System.Text

' Escribe un PDF sencillo (PDF 1.4, fuente Courier, páginas A4) sin librerías externas.
Public Class PdfSimple

    Private Shared ReadOnly Cult As CultureInfo = CultureInfo.InvariantCulture

    ' tamFuente e interlineado en puntos; lineasPorPagina según el tamaño elegido
    Public Shared Sub Guardar(ruta As String, lineas As List(Of String), tamFuente As Integer,
                              interlineado As Integer, lineasPorPagina As Integer)
        Dim enc As Encoding = Encoding.GetEncoding(1252)

        Dim paginas As New List(Of List(Of String))()
        Dim i As Integer = 0
        Do While i < lineas.Count
            paginas.Add(lineas.GetRange(i, Math.Min(lineasPorPagina, lineas.Count - i)))
            i += lineasPorPagina
        Loop
        If paginas.Count = 0 Then paginas.Add(New List(Of String)())
        Dim n As Integer = paginas.Count

        Dim offsets As New List(Of Long)()   ' posición de los objetos 1, 2, 3, ...

        Using ms As New MemoryStream()
            Escribir(ms, enc, "%PDF-1.4" & vbLf)

            ' 1: catálogo
            offsets.Add(ms.Position)
            Escribir(ms, enc, "1 0 obj" & vbLf & "<< /Type /Catalog /Pages 2 0 R >>" & vbLf & "endobj" & vbLf)

            ' 2: lista de páginas
            Dim kids As New StringBuilder()
            For p As Integer = 0 To n - 1
                kids.Append((4 + 2 * p).ToString(Cult)).Append(" 0 R ")
            Next
            offsets.Add(ms.Position)
            Escribir(ms, enc, "2 0 obj" & vbLf & "<< /Type /Pages /Kids [ " & kids.ToString() & "] /Count " & n.ToString(Cult) & " >>" & vbLf & "endobj" & vbLf)

            ' 3: fuente
            offsets.Add(ms.Position)
            Escribir(ms, enc, "3 0 obj" & vbLf & "<< /Type /Font /Subtype /Type1 /BaseFont /Courier /Encoding /WinAnsiEncoding >>" & vbLf & "endobj" & vbLf)

            ' páginas: objeto 4+2p = página, 5+2p = contenido
            For p As Integer = 0 To n - 1
                Dim objPagina As Integer = 4 + 2 * p
                Dim objContenido As Integer = objPagina + 1

                Dim sb As New StringBuilder()
                sb.Append("BT" & vbLf & "/F1 " & tamFuente.ToString(Cult) & " Tf" & vbLf &
                          interlineado.ToString(Cult) & " TL" & vbLf & "50 790 Td" & vbLf)
                For Each linea As String In paginas(p)
                    sb.Append("(").Append(Escapar(linea)).Append(") Tj" & vbLf & "T*" & vbLf)
                Next
                sb.Append("ET")
                Dim cont As Byte() = enc.GetBytes(sb.ToString())

                offsets.Add(ms.Position)
                Escribir(ms, enc, objPagina.ToString(Cult) & " 0 obj" & vbLf &
                    "<< /Type /Page /Parent 2 0 R /MediaBox [0 0 595 842] /Resources << /Font << /F1 3 0 R >> >> /Contents " &
                    objContenido.ToString(Cult) & " 0 R >>" & vbLf & "endobj" & vbLf)

                offsets.Add(ms.Position)
                Escribir(ms, enc, objContenido.ToString(Cult) & " 0 obj" & vbLf & "<< /Length " & cont.Length.ToString(Cult) & " >>" & vbLf & "stream" & vbLf)
                ms.Write(cont, 0, cont.Length)
                Escribir(ms, enc, vbLf & "endstream" & vbLf & "endobj" & vbLf)
            Next

            ' tabla de referencias cruzadas (cada entrada mide exactamente 20 bytes)
            Dim posXref As Long = ms.Position
            Escribir(ms, enc, "xref" & vbLf & "0 " & (offsets.Count + 1).ToString(Cult) & vbLf)
            Escribir(ms, enc, "0000000000 65535 f " & vbLf)
            For Each o As Long In offsets
                Escribir(ms, enc, o.ToString("0000000000", Cult) & " 00000 n " & vbLf)
            Next
            Escribir(ms, enc, "trailer" & vbLf & "<< /Size " & (offsets.Count + 1).ToString(Cult) & " /Root 1 0 R >>" & vbLf &
                              "startxref" & vbLf & posXref.ToString(Cult) & vbLf & "%%EOF" & vbLf)

            File.WriteAllBytes(ruta, ms.ToArray())
        End Using
    End Sub

    Private Shared Sub Escribir(ms As MemoryStream, enc As Encoding, texto As String)
        Dim b As Byte() = enc.GetBytes(texto)
        ms.Write(b, 0, b.Length)
    End Sub

    Private Shared Function Escapar(texto As String) As String
        Return texto.Replace("\", "\\").Replace("(", "\(").Replace(")", "\)")
    End Function
End Class

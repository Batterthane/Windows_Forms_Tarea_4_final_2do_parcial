' Turnos de trabajo. En la base de datos se guardan como Manana / Tarde / Noche (sin ñ).
Public Class Turnos

    Public Shared Function Actual() As String
        Dim h As Integer = DateTime.Now.Hour
        If h >= 6 AndAlso h < 14 Then Return "Manana"
        If h >= 14 AndAlso h < 22 Then Return "Tarde"
        Return "Noche"
    End Function

    Public Shared Function Mostrar(valor As String) As String
        If valor Is Nothing Then Return ""
        If valor = "Manana" Then Return "Mañana"
        Return valor
    End Function

    Public Shared Function Horario(valor As String) As String
        Select Case valor
            Case "Manana" : Return "06:00 a 14:00"
            Case "Tarde" : Return "14:00 a 22:00"
            Case "Noche" : Return "22:00 a 06:00"
            Case Else : Return ""
        End Select
    End Function
End Class

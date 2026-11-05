Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text

Public Class Seguridad
    ' SHA256 en hexadecimal mayúscula (64 caracteres)
    Public Shared Function Hash(texto As String) As String
        Using sha As SHA256 = SHA256.Create()
            Dim bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto))
            Dim sb As New StringBuilder()
            For Each b In bytes
                sb.Append(b.ToString("X2"))
            Next
            Return sb.ToString()
        End Using
    End Function

    ' True si usuario y clave son correctos (y el usuario está activo).
    ' Devuelve el rol y el turno (vacío si no es cajero).
    Public Shared Function Validar(usuario As String, clave As String, ByRef estado As String, ByRef turno As String) As Boolean
        estado = ""
        turno = ""
        Using cn = Conexion.Obtener(), cmd As New SqlCommand("dbo._iniciosesion", cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@usuario", usuario)
            cmd.Parameters.AddWithValue("@clave", Hash(clave))
            cn.Open()
            Using rd As SqlDataReader = cmd.ExecuteReader()
                If rd.Read() Then
                    estado = rd("Estado").ToString()
                    If Not IsDBNull(rd("Turno")) Then turno = rd("Turno").ToString()
                    Return True
                End If
            End Using
        End Using
        Return False
    End Function
End Class

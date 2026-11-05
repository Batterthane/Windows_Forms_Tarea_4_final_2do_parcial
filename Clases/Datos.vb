Imports System.Data
Imports System.Data.SqlClient

' Utilidades comunes para llamar procedimientos almacenados
Public Class Datos

    Public Shared Function Param(nombre As String, valor As Object) As SqlParameter
        Dim v As Object = valor
        If v Is Nothing Then v = DBNull.Value
        Return New SqlParameter(nombre, v)
    End Function

    ' Dinero: DECIMAL(12,2); valor Nothing = NULL
    Public Shared Function ParamDinero(nombre As String, valor As Object) As SqlParameter
        Dim p As New SqlParameter(nombre, SqlDbType.Decimal)
        p.Precision = 12
        p.Scale = 2
        If valor Is Nothing Then p.Value = DBNull.Value Else p.Value = valor
        Return p
    End Function

    Public Shared Function Tabla(sp As String, ParamArray p() As SqlParameter) As DataTable
        Using cn = Conexion.Obtener(), cmd As New SqlCommand(sp, cn)
            cmd.CommandType = CommandType.StoredProcedure
            If p IsNot Nothing Then cmd.Parameters.AddRange(p)
            Dim dt As New DataTable()
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
            Return dt
        End Using
    End Function

    Public Shared Function Escalar(sp As String, ParamArray p() As SqlParameter) As Integer
        Using cn = Conexion.Obtener(), cmd As New SqlCommand(sp, cn)
            cmd.CommandType = CommandType.StoredProcedure
            If p IsNot Nothing Then cmd.Parameters.AddRange(p)
            cn.Open()
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function
End Class

Imports System.Data
Imports System.Data.SqlClient

Public Class MetodosFacturas

    ' Debe ser igual a la tasa usada en dbo.SP_CREA_FACTURA
    Public Const TasaItbis As Decimal = 0.18D

    ' tipo: "Contado" o "Credito"
    ' clienteId: Nothing = consumidor final (en crédito es obligatorio)
    ' montoPagado: Nothing en crédito
    ' detalle: tabla con SOLO dos columnas, en este orden: Codigo (Int32) y Cantidad (Int32)
    Public Shared Function Crear(tipo As String, clienteId As Object, usuario As String,
                                 montoPagado As Object, detalle As DataTable) As Integer
        Dim pd As New SqlParameter("@detalle", SqlDbType.Structured)
        pd.TypeName = "dbo.TipoDetalleFactura"
        pd.Value = detalle
        Return Datos.Escalar("dbo.SP_CREA_FACTURA",
                             Datos.Param("@tipo", tipo),
                             Datos.Param("@clienteId", clienteId),
                             Datos.Param("@usuario", usuario),
                             Datos.ParamDinero("@montoPagado", montoPagado),
                             pd)
    End Function

    ' Devuelve un DataSet: Tables(0) = cabecera (1 fila), Tables(1) = detalle
    Public Shared Function Obtener(id As Integer) As DataSet
        Using cn = Conexion.Obtener(), cmd As New SqlCommand("dbo.SP_OBTENER_FACTURA", cn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@id", id)
            Dim ds As New DataSet()
            Using da As New SqlDataAdapter(cmd)
                da.Fill(ds)
            End Using
            Return ds
        End Using
    End Function

    ' ITBIS de una línea (igual que el procedimiento almacenado)
    Public Shared Function ItbisLinea(importe As Decimal) As Decimal
        Return Math.Round(importe * TasaItbis, 2, MidpointRounding.AwayFromZero)
    End Function
End Class

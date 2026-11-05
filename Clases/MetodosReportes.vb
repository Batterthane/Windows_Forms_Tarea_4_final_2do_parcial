Imports System.Data

Public Class MetodosReportes

    ' cajero = Nothing -> todos
    Public Shared Function Cuadre(fecha As Date, cajero As String) As DataTable
        Return Datos.Tabla("dbo.SP_REP_CUADRE", Datos.Param("@fecha", fecha.Date), Datos.Param("@cajero", cajero))
    End Function

    Public Shared Function PorCajero(desde As Date, hasta As Date, cajero As String) As DataTable
        Return Datos.Tabla("dbo.SP_REP_POR_CAJERO", Datos.Param("@desde", desde.Date), Datos.Param("@hasta", hasta.Date), Datos.Param("@cajero", cajero))
    End Function

    Public Shared Function Inventario() As DataTable
        Return Datos.Tabla("dbo.SP_REP_INVENTARIO")
    End Function

    ' tipo = "Contado", "Credito" o Nothing (todas)
    Public Shared Function Facturas(desde As Date, hasta As Date, tipo As String) As DataTable
        Return Datos.Tabla("dbo.SP_REP_FACTURAS", Datos.Param("@desde", desde.Date), Datos.Param("@hasta", hasta.Date), Datos.Param("@tipo", tipo))
    End Function

    Public Shared Function TopClientes(desde As Date, hasta As Date) As DataTable
        Return Datos.Tabla("dbo.SP_REP_TOP_CLIENTES", Datos.Param("@desde", desde.Date), Datos.Param("@hasta", hasta.Date))
    End Function
End Class

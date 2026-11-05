Imports System.Data

Public Class MetodosClientes
    Public Shared Function Listar() As DataTable
        Return Datos.Tabla("dbo.SP_LISTA_CLIENTES")
    End Function

    Public Shared Function Crear(nombre As String, cedula As String, telefono As String) As Integer
        Return Datos.Escalar("dbo.SP_CREA_CLIENTE",
                             Datos.Param("@nombre", nombre),
                             Datos.Param("@cedula", cedula),
                             Datos.Param("@telefono", telefono))
    End Function
End Class

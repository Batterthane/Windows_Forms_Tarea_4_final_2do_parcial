Imports System.Data

Public Class MetodosUsuarios
    Public Shared Function ListarCajeros() As DataTable
        Return Datos.Tabla("dbo.SP_LISTA_CAJEROS")
    End Function

    ' turno: Manana / Tarde / Noche
    Public Shared Function CrearCajero(usuario As String, claveHash As String, turno As String) As Integer
        Return Datos.Escalar("dbo.SP_CREA_CAJERO",
                             Datos.Param("@usuario", usuario),
                             Datos.Param("@clave", claveHash),
                             Datos.Param("@turno", turno))
    End Function

    Public Shared Function CambiarActivo(id As Integer, activo As Boolean) As Integer
        Return Datos.Escalar("dbo.SP_ACTIVA_CAJERO", Datos.Param("@id", id), Datos.Param("@activo", activo))
    End Function

    ' Usuarios que pueden facturar (para filtros de reportes)
    Public Shared Function ListarUsuariosVenta() As DataTable
        Return Datos.Tabla("dbo.SP_LISTA_USUARIOS_VENTA")
    End Function
End Class

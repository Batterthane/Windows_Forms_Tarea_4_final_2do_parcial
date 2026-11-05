' Estado global del usuario que inició sesión
Public Class Variable
    Public Shared Usuario As String = ""
    Public Shared Estado As String = ""   ' "Administrador", "Cajero" o "Invitado"
    Public Shared Turno As String = ""    ' solo cajeros: Manana / Tarde / Noche

    Public Shared ReadOnly Property EsAdmin() As Boolean
        Get
            Return Estado = "Administrador"
        End Get
    End Property

    Public Shared ReadOnly Property EsCajero() As Boolean
        Get
            Return Estado = "Cajero"
        End Get
    End Property

    ' Administrador y cajeros pueden facturar
    Public Shared ReadOnly Property PuedeFacturar() As Boolean
        Get
            Return EsAdmin OrElse EsCajero
        End Get
    End Property
End Class

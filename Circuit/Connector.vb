Public Class Connector

    Public Owner As Element
    Public OriginalLocation As Point
    Public Location As Point
    Public CanIn = True, CanOut = True
    Private _value As Boolean
    Public [To] As Connector
    Public Event ValueChanged(sender As Connector, args As ValueChangedEventArgs)

    Public Sub Update()
        Owner.UpdateValue(Me)
    End Sub

    Public Property Value() As Boolean
        Get
            Return _value
        End Get
        Set(ByVal newValue As Boolean)
            If newValue = _value OrElse Not CanIn Then
                Return
            End If
            Dim args As New ValueChangedEventArgs(_value, newValue)
            _value = newValue
            RaiseEvent ValueChanged(Me, args)
        End Set
    End Property

End Class

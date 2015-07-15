Public Class ValueChangedEventArgs
    Inherits EventArgs

    Public ValueFrom As Boolean
    Public ValueTo As Boolean

    Public Sub New([from] As Boolean, [to] As Boolean)
        ValueFrom = [from]
        ValueTo = [to]
    End Sub

End Class

Public Class Ammeter
    Inherits Element

    Public Sub New(title As String, location As Point)
        MyBase.New(title, location)
        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = New Point(-35, 0)})
        _connectors.Add(New Connector() With {.Owner = Me, .OriginalLocation = New Point(35, 0)})
    End Sub

    Public Overrides ReadOnly Property OriginalSize As Size
        Get
            Return New Size(70, 50)
        End Get
    End Property

    Protected Overrides Sub InternalDraw(g As Graphics)
        g.FillEllipse(Brushes.White, New Rectangle(-25, -25, 50, 50))
        g.DrawEllipse(Pens.Black, New Rectangle(-25, -25, 50, 50))

        Dim str = "A"
        Dim font = New Font("Consolas", 20)

        Dim strSize = g.MeasureString(str, font)

        g.DrawString(str, font, Brushes.Black, -strSize.Width / 2, -strSize.Height / 2)
        g.DrawLine(Pens.Black, -35, 0, -25, 0)
        g.DrawLine(Pens.Black, 25, 0, 35, 0)
    End Sub

    Public Overrides Sub Update(sender As Connector)

    End Sub

End Class

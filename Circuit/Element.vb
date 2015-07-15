Imports System.ComponentModel

Public MustInherit Class Element
    Implements IConnectable
    Implements IDrawable

    Public Enum RotationAngle '顺时针
        D0
        D90
        D180
        D270
    End Enum

    Public Shared RotationAngleToAngle As New Dictionary(Of RotationAngle, Integer) _
        From
        {{RotationAngle.D0, 0},
         {RotationAngle.D90, 90},
         {RotationAngle.D180, 180},
         {RotationAngle.D270, 270}}

    Public Sub New(ByVal title As String, ByVal location As Point)
        _title = title
        _location = location
    End Sub

    Protected _location As Point
    <DisplayName("位置"), CategoryAttribute("常规"), DescriptionAttribute("设置元件相对于原点的坐标"), DefaultValue(0)> _
    Public Property Location() As Point
        Get
            Return _location
        End Get
        Set(ByVal value As Point)
            _location = value
        End Set
    End Property

    <DisplayName("类型"), CategoryAttribute("常规"), DescriptionAttribute("元件的类型")> _
    Public Overridable ReadOnly Property Type() As String
        Get
            Return Me.GetType().Name
        End Get
    End Property

    Protected _title As String
    <DisplayName("符号"), CategoryAttribute("常规"), DescriptionAttribute("设置元件旁边的符号"), DefaultValue("")> _
    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal value As String)
            _title = value
        End Set
    End Property

    Protected _rotation As RotationAngle
    <DisplayName("旋转"), CategoryAttribute("常规"), DescriptionAttribute("设置顺时针旋转角度"), DefaultValue(RotationAngle.D0)> _
    Public Property Rotation() As RotationAngle
        Get
            Return _rotation
        End Get
        Set(ByVal value As RotationAngle)
            _rotation = value
        End Set
    End Property

    'Shared Function GetImage(ByVal EleType As ElementType) As Image
    '    Select Case EleType
    '        Case ElementType.Ammeter
    '            Return My.Resources.A
    '        Case ElementType.Light
    '            Return My.Resources.L
    '        Case ElementType.Resistor
    '            Return My.Resources.R
    '        Case ElementType.Switch
    '            Return My.Resources.S
    '        Case ElementType.VariableResistor
    '            Return My.Resources.vR
    '        Case ElementType.Voltmeter
    '            Return My.Resources.V
    '        Case ElementType.PowerSupply
    '            Return My.Resources.Ps
    '        Case ElementType.Bell
    '            Return My.Resources.B
    '        Case ElementType.Motor
    '            Return My.Resources.M
    '        Case Else
    '            Return New Bitmap(49, 49)
    '    End Select
    'End Function

    'Public Shared Function GetAbbr() As String
    '    Throw New NotImplementedException("won't be implemented")
    '    '    Select Case EleType
    '    '        Case ElementType.Ammeter
    '    '            Return "A"
    '    '        Case ElementType.Light
    '    '            Return "L"
    '    '        Case ElementType.VariableResistor, ElementType.Resistor
    '    '            Return "R"
    '    '        Case ElementType.Switch
    '    '            Return "S"
    '    '        Case ElementType.Voltmeter
    '    '            Return "V"
    '    '        Case ElementType.PowerSupply
    '    '            Return "E"
    '    '        Case ElementType.Bell
    '    '            Return "B"
    '    '        Case ElementType.Motor
    '    '            Return "M"
    '    '        Case Else
    '    '            Return ""
    '    '    End Select
    'End Function

    Protected _connectors As New List(Of Connector)
    <DisplayName("连接点"), CategoryAttribute("常规"), DescriptionAttribute("所有连接点")> _
    Public ReadOnly Property Connectors As List(Of Connector) Implements IConnectable.Connectors
        Get
            Return _connectors
        End Get
    End Property

    <DisplayName("占地大小"), CategoryAttribute("常规"), DescriptionAttribute("获取该元件的占地大小")> _
    Public ReadOnly Property Size As Size
        Get
            Select Case _rotation
                Case RotationAngle.D0, RotationAngle.D180
                    Return OriginalSize
                Case RotationAngle.D90, RotationAngle.D270
                    Return New Size(OriginalSize.Height, OriginalSize.Width)
            End Select
        End Get
    End Property

    <DisplayName("图形原始大小"), CategoryAttribute("常规"), DescriptionAttribute("获取该元件的图形原始大小")> _
    Public MustOverride ReadOnly Property OriginalSize As Size

    Public Function Boundary() As Rectangle
        Return New Rectangle(New Point(Location.X - Size.Width / 2, Location.Y - Size.Height / 2), Size)
    End Function

    Public Function OriginalBoundary() As Rectangle
        Return New Rectangle(New Point(-OriginalSize.Width / 2, -OriginalSize.Height / 2), OriginalSize)
    End Function

    Public Overridable Sub Draw(g As Graphics) Implements IDrawable.Draw
        Dim state = g.Save()

        g.TranslateTransform(_location.X, _location.Y)
        g.RotateTransform(RotationAngleToAngle(_rotation))

        InternalDraw(g)

        g.Restore(state)
    End Sub

    Protected MustOverride Sub InternalDraw(g As Graphics)

    Public MustOverride Sub Update(sender As Connector) Implements IConnectable.UpdateValue

    Public Sub DrawBoundary(g As Graphics) Implements IDrawable.DrawBoundary
        Dim state = g.Save()

        g.TranslateTransform(_location.X, _location.Y)
        g.RotateTransform(RotationAngleToAngle(_rotation))

        g.DrawRectangle(Pens.Gray, OriginalBoundary)

        g.Restore(state)
    End Sub
End Class

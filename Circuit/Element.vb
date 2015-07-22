Imports System.ComponentModel
Imports Newtonsoft.Json

Public MustInherit Class Element

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
            UpdateProperties()
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
    Public Overridable Property Rotation() As RotationAngle
        Get
            Return _rotation
        End Get
        Set(ByVal value As RotationAngle)
            _rotation = value
            UpdateProperties()
        End Set
    End Property

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
    <DisplayName("连接点"), CategoryAttribute("常规"), DescriptionAttribute("所有连接点"), JsonIgnore> _
    Public ReadOnly Property Connectors As List(Of Connector)
        Get
            Return _connectors
        End Get
    End Property

    Protected _size As New Size(0, 0)

    <DisplayName("占地大小"), CategoryAttribute("常规"), DescriptionAttribute("获取该元件的占地大小"), JsonIgnore> _
    Friend ReadOnly Property Size As Size
        Get
            Return _size
        End Get
    End Property

    Protected _originalSize As New Size(0, 0)

    <DisplayName("图形原始大小"), CategoryAttribute("常规"), DescriptionAttribute("获取该元件的图形原始大小"), JsonIgnore> _
    Friend ReadOnly Property OriginalSize As Size
        Get
            Return _originalSize
        End Get
    End Property

    Protected _boundary As New Rectangle(0, 0, 0, 0)

    Friend Overridable ReadOnly Property Boundary As Rectangle
        Get
            Return _boundary
        End Get
    End Property

    Protected _originalBoundary As New Rectangle(0, 0, 0, 0)

    Friend Overridable ReadOnly Property OriginalBoundary As Rectangle
        Get
            Return _originalBoundary
        End Get
    End Property

    Public Overridable Function Contains(p As Point) As Boolean
        Return _boundary.Contains(p)
    End Function

    Public Function GetDescription() As String
        Dim text = _title
        If text = "" Then
            text = Type
        End If
        Return text
    End Function

    Public Overridable Sub Draw(g As Graphics)
        Dim state = g.Save()

        g.TranslateTransform(_location.X, _location.Y)
        g.RotateTransform(RotationAngleToAngle(_rotation))

        InternalDraw(g)

        For i = 0 To _connectors.Count - 1
            g.FillRectangle(Brushes.Gray, New Rectangle(_connectors(i).OriginalLocation - New Point(2, 2), New Size(4, 4)))
        Next

        g.Restore(state)

        If _title <> "" Then
            g.DrawString(_title, New Font("Consolas", 13), Brushes.Black, _boundary.Location)
        End If
    End Sub

    Protected MustOverride Sub InternalDraw(g As Graphics)

    Public MustOverride Sub UpdateValue(valueChangedConnector As Connector)

    Public Overridable Sub DrawBoundary(g As Graphics)
        'Dim state = g.Save()

        'g.TranslateTransform(_location.X, _location.Y)
        'g.RotateTransform(RotationAngleToAngle(_rotation))

        g.DrawRectangle(Pens.Blue, _boundary)

        'g.Restore(state)
    End Sub

    Private Function GetTransform() As Func(Of Point, Point)
        Dim trans As Func(Of Point, Point)
        Select Case _rotation
            Case RotationAngle.D0
                trans = Function(p As Point) p
            Case RotationAngle.D90
                trans = Function(p As Point) New Point(-p.Y, p.X)
            Case RotationAngle.D180
                trans = Function(p As Point) New Point(-p.X, -p.Y)
            Case RotationAngle.D270
                trans = Function(p As Point) New Point(p.Y, -p.X)
            Case Else
                Throw New ArgumentException()
        End Select
        Return trans
    End Function

    ''' <summary>
    ''' 更新一些属性, 在初始化以及Rotation或Location变动时应当调用
    ''' </summary>
    ''' <remarks></remarks>
    Public Overridable Sub UpdateProperties()
        '更新连接器实际位置
        Dim trans = GetTransform()
        For i = 0 To _connectors.Count - 1
            _connectors(i).Location = trans(_connectors(i).OriginalLocation)
        Next

        '大小
        Select Case _rotation
            Case RotationAngle.D0, RotationAngle.D180
                _size = _originalSize
            Case RotationAngle.D90, RotationAngle.D270
                _size = New Size(_originalSize.Height, _originalSize.Width)
        End Select

        _boundary = New Rectangle(New Point(_location.X - _size.Width / 2, _location.Y - _size.Height / 2), _size)
    End Sub

    Public Sub RemoveConnections()
        For Each c In _connectors
            c.RemoveConnection()
        Next
    End Sub

End Class

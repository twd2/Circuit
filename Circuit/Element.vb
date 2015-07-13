Imports System.ComponentModel

Public Class Element
    Enum ElementType
        Light
        Switch
        Ammeter
        Voltmeter
        VariableResistor
        Resistor
        PowerSupply
        Bell
        Motor
    End Enum

    Sub New(ByVal EleType As ElementType, ByVal Title As String, ByVal Location As Point)
        m_Type = EleType
        m_Title = Title
        m_Location = Location
    End Sub

    Private m_Location As Point
    <DisplayName("位置"), CategoryAttribute("常规"), DescriptionAttribute("设置元件相对于原点的坐标"), DefaultValue(0)> _
    Public Property Location() As Point
        Get
            Return m_Location
        End Get
        Set(ByVal value As Point)
            m_Location = value
        End Set
    End Property

    Private m_Type As ElementType
    <DisplayName("类型"), CategoryAttribute("常规"), DescriptionAttribute("设置元件的类型")> _
     Public Property Type() As ElementType
        Get
            Return m_Type
        End Get
        Set(ByVal value As ElementType)
            m_Type = value
        End Set
    End Property

    Private m_Title As String
    <DisplayName("标题"), CategoryAttribute("常规"), DescriptionAttribute("设置元件旁边的标题"), DefaultValue("")> _
    Public Property Title() As String
        Get
            Return m_Title
        End Get
        Set(ByVal value As String)
            m_Title = value
        End Set
    End Property

    Private m_Rotation As Boolean
    <DisplayName("旋转"), CategoryAttribute("常规"), DescriptionAttribute("设置元件是否顺时针90°旋转"), DefaultValue(False)> _
    Public Property Rotation() As Boolean
        Get
            Return m_Rotation
        End Get
        Set(ByVal value As Boolean)
            m_Rotation = value
        End Set
    End Property

    Shared Function GetImage(ByVal EleType As ElementType) As Image
        Select Case EleType
            Case ElementType.Ammeter
                Return My.Resources.A
            Case ElementType.Light
                Return My.Resources.L
            Case ElementType.Resistor
                Return My.Resources.R
            Case ElementType.Switch
                Return My.Resources.S
            Case ElementType.VariableResistor
                Return My.Resources.vR
            Case ElementType.Voltmeter
                Return My.Resources.V
            Case ElementType.PowerSupply
                Return My.Resources.Ps
            Case ElementType.Bell
                Return My.Resources.B
            Case ElementType.Motor
                Return My.Resources.M
            Case Else
                Return New Bitmap(49, 49)
        End Select
    End Function

    Shared Function GetAbbr(ByVal EleType As ElementType) As String
        Select Case EleType
            Case ElementType.Ammeter
                Return "A"
            Case ElementType.Light
                Return "L"
            Case ElementType.VariableResistor, ElementType.Resistor
                Return "R"
            Case ElementType.Switch
                Return "S"
            Case ElementType.Voltmeter
                Return "V"
            Case ElementType.PowerSupply
                Return ""
            Case ElementType.Bell
                Return "B"
            Case ElementType.Motor
                Return "M"
            Case Else
                Return ""
        End Select
    End Function

End Class

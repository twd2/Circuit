Public Class Line
    Sub New(ByVal P1 As Point, ByVal P2 As Point)
        m_P1 = P1
        m_P2 = P2
    End Sub

    Private m_P1 As Point
    Public Property Point1() As Point
        Get
            Return m_P1
        End Get
        Set(ByVal value As Point)
            m_P1 = value
        End Set
    End Property

    Private m_P2 As Point
    Public Property Point2() As Point
        Get
            Return m_P2
        End Get
        Set(ByVal value As Point)
            m_P2 = value
        End Set
    End Property

    Private m_oP1 As Point
    Public Property OldPoint1() As Point
        Get
            Return m_oP1
        End Get
        Set(ByVal value As Point)
            m_oP1 = value
        End Set
    End Property

    Private m_oP2 As Point
    Public Property OldPoint2() As Point
        Get
            Return m_oP2
        End Get
        Set(ByVal value As Point)
            m_oP2 = value
        End Set
    End Property

End Class

Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Drawing.Printing
Imports System.Threading
Imports System.ComponentModel
Imports WDList

Public Class frmMain

    Private WithEvents _elements As New MyList(Of Element)
    Private _panel As ElementsPanel
    Private _engine As DigitalEngine

    Private WithEvents Printer As New PrintDocument

    Private _renderThread As Thread
    Private _isRunning As Boolean = False
    Private _lastFPS As Integer = 0
    Private _lastFPSTime As DateTime = DateTime.Now

    Private Sub OnListChanged(sender As Object, e As ChangedEventArgs(Of Element)) Handles _elements.Changed
        Dim sb As New StringBuilder()
        sb.AppendFormat("{0} = {1}", "ListChangedType", e.Type)
        sb.AppendLine()
        sb.AppendFormat("{0} = {1}", "NewIndex", e.NewIndex)
        sb.AppendLine()
        sb.AppendFormat("{0} = {1}", "OldIndex", e.OldIndex)
        sb.AppendLine()
        sb.AppendFormat("{0} = {1}", "Item", e.Item)
        sb.AppendLine()
        Utilities.Info(sb.ToString())
    End Sub

    Public Sub StartRendering()
        If _isRunning Then
            Return
        End If
        _renderThread = New Thread(AddressOf RenderWorker)
        _isRunning = True
        _renderThread.Start()
    End Sub

    Public Sub StopRendering()
        If Not _isRunning Then
            Return
        End If
        _isRunning = False
        _renderThread = Nothing
        labFPS.Text = "-"
    End Sub

    Public Sub RenderWorker()
        Utilities.Info("RenderWorker started")
        Do While _isRunning
            picMain.Invoke(New Action(AddressOf _panel.Render))

            _lastFPS += 1

            If (DateTime.Now - _lastFPSTime).TotalMilliseconds >= 1000 Then
                labFPS.Text = _lastFPS.ToString()
                _lastFPS = 0
                _lastFPSTime = DateTime.Now
            End If

            Thread.Sleep(0)
        Loop
        Utilities.Info("RenderWorker stopped")
    End Sub

    Private Sub LoadImages()
        picAmmeter.Image = Ammeter.GetImage()
        picAmmeter.Refresh()
        picVoltmeter.Image = Voltmeter.GetImage()
        picVoltmeter.Refresh()
        picLight.Image = Light.GetImage()
        picLight.Refresh()
        picMotor.Image = Motor.GetImage()
        picMotor.Refresh()
        picNotGate.Image = NotGate.GetImage()
        picNotGate.Refresh()
        picHub3p.Image = Hub3p.GetImage()
        picHub3p.Refresh()
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadImages()

        _panel = New ElementsPanel(Me, _elements)
        _panel.Render()
    End Sub

    Private Sub picAmmeter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picAmmeter.Click
        _elements.Add(New Ammeter("A1", New Point(50, 50) - _panel.Origin))
        _panel.Render()
        PropertyGrid1.SelectedObject = _panel.Elements(_panel.Elements.Count - 1)
    End Sub

    Private Sub 删除ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除ToolStripMenuItem.Click
        _panel.DeleteSelected()
        _panel.Render()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        _panel.ResetOrigin()
    End Sub

    Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If _panel Is Nothing Then
            Return
        End If
        _panel.Resize()
        _panel.Render()
    End Sub

    Private Sub btnWire_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWire.Click
        _panel.StartDrawWire()
    End Sub

    Private Sub 新建ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新建ToolStripMenuItem.Click
        NewCircuit()
    End Sub

    Sub NewCircuit()
        _panel.Elements.Clear()
        _panel.ResetOrigin()
        _panel.Render()
    End Sub

    Private Sub 保存ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 保存ToolStripMenuItem.Click
        Throw New NotImplementedException
        'Using SFD As New SaveFileDialog
        '    SFD.Title = "电路图保存路径"
        '    SFD.Filter = FileFilter
        '    If SFD.ShowDialog = DialogResult.OK Then
        '        'SavePic(SFD.FileName)
        '        WriteCirData(SFD.FileName)
        '    End If
        'End Using
    End Sub

    Private Sub 打开ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 打开ToolStripMenuItem.Click
        Throw New NotImplementedException
        'Using OFD As New OpenFileDialog
        '    OFD.Title = "电路图存储路径"
        '    OFD.Filter = FileFilter
        '    If OFD.ShowDialog = DialogResult.OK Then
        '        LoadCirData(OFD.FileName)
        '    End If
        'End Using
    End Sub

    Sub LoadCirData(ByVal Filename As String)
        Throw New NotImplementedException
        'Try
        '    NewCircuit()
        '    Dim XD As New XmlDocument()
        '    XD.Load(Filename)
        '    Dim root = XD.SelectSingleNode("Circuit")
        '    For Each XE As XmlElement In root.SelectNodes("Element")
        '        Dim MyElement As New Element([Enum].Parse(GetType(Element.ElementType), XE.GetAttribute("Type")), XE.GetAttribute("Title"), ToPoint(XE.GetAttribute("Location")))
        '        MyElement.Rotation = (XE.GetAttribute("Rotation") = "True")
        '        Elements.Add(MyElement)
        '    Next

        '    For Each XE As XmlElement In root.SelectNodes("Line")
        '        Dim MyLine As New Line(ToPoint(XE.GetAttribute("Point1")), ToPoint(XE.GetAttribute("Point2")))
        '        Lines.Add(MyLine)
        '    Next
        '    ReDraw()
        'Catch ex As Exception
        '    MsgBox(ex.ToString, 16 + 0, "错误的文件")
        'End Try
    End Sub

    Private Sub PrintToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripMenuItem.Click
        Throw New NotImplementedException
        'Printer.DocumentName = "电路图"
        'Printer.Print()
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If _engine IsNot Nothing Then
            Return
        End If
        _engine = New DigitalEngine(_elements)
        _engine.Elements = _panel.Elements
        _engine.Start()
        StartRendering()
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        If _engine Is Nothing Then
            Return
        End If
        _engine.Stop()
        _engine = Nothing
        StopRendering()
    End Sub

    Private Sub picNot_Click(sender As Object, e As EventArgs) Handles picNotGate.Click
        _elements.Add(New NotGate("N1", New Point(50, 50) - _panel.Origin))
        _panel.Render()
        PropertyGrid1.SelectedObject = _panel.Elements(_panel.Elements.Count - 1)
    End Sub

    Private Sub picVoltmeter_Click(sender As Object, e As EventArgs) Handles picVoltmeter.Click
        _elements.Add(New Voltmeter("V1", New Point(50, 50) - _panel.Origin))
        _panel.Render()
        PropertyGrid1.SelectedObject = _panel.Elements(_panel.Elements.Count - 1)
    End Sub

    Private Sub picMotor_Click(sender As Object, e As EventArgs) Handles picMotor.Click
        _elements.Add(New Motor("M1", New Point(50, 50) - _panel.Origin))
        _panel.Render()
        PropertyGrid1.SelectedObject = _panel.Elements(_panel.Elements.Count - 1)
    End Sub

    Private Sub picLight_Click(sender As Object, e As EventArgs) Handles picLight.Click
        _elements.Add(New Light("L1", New Point(50, 50) - _panel.Origin))
        _panel.Render()
        PropertyGrid1.SelectedObject = _panel.Elements(_panel.Elements.Count - 1)
    End Sub

    Private Sub picHub3p_Click(sender As Object, e As EventArgs) Handles picHub3p.Click
        _elements.Add(New Hub3p("", New Point(50, 50) - _panel.Origin))
        _panel.Render()
        PropertyGrid1.SelectedObject = _panel.Elements(_panel.Elements.Count - 1)
    End Sub
End Class

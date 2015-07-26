Imports System.Threading
Imports System.ComponentModel
Imports WDList

Public Class DigitalEngine

    Public IsRunning As Boolean = False
    Public WithEvents Elements As MyList(Of Element)

    '待更新连接器的队列
    Private Q As New Queue(Of Connector)
    Private eventEnqueue As New ManualResetEvent(False)


    Private threadWorker As Thread

    Public Sub New(elements As MyList(Of Element))
        Me.Elements = elements
    End Sub

    Private Sub OnListChanged(sender As Object, e As ChangedEventArgs(Of Element)) Handles Elements.Changed
        Select Case e.Type
            Case ChangedType.AddOrInsert
                Utilities.Info("DigitalEngine: On add element, adding handler")
                OnAddElement(e.Item)
            Case ChangedType.Remove
                Utilities.Info("DigitalEngine: On remove element, removing handler")
                OnRemoveElement(e.Item)
        End Select
    End Sub

    Private Sub OnAddElement(e As Element)
        SyncLock Elements
            If IsRunning Then
                '如果正在运行则添加Handler
                For Each c In e.Connectors
                    AddHandler c.ValueChanged, AddressOf ValueChangedHandler
                Next
            End If
        End SyncLock
    End Sub

    Private Sub OnRemoveElement(e As Element)
        SyncLock Elements
            If IsRunning Then
                '如果正在运行则添加Handler
                For Each c In e.Connectors
                    RemoveHandler c.ValueChanged, AddressOf ValueChangedHandler
                Next
            End If
        End SyncLock
    End Sub

    Public Sub Start()
        If IsRunning Then
            Return
        End If

        IsRunning = True

        SyncLock Elements
            For Each ic In Elements
                For Each c In ic.Connectors
                    AddHandler c.ValueChanged, AddressOf ValueChangedHandler
                Next
            Next
        End SyncLock

        threadWorker = New Thread(AddressOf Worker)
        threadWorker.Start()
    End Sub

    Public Sub [Stop]()
        If Not IsRunning Then
            Return
        End If

        IsRunning = False

        SyncLock Q
            eventEnqueue.Set()
        End SyncLock

        threadWorker.Join()

        SyncLock Elements
            For Each ic In Elements
                For Each c In ic.Connectors
                    RemoveHandler c.ValueChanged, AddressOf ValueChangedHandler
                Next
            Next
        End SyncLock
    End Sub

    Public Sub [Next]()
        Dim conn As Connector = Nothing
        eventEnqueue.WaitOne()
        SyncLock Q
            If Q.Count > 0 Then
                conn = Q.Dequeue()
                'Utilities.Info("Dequeue")
            End If
            If Q.Count <= 0 Then
                eventEnqueue.Reset()
            End If
        End SyncLock
        If conn IsNot Nothing Then
            conn.Update()
        End If
    End Sub

    Private Sub Worker()
        Do While IsRunning
            [Next]()
            Thread.Sleep(500)
        Loop
    End Sub

    Private Sub ValueChangedHandler(sender As Connector, args As ValueChangedEventArgs)
        SyncLock Q
            Q.Enqueue(sender)
            'Utilities.Info("Enqueue")
            eventEnqueue.Set()
        End SyncLock
    End Sub

End Class

Imports System.Threading

Public Class DigitalEngine

    Public IsRunning As Boolean = False
    Public Elements As List(Of IConnectable)

    '待更新连接器的队列
    Private Q As Queue(Of Connector)

    Private threadWorker As Thread

    Public Sub AddElement(ic As IConnectable)
        SyncLock Elements
            If IsRunning Then
                '如果正在运行则添加Handler
                For Each c In ic.Connectors
                    AddHandler c.ValueChanged, AddressOf ValueChangedHandler
                Next
            End If
            Elements.Add(ic)
        End SyncLock
    End Sub

    Public Sub RemoveElement(index As Integer)
        SyncLock Elements
            If IsRunning Then
                '如果正在运行则添加Handler
                Dim ic = Elements(index)
                For Each c In ic.Connectors
                    RemoveHandler c.ValueChanged, AddressOf ValueChangedHandler
                Next
            End If
            Elements.RemoveAt(index)
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
        SyncLock Q
            conn = Q.Peek()
            If conn IsNot Nothing Then
                Q.Dequeue()
            End If
        End SyncLock
        If conn IsNot Nothing Then
            conn.Update()
        End If
    End Sub

    Private Sub Worker()
        Do While IsRunning
            [Next]()
            'Thread.Sleep(0)
        Loop
    End Sub

    Private Sub ValueChangedHandler(sender As Connector, args As ValueChangedEventArgs)
        SyncLock Q
            Q.Enqueue(sender)
        End SyncLock
    End Sub



End Class

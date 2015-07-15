Public Interface IConnectable


    ReadOnly Property Connectors() As List(Of Connector)

    Sub UpdateValue(valueChangedConnector As Connector)

End Interface

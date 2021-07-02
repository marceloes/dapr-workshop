param longName string

resource serviceBus 'Microsoft.ServiceBus/namespaces@2021-01-01-preview' = {
  name: 'sb-${longName}'
  location: resourceGroup().location
  identity: {
    type: 'SystemAssigned'
  }
  sku: {
    name: 'Standard'
    tier: 'Standard'
    capacity: 1
  }
}

resource serviceBusTrafficControlTopic 'Microsoft.ServiceBus/namespaces/topics@2017-04-01' = {
  name: '${serviceBus.name}/trafficcontrol'
}

output serviceBusName string = serviceBus.name

param longName string

resource serviceBus 'Microsoft.ServiceBus/namespaces@2021-01-01-preview' = {
  name: 'sb-${longName}'
  location: resourceGroup().location  
}

output serviceBusName string = serviceBus.name

param longName string

resource logicApp 'Microsoft.Logic/workflows@2019-05-01' = {
  name: 'logic-smtp-${longName}'
  location: resourceGroup().location
  properties: {
    parameters: {
      
    }
    definition: {
      '$schema': 'https://schema.management.azure.com/providers/Microsoft.Logic/schemas/2016-06-01/workflowdefinition.json#'
      parameters: {

      }
      triggers: {

      }
      actions: {

      }
    }
  }
}

output logicAppName string = logicApp.name

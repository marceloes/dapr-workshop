param appName string
param region string
param environment string
param adminUsername string
param publicSSHKey string

var longName = '${appName}-${region}-${environment}'

module keyVaultModule 'keyVault.bicep' = {
  name: 'keyVaultDeploy'
  params: {
    longName: longName
  }
}

module serviceBusModule 'serviceBus.bicep' = {
  name: 'serviceBusDeploy'
  params: {
    longName: longName
  }
}

module logicAppModule 'logicApp.bicep' = {
  name: 'logicAppDeploy'
  params: {
    longName: longName
  }  
}

module containerRegistryModule 'containerRegistry.bicep' = {
  name: 'containerRegistryDeploy'
  params: {
    longName: longName
  }
}

module aksModule 'aks.bicep' = {
  name: 'aksDeploy'
  params: {
    longName: longName
    adminUsername: adminUsername
    publicSSHKey: publicSSHKey
  }  
}

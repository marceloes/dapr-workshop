# How to deploy infrastructure

## Deploy manually

1. Create a new resource group to deploy your Azure infrastructure into.

```
New-AzResourceGroup -Name rg-dapr-workshop-ussc-demo -Location ussc
```

1. Generate an SSH key pair if you don't already have one.

```
ssh-keygen -t rsa -b 2048
```

1. Configure & run the following PowerShell to set up the input parameters.

```
$params = @{ 
  appName="dapr"
  region="ussc"
  environment="demo"
  adminUsername="adminBruce"
  publicSSHKey="ssh-rsa AAAAB...wnBTn bruce.wayne@wayneenterprises.com"
}
```

1. Run the following PowerShell command to deploy.

```
New-AzResourceGroupDeployment -ResourceGroupName rg-dapr-workshop-ussc-demo -TemplateParameterObject $params -TemplateFile ./main.bicep -Verbose
```

## GitHub Actions
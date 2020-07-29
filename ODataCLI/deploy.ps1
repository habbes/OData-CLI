param(
 [Parameter(Mandatory=$True)]
 [string]
 $subscriptionId,

 [string]
 $resourceGroupName = "ODATASAMPLERGROUP36",

 [string]
 $resourceGroupLocation = "Central US",

 [string]
 $deploymentName = "ODATASAMPLEDEPLOYMENT36",

 [string]
 $templateFilePath = "azuredeploy.json",

 [string]
 $parametersFilePath = "azuredeploy.parameters.json",

 [Parameter(Mandatory=$True)]
 [string]
 $projectFilePath
)

#******************************************************************************
# Script body
# Execution begins here
#******************************************************************************
$ErrorActionPreference = "Stop"

# sign in
Write-Host "Logging in...";
Login-AzureRmAccount;

# select subscription
Write-Host "Selecting subscription '$subscriptionId'";
Select-AzureRmSubscription -SubscriptionID $subscriptionId;

#Create or check for existing resource group
$resourceGroup = Get-AzureRmResourceGroup -Name $resourceGroupName -ErrorAction SilentlyContinue
if(!$resourceGroup)
{
    Write-Host "Creating resource group '$resourceGroupName' in location '$resourceGroupLocation'";
    New-AzureRmResourceGroup -Name $resourceGroupName -Location $resourceGroupLocation
}
else{
    Write-Host "Using existing resource group '$resourceGroupName'";
}

# Start the deployment
Write-Host "Starting deployment...";
if(Test-Path $parametersFilePath) {
    New-AzureRmResourceGroupDeployment -ResourceGroupName $resourceGroupName -TemplateFile $templateFilePath -TemplateParameterFile $parametersFilePath;   
} 
else {
    New-AzureRmResourceGroupDeployment -ResourceGroupName $resourceGroupName -TemplateFile $templateFilePath;
}
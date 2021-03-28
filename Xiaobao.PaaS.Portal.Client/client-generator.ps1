function GenerateClient($serviceName, $swaggerUrl) {
    $clientFileName = "${serviceName}Client.cs"
    $apiNamespace = "${serviceName}Sdk"

nswag swagger2csclient `
    "/Input:${swaggerUrl}" `
    "/Output:Client/${clientFileName}" `
    "/Namespace:${apiNamespace}" `
    "/ClassName:${serviceName}Client" `
    "/GenerateClientInterfaces:true" `
    "/OperationGenerationMode:SingleClientFromOperationId" `
    "/UseBaseUrl:false" `
    "/DateType:System.DateTime" `
    "/DateTimeType:System.DateTime" 
 
    if ($LastExitCode) {
        write-host ""
        write-error "Client generation failed!"
    } else {
        write-host -foregroundcolor green "Updated API client: ${clientFileName}"
    }
}


GenerateClient 'PaasPortal' './Swagger/paas-portal-swagger.json'

"Any key to exit"  ;
 Read-Host | Out-Null ;
Exit

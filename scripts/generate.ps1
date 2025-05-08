$apiName = 'routing'

npm install @openapitools/openapi-generator-cli

openapi-generator-cli version-manager set 7.8.0

openapi-generator-cli generate --generator-name csharp --config net48-configuration.yaml --input-spec "https://api.myptv.com/meta/services/$apiName/v1/openapi.json" "--additional-properties=packageName=PTV.Developer.Clients.$apiName"  --global-property "apis,apiTests=false,models,modelTests=false,supportingFiles" --output "output/dotnet/$apiName" --openapi-normalizer SET_PRIMITIVE_TYPES_TO_NULLABLE='\"integer|number|string|boolean|dateTime\"'

Remove-Item ..\PTV.Developer.Clients.$apiName -Recurse -ErrorAction SilentlyContinue

copy-item output\dotnet\$apiName\src\* -Destination ..\ -force -recurse -verbose
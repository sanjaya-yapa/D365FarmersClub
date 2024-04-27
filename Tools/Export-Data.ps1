pac data export --environment https://fcm-dev0424.crm6.dynamics.com/ --schemaFile C:\PROJECTS\OSIRIS\Solutions\FarmersClubData\data_schema.xml --dataFile C:\PROJECTS\OSIRIS\Solutions\FarmersClubData\data.zip

$zipFilePath = "C:\PROJECTS\OSIRIS\Solutions\FarmersClubData\data.zip"
$destinationPath = "C:\PROJECTS\OSIRIS\Solutions\FarmersClubData"

if(Test-Path $zipFilePath){
    Expand-Archive -Path $zipFilePath -DestinationPath $destinationPath -Force
    Remove-Item $zipFilePath -Force
} else {
    Write-Host "File not found"
}
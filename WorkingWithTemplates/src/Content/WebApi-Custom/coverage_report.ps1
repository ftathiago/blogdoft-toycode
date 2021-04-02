Write-Host "Cleanning project"
Get-ChildItem -name -r | select-string -pattern ".*open.*\.xml" | ForEach-Object { Remove-Item $_ }
Get-ChildItem -name -r | select-string -Pattern "\\coverage_report\\" | ForEach-Object { Remove-Item $_ -Recurse -Force -Confirm:$false | Out-Null }
dotnet clean | Out-Null

Write-Host "Update/Install Report generator"
dotnet tool update -g dotnet-reportgenerator-globaltool | Out-Null

Write-Host "Running tests. Please wait."
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput=coverage.opencover.xml | Out-Null

Write-Host "Generating report."
reportgenerator -reports:__tests__/**/coverage.opencover.xml -targetdir:coverage_report | Out-Null

Write-Host "Open default browser"
coverage_report\index.html

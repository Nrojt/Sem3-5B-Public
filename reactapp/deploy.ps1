# Check if the username is provided
param (
    [Parameter(Mandatory=$true)]
    [string]$deployUser
)

Write-Host "building app ..."
npm run build

Write-Host "deploying app ..."
scp -r .\dist\* ${deployUser}@164.92.149.13:/var/www/sem3-5b.com/html

Write-Host "Restarting Nginx on the server, requires ssh key ..."
ssh ${deployUser}@164.92.149.13 'sudo systemctl restart nginx'

Write-Host "done"

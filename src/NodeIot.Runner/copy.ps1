[CmdletBinding()]
param (
    [Parameter(Mandatory)]
    [string] $node
)

$artifacts = "../../artifacts/bin/Node.Runner/Debug/net48"

Remove-Item $artifacts/logs/ -r *>$null
Write-Host "Removed local logs"

ssh $node "rm node -r" *>$null
Write-Host "Removed remote files"

Write-Host "Copying files to remote"
scp -r $artifacts "$node:node" *>$null

Write-Host "Finished"
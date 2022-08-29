while ($true) {
    if (-not (Get-AppxPackage *aad.broke*)) {
        Write-Host (Get-Date -Format O) + "OH NO THE PACKAGE IS GONE"
    }
    Start-Sleep -Seconds 1
}
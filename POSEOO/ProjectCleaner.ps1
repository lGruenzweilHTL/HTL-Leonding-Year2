param (
    [string]$solutionDir
)

# Convert to absolute path
try {
    $absolutePath = Convert-Path $solutionDir
    Write-Host "Absolute Path: $absolutePath"
    Write-Host "Relative Path: $solutionDir"
} catch {
    Write-Host "The path $solutionDir does not exist."
}

# Ensure the solution directory exists
if (-Not (Test-Path -Path $absolutePath)) {
    Write-Host "The specified solution directory does not exist."
    exit 1
}

# Change to the solution directory
Set-Location -Path $absolutePath

# Find all .csproj files and run dotnet clean on each
Get-ChildItem -Path $absolutePath -Filter *.csproj | ForEach-Object {
    Write-Host "Cleaning project: $_.FullName"
    dotnet clean $_.FullName
}

# Extract the directory name
$directoryName = Split-Path -Path $absolutePath -Leaf

# Zip the solution directory
$zipFile = "$absolutePath$directoryName.zip"
Write-Host "zip name: $zipFile"

Compress-Archive -Path $absolutePath -DestinationPath $zipFile

Write-Host "Solution directory zipped to: $zipFile"

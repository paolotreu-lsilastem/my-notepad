# Increments the build number in the Version tag of MyNotepad.csproj
$csproj = "MyNotepad.csproj"
[xml]$xml = Get-Content $csproj
$propertyGroup = $xml.Project.PropertyGroup

# Trova o crea il nodo <Version>
$versionNode = $propertyGroup.SelectSingleNode("Version")
if ($null -eq $versionNode) {
    $versionNode = $xml.CreateElement("Version")
    $versionNode.InnerText = "1.0.0"
    $null = $propertyGroup.AppendChild($versionNode)
} else {
    $version = $versionNode.InnerText
    $parts = $version -split '\.'
    if ($parts.Length -lt 3) { $parts += @(0) * (3 - $parts.Length) }
    $parts[2] = [int]$parts[2] + 1
    $newVersion = "$($parts[0]).$($parts[1]).$($parts[2])"
    $versionNode.InnerText = $newVersion
}
$xml.Save($csproj)
Write-Host "Version updated to $($versionNode.InnerText)"

param([String]$version = "1.0.0")

function updateVersion([String] $file) {
	$bs = Get-Content $file
	$json = ConvertFrom-Json "$bs"
	$json.version = $version
	$bs = ConvertTo-Json $json
	$bs > $file
}

updateVersion(".\BankService\apiapp.json")
updateVersion(".\KeyVault\apiapp.json")
updateVersion(".\UserService\apiapp.json")

Write-Host "Updated to version $version"
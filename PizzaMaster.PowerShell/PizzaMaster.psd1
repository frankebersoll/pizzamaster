@{
	RootModule = "PizzaMaster.PowerShell.dll"
	ModuleVersion = "1.0.0"
	Author = "Frank Ebersoll"

	NestedModules = "PizzaMasterScripts"

	FormatsToProcess = "PizzaMaster.format.ps1xml"
	TypesToProcess = "PizzaMaster.types.ps1xml"

	CmdletsToExport = "New-Konto",
				      "Get-Konto",
					  "Add-Einzahlung",
					  "Add-Abbuchung",
					  "New-Bestellung",
					  "Remove-Bestellung",
					  "Get-Bestellung",
					  "Get-Artikel",
				      "Complete-Bestellung",
					  "Remove-Bestellung",
					  "Get-ArtikelFromOutlook",
					  "Start-ArtikelZuordnung",
					  "Reset-PizzaMasterReadModels"
}
Add-Type -Assembly "Microsoft.Office.Interop.Outlook" | Out-Null
$global:PSModuleAutoloadingPreference = "none"

Function Global:prompt {"PizzaMaster > "}

Write-Host @"
                    ___
                    |  ~~--.
                    |%=@%%/
                    |o%%%/
                 __ |%%o/
           _,--~~ | |(_/ ._
        ,/'  m%%%%| |o/ /  ``\.
       /' m%%o(_)%| |/ /o%%m ``\
     /' %%@=%o%%%o|   /(_)o%%% ``\
    /  %o%%%%%=@%%|  /%%o%%@=%%  \
   |  (_)%(_)%%o%%| /%%%=@(_)%%%  |
   | %%o%%%%o%%%(_|/%o%%o%%%%o%%% |
   | %%o%(_)%%%%%o%(_)%%%o%%o%o%% |
   |  (_)%%=@%(_)%o%o%%(_)%o(_)%  |
    \ ~%%o%%%%%o%o%=@%%o%%@%%o%~ /
     \. ~o%%(_)%%%o%(_)%%(_)o~ ,/
       \_ ~o%=@%(_)%o%%(_)%~ _/
         ``\_~~o%%%o%%%%%~~_/'
            ``--..____,,--'
              
   ██████╗ ██╗███████╗███████╗ █████╗ 
   ██╔══██╗██║╚══███╔╝╚══███╔╝██╔══██╗
   ██████╔╝██║  ███╔╝   ███╔╝ ███████║
   ██╔═══╝ ██║ ███╔╝   ███╔╝  ██╔══██║
   ██║     ██║███████╗███████╗██║  ██║
   ╚═╝     ╚═╝╚══════╝╚══════╝╚═╝  ╚═╝
███╗   ███╗ █████╗ ███████╗████████╗███████╗██████╗ 
████╗ ████║██╔══██╗██╔════╝╚══██╔══╝██╔════╝██╔══██╗
██╔████╔██║███████║███████╗   ██║   █████╗  ██████╔╝
██║╚██╔╝██║██╔══██║╚════██║   ██║   ██╔══╝  ██╔══██╗
██║ ╚═╝ ██║██║  ██║███████║   ██║   ███████╗██║  ██║
╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝
"@

Function Get-ArtikelFromOutlook
{
    param(
        [int]$Index = 0
    )
    
    $olFolders = "Microsoft.Office.Interop.Outlook.olDefaultFolders" -as [type]
    $outlook = New-Object -ComObject outlook.application
    $namespace = $outlook.GetNameSpace("MAPI")
    $inbox = $namespace.GetDefaultFolder($olFolders::olFolderInbox)

    $items = $inbox.Items.
        Restrict("[SenderEmailAddress] = 'kontakt@pizza.de'").
        Restrict("@SQL=""urn:schemas:httpmail:subject"" like 'Deine Bestellung ist angekommen'")

    $items | select -Skip $Index -Last 1 | ParseArtikel
}

Function ParseArtikel
{
    param(
        [Parameter(Position=0, Mandatory=$true, ValueFromPipeline=$true)][object[]]$InputObject
    )

    process {
        foreach($i in $InputObject)
        {
			$rechnung = [PizzaMaster.PowerShell.Rechnung]@{
				Datum = $i.ReceivedTime
			}

            $match = [regex]::Match($i.Body, "Lieferservice:\s+(.+)")
            if (-not $match.Success)
            {
                throw "Not Found"
            }

            $rechnung.Lieferdienst = $match.Groups[1].Value.Trim()

            $match = [regex]::Match($i.Body, "\s+Gesamt\s+(\d+.\d+) €")
            if (-not $match.Success)
            {
                throw "Not Found"
            }

            $rechnung.Betrag = [decimal] $match.Groups[1].Value.Trim()

            $match = [regex]::Match($i.Body, "Gericht\s+TOTAL\s+((?:\d+x .+\d+.\d+ €\s+)+)Zwischensumme")
            if (-not $match.Success)
            {
                throw "Not Found"
            }
    
            $rows = [regex]::Matches($match.Groups[1].Value, "(\d+)x (?:\d+ )?(.+?)(\d+\.\d+) €")
            $rows | %{
                $result = [pscustomobject]@{
                    Anzahl = [int] $_.Groups[1].Value
                    Preis = [decimal]$_.Groups[3].Value
                    Beschreibung = $_.Groups[2].Value.Trim()
					Rechnung = $rechnung
                }

				$result.PSObject.TypeNames.Insert(0, "PizzaMaster.PowerShell.Artikel")

				$result
            }
        }
    }
}

Export-ModuleMember "*-*"

Remove-Module Microsoft.PowerShell*

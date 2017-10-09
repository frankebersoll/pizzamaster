using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, PizzaMasterNouns.Artikel)]
    public class GetArtikel : PizzaMasterCmdlet
    {
        protected override void BeginOverride()
        {
            var bestellungen = this.Client.GetBestellungen();
            foreach (var bestellung in bestellungen)
            {
                this.WriteObject(bestellung.Artikel, true);
            }
        }
    }
}
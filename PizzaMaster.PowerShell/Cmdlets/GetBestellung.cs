using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, PizzaMasterNouns.Bestellung)]
    public class GetBestellung : PizzaMasterCmdlet
    {
        protected override void BeginOverride()
        {
            var bestellungen = this.Client.GetBestellungen();
            this.WriteObject(bestellungen, true);
        }
    }
}
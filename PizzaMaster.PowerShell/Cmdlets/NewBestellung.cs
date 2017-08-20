using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.New, PizzaMasterNouns.Bestellung)]
    public class NewBestellung : PizzaMasterCmdlet
    {
        [Parameter(Mandatory = true, Position = 1)]
        [ValidateNotNullOrEmpty]
        public string Lieferdienst { get; set; }

        protected override void BeginOverride()
        {
            var bestellung = this.Client.BestellungBeginnen(this.Lieferdienst);
            this.WriteObject(bestellung);
        }
    }
}
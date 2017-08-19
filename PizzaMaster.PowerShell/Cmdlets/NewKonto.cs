using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.New, Nouns.Konto)]
    public class NewKonto : PizzaMasterCmdlet
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string Benutzer { get; set; }

        protected override void BeginOverride()
        {
            var benutzer = new Benutzer(this.Benutzer);
            var konto = this.Client.KontoEroeffnen(benutzer);
            this.WriteObject(konto);
        }
    }
}
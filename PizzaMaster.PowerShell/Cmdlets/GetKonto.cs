using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, Nouns.Konto)]
    public class GetKonto : PizzaMasterCmdletWithBenutzer
    {
        protected override void BeginOverride()
        {
            if (this.Benutzer != null)
            {
                var konto = this.Client.TryGetKonto(this.Benutzer);
                this.WriteObject(konto);
                return;
            }

            var konten = this.Client.GetKonten();
            foreach (var konto in konten)
            {
                this.FlushLog();
                this.WriteObject(konto);
            }
        }
    }
}
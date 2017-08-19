using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Add, Nouns.Einzahlung)]
    public class AddEinzahlung : PizzaMasterCmdletWithBenutzer
    {
        protected override bool BenutzerIsMandatory => true;

        [Parameter(Mandatory = true, Position = 2)]
        [ValidateRange(0.01, 100000)]
        public decimal Betrag { get; set; }

        protected override void BeginOverride()
        {
            var konto = this.Client.TryGetKonto(this.Benutzer);
            if (konto == null)
            {
                throw new Exception("Konto nicht gefunden.");
            }

            konto.Einzahlen(this.Betrag);

            this.WriteObject(konto);
        }
    }
}
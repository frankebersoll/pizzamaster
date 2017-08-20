using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Add, PizzaMasterNouns.Einzahlung)]
    public class AddEinzahlung : PizzaMasterCmdletWithBenutzer
    {
        private const string BetragKey = "Betrag";

        protected override bool BenutzerIsMandatory => true;

        private decimal Betrag => this.GetParameter<decimal>(BetragKey);

        protected override void AddParameters()
        {
            base.AddParameters();
            this.AddParameter(BetragKey, typeof(decimal), 1, true,
                              c => c.Add(new ValidateRangeAttribute(0.01, 100000)));
        }

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
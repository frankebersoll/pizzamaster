using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PizzaMaster.Domain.Konten;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Add, PizzaMasterNouns.Einzahlung)]
    public class AddEinzahlung : PizzaMasterCmdletWithBenutzer
    {
        private const string ArtKey = "Art";
        private const string BetragKey = "Betrag";

        private Einzahlungsart Art => this.GetParameter<Einzahlungsart>(ArtKey);

        private decimal Betrag => this.GetParameter<decimal>(BetragKey);

        protected override void AddParameters()
        {
            base.AddParameters();
            this.AddParameter(BetragKey, typeof(decimal), 1, true,
                              c => c.Add(new ValidateRangeAttribute(0.01, 100000)));
            this.AddParameter(ArtKey, typeof(Einzahlungsart), 2, false);
        }

        protected override void ProcessOverride()
        {
            foreach (var benutzer in this.Benutzer)
            {
                var konto = this.Client.TryGetKonto(benutzer);
                if (konto == null)
                {
                    throw new Exception("Konto nicht gefunden.");
                }

                konto.Einzahlen(this.Betrag, this.Art);
                this.WriteObject(konto);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Add, PizzaMasterNouns.Abbuchung, DefaultParameterSetName = "Default")]
    public class AddAbbuchung : PizzaMasterCmdletWithBenutzer
    {
        private const string BeschreibungKey = "Beschreibung";

        private const string BetragKey = "Betrag";

        private string Beschreibung => this.GetParameter<string>(BeschreibungKey);

        private decimal Betrag => this.GetParameter<decimal>(BetragKey);

        protected override void AddParameters()
        {
            base.AddParameters();
            this.AddParameter(BetragKey, typeof(decimal), 1, true,
                              c => c.Add(new ValidateRangeAttribute(0.01, 100000)));
            this.AddParameter(BeschreibungKey, typeof(string), 2, true,
                              c => c.Add(new ValidateNotNullOrEmptyAttribute()));
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

                konto.Abbuchen(this.Betrag, this.Beschreibung);
                this.WriteObject(konto);
            }
        }
    }
}
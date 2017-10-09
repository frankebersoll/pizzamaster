using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PizzaMaster.Application.Client;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.New, PizzaMasterNouns.Bestellung, DefaultParameterSetName = "Default")]
    public class NewBestellung : PizzaMasterCmdlet
    {
        private BestellungModel bestellung;

        [Parameter(ValueFromPipeline = true, Position = 0, ParameterSetName = "Rechnung")]
        public PSObject[] InputObject { get; set; }

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "Default")]
        [ValidateNotNullOrEmpty]
        public string Lieferdienst { get; set; }

        protected override void EndOverride()
        {
            if (this.bestellung == null)
            {
                this.bestellung = this.Client.BestellungBeginnen(this.Lieferdienst);
            }

            this.WriteObject(this.bestellung.Artikel, true);
        }

        protected override void ProcessOverride()
        {
            if (this.ParameterSetName != "Rechnung")
                return;

            foreach (var artikel in this.InputObject)
            {
                var anzahl = (int) artikel.Properties["Anzahl"].Value;
                var preis = (decimal) artikel.Properties["Preis"].Value;
                var beschreibung = (string) artikel.Properties["Beschreibung"].Value;
                var rechnung = (Rechnung) artikel.Properties["Rechnung"].Value;
                var einzelpreis = preis / anzahl;

                if (this.bestellung == null)
                {
                    this.bestellung = this.Client.BestellungBeginnen(rechnung.Lieferdienst, rechnung.Datum);
                }

                for (var i = 0; i < anzahl; i++)
                {
                    this.bestellung.ArtikelHinzufuegen(einzelpreis, beschreibung);
                }
            }
        }
    }
}
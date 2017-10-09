using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, PizzaMasterNouns.Konto, DefaultParameterSetName = "GetAll")]
    public class GetKonto : PizzaMasterCmdletWithBenutzer
    {
        private const string TransaktionenKey = "Transaktionen";

        private SwitchParameter Transaktionen => this.GetParameter<SwitchParameter>(TransaktionenKey);

        protected override void AddParameters()
        {
            base.AddParameters();
            this.AddParameter(TransaktionenKey, typeof(SwitchParameter), 1, false, set: "Benutzer");
        }

        protected override void ProcessOverride()
        {
            if (this.Benutzer != null)
            {
                foreach (var benutzer in this.Benutzer)
                {
                    var konto = this.Client.TryGetKonto(benutzer);

                    if (!this.Transaktionen.IsPresent)
                    {
                        this.WriteObject(konto);
                    }
                    else
                    {
                        this.WriteObject(konto.Transaktionen, true);
                    }
                }
            }
            else
            {
                var konten = this.Client.GetKonten();
                this.WriteObject(konten, true);
            }
        }
    }
}
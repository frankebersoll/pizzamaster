using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PizzaMaster.Application.Client;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsLifecycle.Complete, PizzaMasterNouns.Bestellung)]
    public class CompleteBestellung : PizzaMasterCmdlet
    {
        [Parameter(ValueFromPipeline = true, Position = 0)]
        public BestellungModel[] InputObject { get; set; }

        protected override void ProcessOverride()
        {
            foreach (var bestellung in this.InputObject)
            {
                bestellung.Abschliessen();
            }

            var benutzer = this.InputObject
                               .SelectMany(b => b.Artikel.Select(a => a.Benutzer))
                               .Distinct();

            var konten = benutzer.Select(b => this.Client.TryGetKonto(b)).ToArray();
            this.WriteObject(konten, true);
        }
    }
}
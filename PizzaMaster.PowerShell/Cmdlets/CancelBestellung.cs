using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PizzaMaster.Application.Client;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Remove, PizzaMasterNouns.Bestellung)]
    public class CancelBestellung : PizzaMasterCmdlet
    {
        [Parameter(ValueFromPipeline = true, Position = 0)]
        public BestellungModel[] InputObject { get; set; }

        protected override void ProcessOverride()
        {
            foreach (var bestellung in this.InputObject)
            {
                bestellung.Abbrechen();
            }
        }
    }
}
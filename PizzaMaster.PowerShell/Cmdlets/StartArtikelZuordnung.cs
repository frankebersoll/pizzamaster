using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PizzaMaster.Application.Client;
using PizzaMaster.PowerShell.UI;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsLifecycle.Start, "ArtikelZuordnung")]
    public class StartArtikelZuordnung : PizzaMasterCmdlet
    {
        private readonly List<ArtikelModel> artikel = new List<ArtikelModel>();

        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 0)]
        public PSObject[] InputObject { get; private set; }

        protected override void EndOverride()
        {
            var benutzer = this.Client.GetBenutzer();
            var viewModel = new ZuordnenViewModel(this.artikel.ToArray(), benutzer);

            new Zuordnen(viewModel).ShowDialog();
        }

        protected override void ProcessOverride()
        {
            var baseObjects = this.InputObject
                                  .Select(i => i.BaseObject)
                                  .ToArray();

            var artikel = baseObjects.OfType<BestellungModel>()
                                     .SelectMany(b => b.Artikel)
                                     .Concat(baseObjects.OfType<ArtikelModel>());

            foreach (var a in artikel)
            {
                this.artikel.Add(a);
            }
        }
    }
}
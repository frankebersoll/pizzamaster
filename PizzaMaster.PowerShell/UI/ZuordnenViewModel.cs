using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using PizzaMaster.Application.Client;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.PowerShell.UI
{
    public class ZuordnenViewModel
    {
        public ZuordnenViewModel(ArtikelModel[] artikel, Benutzer[] benutzer)
        {
            this.Artikel = artikel.Select(a => new ArtikelViewModel(a, benutzer)).ToArray();
            this.SelectedArtikel = this.Artikel.First();
        }

        public ArtikelViewModel[] Artikel { get; }

        public ArtikelViewModel SelectedArtikel { get; set; }

        private void Finish()
        {
            foreach (var artikelViewModel in this.Artikel)
            {
                artikelViewModel.Zuordnen();
            }

            this.Finished?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Finished;

        public void SelectNext()
        {
            this.SelectedArtikel.ClearFilter();

            var view = CollectionViewSource.GetDefaultView(this.Artikel);
            view.MoveCurrentToNext();

            if (view.IsCurrentAfterLast)
            {
                this.Finish();
            }
        }
    }
}
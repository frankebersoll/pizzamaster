using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using PizzaMaster.Application.Client;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.PowerShell.UI
{
    public class ArtikelViewModel
    {
        private readonly ICollectionView collectionView;
        private readonly ArtikelModel model;
        private string filter = string.Empty;

        public ArtikelViewModel(ArtikelModel model, Benutzer[] alleBenutzer)
        {
            this.AlleBenutzer = alleBenutzer.ToArray();
            this.model = model;
            this.Benutzer = model.Benutzer ?? alleBenutzer.First();
            this.collectionView = CollectionViewSource.GetDefaultView(this.AlleBenutzer);
            this.collectionView.Filter = this.StartsWithFilter;
        }

        public Benutzer[] AlleBenutzer { get; }

        public Benutzer Benutzer { get; set; }

        public string Beschreibung => this.model.Beschreibung;

        public Betrag Betrag => this.model.Betrag;

        public string Filter
        {
            get => string.IsNullOrEmpty(this.filter) ? "" : char.ToUpper(this.filter[0]) + this.filter.Substring(1);
            private set => this.filter = value;
        }

        public void AppendToFilter(char c)
        {
            var current = this.collectionView.CurrentItem;
            this.Filter += c;
            this.collectionView.Refresh();

            if (!this.collectionView.Contains(current))
            {
                this.collectionView.MoveCurrentToFirst();
            }

            if (this.collectionView.CurrentItem == null)
            {
                this.DeleteLastFilterChar();
                this.collectionView.MoveCurrentTo(current);
            }

            this.OnFiltered();
        }

        public void BenutzerDown()
        {
            this.collectionView.MoveCurrentToNext();
            if (this.collectionView.IsCurrentAfterLast)
            {
                this.collectionView.MoveCurrentToLast();
            }
        }

        public void BenutzerUp()
        {
            this.collectionView.MoveCurrentToPrevious();
            if (this.collectionView.IsCurrentBeforeFirst)
            {
                this.collectionView.MoveCurrentToFirst();
            }
        }

        public void ClearFilter()
        {
            this.Filter = "";
            this.collectionView.Refresh();
            this.OnFiltered();
        }

        public void DeleteLastFilterChar()
        {
            if (this.Filter.Length == 0)
            {
                return;
            }

            this.Filter = this.Filter.Substring(0, this.Filter.Length - 1);
            this.collectionView.Refresh();

            if (this.collectionView.CurrentItem == null)
            {
                this.collectionView.MoveCurrentToFirst();
            }

            this.OnFiltered();
        }

        public event EventHandler FilterChanged;

        protected virtual void OnFiltered()
        {
            this.FilterChanged?.Invoke(this, EventArgs.Empty);
        }

        private bool StartsWithFilter(object o)
        {
            if (this.Filter.Length > 0 && o is Benutzer b)
            {
                return b.Value.StartsWith(this.Filter, StringComparison.CurrentCultureIgnoreCase);
            }

            return true;
        }

        public void Zuordnen()
        {
            this.model.Zuordnen(this.Benutzer);
        }
    }
}
using EventFlow.Queries;
using PizzaMaster.Konten.ValueObjects;

namespace PizzaMaster.Konten.Queries
{
    public class KontoByBenutzerQuery : IQuery<Konto>
    {
        public Benutzer Benutzer { get; }
        
        public bool ThrowIfNotFound { get; }

        public KontoByBenutzerQuery(Benutzer benutzer, bool throwIfNotFound = true)
        {
            this.Benutzer = benutzer;
            this.ThrowIfNotFound = throwIfNotFound;
        }
    }
}
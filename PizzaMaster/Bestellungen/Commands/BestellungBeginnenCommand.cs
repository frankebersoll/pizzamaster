using EventFlow.Commands;

namespace PizzaMaster.Bestellungen.Commands
{
    public class BestellungBeginnenCommand : Command<BestellungAggregate, BestellungId>
    {
        public BestellungBeginnenCommand(string lieferdienst) : base(new BestellungId(BestellungId.New.Value))
        {
            this.Lieferdienst = lieferdienst;
        }

        public string Lieferdienst { get; }
    }
}
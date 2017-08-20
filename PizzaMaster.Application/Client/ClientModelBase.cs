using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Commands;
using EventFlow.Queries;

namespace PizzaMaster.Application.Client
{
    public abstract class ClientModelBase<TReadModel, TId> where TReadModel : class
    {
        protected readonly PizzaMasterClient Client;

        private TReadModel readModel;

        protected TReadModel ReadModel => this.readModel ?? (this.readModel = this.QueryReadModel());

        public ClientModelBase(PizzaMasterClient client, TId id)
        {
            this.Client = client;
            this.Id = id;
        }

        public ClientModelBase(PizzaMasterClient client, TId id, TReadModel readModel) : this(client, id)
        {
            this.readModel = readModel;
        }

        protected void Publish(params ICommand[] commands)
        {
            this.Client.Publish(commands);
            this.Refresh();
        }

        protected TResult Query<TResult>(IQuery<TResult> query)
        {
            return this.Client.Query(query);
        }

        public TId Id { get; private set; }

        protected abstract TReadModel QueryReadModel();

        public virtual void Refresh()
        {
            this.readModel = null;
        }
    }
}
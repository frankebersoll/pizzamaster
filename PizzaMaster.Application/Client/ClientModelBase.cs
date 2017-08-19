using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaMaster.Application.Client
{
    public abstract class ClientModelBase
    {
        protected readonly PizzaMasterClient Client;

        public ClientModelBase(PizzaMasterClient client)
        {
            this.Client = client;
        }
    }
}
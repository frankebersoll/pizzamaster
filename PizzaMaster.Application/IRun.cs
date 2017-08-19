using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMaster.Application.Client;

namespace PizzaMaster.Application
{
    public interface IRun
    {
        PizzaMasterClient Run();
    }
}
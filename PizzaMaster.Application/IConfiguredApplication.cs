using System;
using System.Collections.Generic;
using System.Linq;
using EventFlow.Logs;

namespace PizzaMaster.Application
{
    public interface IConfiguredApplication : IRun
    {
        IRun UseLog(ILog logger);
    }
}
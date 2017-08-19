using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using EventFlow.Logs;
using PizzaMaster.Application;
using PizzaMaster.Application.Client;

namespace PizzaMaster.PowerShell
{
    public abstract class PizzaMasterCmdlet : PSCmdlet
    {
        static PizzaMasterCmdlet()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        }

        protected PizzaMasterClient Client { get; private set; }

        protected virtual void BeginOverride() { }

        protected sealed override void BeginProcessing()
        {
            this.GetLog().StartLogging(this);
            try
            {
                this.Client = this.GetClient();
                this.BeginOverride();
            }
            catch (Exception)
            {
                this.GetLog().StopLogging(this);
                throw;
            }
        }

        private PizzaMasterClient CreateClient()
        {
            var log = this.GetLog();
            return PizzaMasterApplication
                .Create()
                .ConfigureLiteDb()
                .UseLog(log)
                .Run();
        }

        private static Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assembly = Assembly.Load(args.Name.Substring(0, args.Name.IndexOf(',')));
            return assembly;
        }

        protected virtual void EndOverride() { }

        protected sealed override void EndProcessing()
        {
            try
            {
                this.EndOverride();
            }
            finally
            {
                this.GetLog().StopLogging(this);
            }
        }

        protected void FlushLog()
        {
            this.GetLog().Flush();
        }

        protected PizzaMasterClient GetClient()
        {
            return this.GetOrCreateFromSessionCache("client", this.CreateClient);
        }

        private BufferedCmdletLog GetLog()
        {
            return this.GetOrCreateFromSessionCache("log", () => new BufferedCmdletLog());
        }

        private T GetOrCreateFromSessionCache<T>(string key, Func<T> factory)
        {
            var variables = this.SessionState.PSVariable;
            var value = (T) variables.GetValue(key);
            if (value == null)
            {
                value = factory();
                variables.Set(key, value);
            }

            return value;
        }

        protected virtual void ProcessOverride() { }

        protected override void ProcessRecord()
        {
            try
            {
                this.ProcessOverride();
            }
            catch (Exception)
            {
                this.GetLog().StopLogging(this);
                throw;
            }
        }
    }
}
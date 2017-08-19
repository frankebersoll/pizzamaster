using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using PizzaMaster.Domain.Common;

namespace PizzaMaster.PowerShell
{
    public abstract class PizzaMasterCmdletWithBenutzer : PizzaMasterCmdlet, IDynamicParameters
    {
        private const string BenutzerKey = "Benutzer";

        protected Benutzer Benutzer => this.MyInvocation.BoundParameters.TryGetValue(BenutzerKey, out var value)
                                           ? new Benutzer((string) value)
                                           : null;

        protected virtual bool BenutzerIsMandatory => false;

        public object GetDynamicParameters()
        {
            var benutzer = this.GetClient().GetKonten().Select(k => k.Benutzer.Value).ToArray();

            var attributes = new Collection<Attribute>
                             {
                                 new ParameterAttribute
                                 {
                                     Position = 1,
                                     Mandatory = this.BenutzerIsMandatory
                                 },
                                 new ValidateSetAttribute(benutzer)
                             };

            var runtimeDefinedParameterDictionary
                = new RuntimeDefinedParameterDictionary
                  {
                      {
                          BenutzerKey,
                          new RuntimeDefinedParameter(BenutzerKey, typeof(String), attributes)
                      }
                  };

            return runtimeDefinedParameterDictionary;
        }
    }
}
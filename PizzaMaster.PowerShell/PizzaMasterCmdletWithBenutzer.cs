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

        private RuntimeDefinedParameterDictionary parameters;

        protected Benutzer Benutzer
        {
            get
            {
                var benutzer = this.GetParameter<string>(BenutzerKey);
                return benutzer != null ? new Benutzer(benutzer) : null;
            }
        }

        protected virtual bool BenutzerIsMandatory => false;

        public object GetDynamicParameters()
        {
            this.parameters = new RuntimeDefinedParameterDictionary();
            this.AddParameters();
            return this.parameters;
        }

        protected void AddParameter(
            string key,
            Type type,
            int position,
            bool isMandatory,
            Action<Collection<Attribute>> additionalAttributes = null)
        {
            var attributes = new Collection<Attribute>
                             {
                                 new ParameterAttribute
                                 {
                                     Position = position,
                                     Mandatory = isMandatory
                                 }
                             };

            additionalAttributes?.Invoke(attributes);
            this.parameters.Add(key, new RuntimeDefinedParameter(key, type, attributes));
        }

        protected virtual void AddParameters()
        {
            var benutzer = this.GetClient().GetKonten().Select(k => k.Benutzer.Value).ToArray();
            var validateSet = new ValidateSetAttribute(benutzer);
            this.AddParameter(BenutzerKey, typeof(string), 0, this.BenutzerIsMandatory, p => p.Add(validateSet));
        }

        protected T GetParameter<T>(string key)
        {
            return (T)this.parameters?[key].Value;
        }
    }
}
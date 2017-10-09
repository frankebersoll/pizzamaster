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

        protected Benutzer[] Benutzer
        {
            get
            {
                var benutzer = this.GetParameter<string[]>(BenutzerKey);
                return benutzer?.Select(b => new Benutzer(b)).ToArray();
            }
        }

        public object GetDynamicParameters()
        {
            if (this.parameters == null)
            {
                this.parameters = new RuntimeDefinedParameterDictionary();
                this.AddParameters();
            }

            return this.parameters;
        }

        protected void AddParameter(
            string key,
            Type type,
            int position,
            bool isMandatory,
            Action<Collection<Attribute>> additionalAttributes = null,
            string set = null,
            bool fromPipeline = false)
        {
            var attributes = new Collection<Attribute>
                             {
                                 new ParameterAttribute
                                 {
                                     Position = position,
                                     Mandatory = isMandatory,
                                     ParameterSetName = set,
                                     ValueFromPipelineByPropertyName = fromPipeline
                                 }
                             };

            additionalAttributes?.Invoke(attributes);
            var runtimeParameter = new RuntimeDefinedParameter(key, type, attributes);
            this.parameters[key] = runtimeParameter;
        }

        protected virtual void AddParameters()
        {
            var benutzer = this.GetClient().GetBenutzer().Select(b => b.Value).ToArray();
            if (benutzer.Any())
            {
                var validateSet = new ValidateSetAttribute(benutzer);
                this.AddParameter(BenutzerKey, typeof(string[]), 0, true, p => p.Add(validateSet), "Benutzer", true);
            }
        }

        protected T GetParameter<T>(string key)
        {
            if (this.parameters != null)
            {
                if (this.parameters.TryGetValue(key, out var parameter))
                {
                    var value = parameter?.Value;
                    if (value != null)
                        return (T) value;
                }
            }

            return default(T);
        }
    }
}
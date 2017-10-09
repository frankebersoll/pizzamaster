using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PizzaMaster.PowerShell.Cmdlets
{
    [Cmdlet(VerbsCommon.Reset, "PizzaMasterReadModels")]
    public class ResetPizzaMasterReadModels : PizzaMasterCmdlet
    {
        protected override void EndOverride()
        {
            this.Client.ResetReadModels();
        }
    }
}
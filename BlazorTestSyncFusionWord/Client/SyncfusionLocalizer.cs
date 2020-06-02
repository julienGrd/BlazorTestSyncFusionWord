using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorTestSyncFusionWord.Client
{
    public class SyncfusionLocalizer : ISyncfusionStringLocalizer
    {
        // To get the locale key from mapped resources file
        public string Get(string key)
        {
            Console.WriteLine($"get resources {key}");
            return this.Manager.GetString(key);
        }

        // To access the resource file and get the exact value for locale key

        public System.Resources.ResourceManager Manager
        {
            get
            {
                // Replace the ApplicationNamespace with your application name.
                return Resources.SfResources.ResourceManager;
            }
        }
    }
}

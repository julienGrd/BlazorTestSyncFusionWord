using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace BlazorTestSyncFusionWord.Client
{
    public class SyncfusionLocalizer : ISyncfusionStringLocalizer
    {

        public string GetText(string key)
        {
            Console.WriteLine($"get resources {key}");
            return this.ResourceManager.GetString(key);
        }

        public ResourceManager ResourceManager => Resources.SfResources.ResourceManager;
    }
}

using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using System.Globalization;
using Microsoft.JSInterop;

namespace BlazorTestSyncFusionWord.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSyncfusionBlazor();

            #region Localization
            // Register the Syncfusion locale service to customize the  SyncfusionBlazor component locale culture
           // builder.Services.AddSingleton(typeof(ISyncfusionStringLocalizer), typeof(SyncfusionLocalizer));

            // Set the default culture of the application
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fr");

            // Get the modified culture from culture switcher
            //var host = builder.Build();
            //var jsInterop = host.Services.GetRequiredService<IJSRuntime>();
            //var result = await jsInterop.InvokeAsync<string>("fr-FR");
            //if (result != null)
            //{
            //    // Set the culture from culture switcher
            //    var culture = new CultureInfo(result);
            //    CultureInfo.DefaultThreadCurrentCulture = culture;
            //    CultureInfo.DefaultThreadCurrentUICulture = culture;
            //}
            #endregion

            await builder.Build().RunAsync();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FacCigo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        public App()
        {
            host = CreateHostBuilder().Build();
        }
        private  IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<AppHostedService>();
                  
               });
        protected override async void OnStartup(StartupEventArgs e)
        {
            
            await host.StartAsync();
            base.OnStartup(e);
        }
        public void Application_Startup(object sender,StartupEventArgs e)
        {

        }
        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}

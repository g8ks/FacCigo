using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using System.Windows;

namespace FacCigo
{
    public class AppHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
           
            using (var application = AbpApplicationFactory.Create<FacCigoWpfUIModule>(options =>
            {
                options.UseAutofac();//Autofac integration

            }))
            {
                application.Initialize();
              
                var mainForm = application.ServiceProvider.GetService<MainWindow>();
                mainForm.Show();
                application.Shutdown();
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

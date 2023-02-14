using CUDJobApiIdentity.Contracts;
using CUDJobAPiIdentity.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.BackGroundServices
{
    public class CareerBackgroundService : BackgroundService
    {
        private readonly ILoggerService _logger;
        private readonly IServiceProvider _serviceProvider;

        public CareerBackgroundService(ILoggerService logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scoped= scope.ServiceProvider.GetRequiredService<IBackgroundContracts>();
                    var companiesList = scoped.expiringcompanies(5);
                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
                   
            }
            //throw new NotImplementedException();
        }
    }
}

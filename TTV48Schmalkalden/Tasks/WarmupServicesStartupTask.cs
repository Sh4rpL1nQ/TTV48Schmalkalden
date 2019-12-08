﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TTV48Schmalkalden.Tasks
{
    public class WarmupServicesStartupTask : IStartupTask
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;
        public WarmupServicesStartupTask(IServiceCollection services, IServiceProvider provider)
        {
            _services = services;
            _provider = provider;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var scope = _provider.CreateScope())
            {
                foreach (var singleton in GetServices(_services))
                {
                    scope.ServiceProvider.GetServices(singleton);
                }
            }

            return Task.CompletedTask;
        }

        static IEnumerable<Type> GetServices(IServiceCollection services)
        {
            return services
                .Where(descriptor => descriptor.ImplementationType != typeof(WarmupServicesStartupTask))
                .Where(descriptor => descriptor.ServiceType.ContainsGenericParameters == false)
                .Select(descriptor => descriptor.ServiceType)
                .Distinct();
        }
    }
}

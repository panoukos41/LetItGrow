using LetItGrow.Identity.Workers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace LetItGrow.Identity
{
    public static class Worker
    {
        /// <summary>
        /// If this file exists the workes won't run.
        /// </summary>
        public const string WorkerFile = "app_workers_executed";

        private static string Directory => Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, WorkerFile);

        /// <summary>
        /// Registers default hosted services to run. Checks if the <see cref="WorkerFile"/>
        /// exists before registration, if it exists then workers are not registered.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWorkers(this IServiceCollection services)
        {
#if !DEBUG
            if (File.Exists(Directory)) return services;
            File.Create(Directory);
#endif
            services.AddHostedService<OpenIddictWorker>();
            services.AddHostedService<IdentityWorker>();

            return services;
        }
    }
}
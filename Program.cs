using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace dotnet_core_console_di
{
    class Program
    {
        static void Main(string[] args)
        {
            // dotnet add package Microsoft.Extensions.DependencyInjection

             IServiceCollection services = new ServiceCollection();

             ConfigureServices(services);

            //  services.AddScoped<IFooService, FooService>();
            //  services.AddScoped<IBooService, BooService>();
            //  services.AddLogging(configure => configure.AddConsole());

             using(var serviceProvider = services.BuildServiceProvider())
             {
                IFooService fooService = serviceProvider.GetService<IFooService>();

                string result = fooService.Get();

                System.Console.WriteLine(result);

                IBooService booService = serviceProvider.GetService<IBooService>();

                booService.DoWork();
             
             }

            System.Console.WriteLine("Press any key to exit.");

            Console.ReadKey();

        }

        private static void ConfigureServices(IServiceCollection services)
        {
             services.AddScoped<IFooService, FooService>();
             services.AddScoped<IBooService, BooService>();

             services.AddLogging(configure => configure.AddConsole());
        }
    }

    public interface IFooService
    {
        string Get();
    }

    public interface IBooService
    {
        void DoWork();
    }

    public class FooService : IFooService
    {
       
        public string Get() => "Boo";
    }

    public class BooService : IBooService
    {
        private readonly IFooService fooService;

         // dotnet add package Microsoft.Extensions.Logging.Console
        private readonly ILogger logger;


        public BooService(IFooService fooService, ILogger<BooService> logger)
        {
            this.fooService = fooService;
            this.logger = logger;
        }

        public void DoWork()
        {
            System.Console.WriteLine(fooService.Get());

            logger.LogInformation(fooService.Get());
        }
    }
}

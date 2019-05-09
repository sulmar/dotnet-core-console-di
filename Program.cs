using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

            services.AddOptions();

            services.Configure<BooOptions>(options =>
            {
                options.Count = 5;
            });
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

    public class BooOptions
    {
        public byte Count { get; set; }
    }

    public class BooService : IBooService
    {
        private readonly IFooService fooService;

         // dotnet add package Microsoft.Extensions.Logging.Console
        private readonly ILogger logger;

        private readonly BooOptions options;

        public BooService(IOptions<BooOptions> options, IFooService fooService, ILogger<BooService> logger)
        {
            this.options = options.Value;
            this.fooService = fooService;
            this.logger = logger;
        }

        public void DoWork()
        {
            for(byte i = 0; i<options.Count; i++)
            {
                System.Console.WriteLine(fooService.Get());
                logger.LogInformation(fooService.Get());
            }
        }
    }
}

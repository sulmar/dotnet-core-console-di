# Wstrzykiwanie zależności w aplikacji konsolowej .NET Core


Program.cs

~~~ csharp

    // dotnet add package Microsoft.Extensions.DependencyInjection
static void Main(string[] args)
        {
             IServiceCollection services = new ServiceCollection();

             services.AddScoped<IFooService, FooService>();
            
             using(var serviceProvider = services.BuildServiceProvider())
             {
                IFooService fooService = serviceProvider.GetService<IFooService>();

                string result = fooService.Get();

                System.Console.WriteLine(result);
            
             }

            System.Console.WriteLine("Press any key to exit.");

            Console.ReadKey();

        }

        

    public interface IFooService
    {
        string Get();
    }

  
    public class FooService : IFooService
    {
       
        public string Get() => "Boo";
    }
}


~~~


W repozytorium znajduje się rozbudowany przykład ze wstrzykiwaniem logowania oraz konfiguracji.

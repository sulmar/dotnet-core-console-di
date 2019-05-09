﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_core_console_di
{
    class Program
    {
        static void Main(string[] args)
        {
            // dotnet add package Microsoft.Extensions.DependencyInjection

             var services = new ServiceCollection();

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

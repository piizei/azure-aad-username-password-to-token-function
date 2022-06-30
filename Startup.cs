using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;


[assembly: FunctionsStartup(typeof(UsernamePasswordTokenFunction.Startup))]

namespace UsernamePasswordTokenFunction
{
    public class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Console.WriteLine("Starting up");
           // Todo: Add JWT authentication
               
        }




    }
}

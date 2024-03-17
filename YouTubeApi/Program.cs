// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YouTubeApi;


class Program
{
    static async Task Main(string[] args)
    {
        IHost host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ApiCallerService>();
            });
}

public class ApiCallerService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            Console.WriteLine("Program is starting");
            MainProgram mainProgram = new MainProgram();
            await mainProgram.ApiCaller();

            Console.WriteLine("ApiCallerService will wait for 20 minutes");
            await Task.Delay(TimeSpan.FromMinutes(20), stoppingToken); // 20 minutes delay
        }
    }
}

using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TFWMFeedReader;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


// Add Azure App Configuration.
var config = new ConfigurationBuilder().AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly(), true);
var azAppConfigConnection = config.Build()["AppConfig"];

if (!string.IsNullOrEmpty(azAppConfigConnection))
{
    // Use the connection string if it is available.
    config.AddAzureAppConfiguration(options =>
    {
        options.Connect(azAppConfigConnection);
        options.Select("BusConfig:*", "Development");
    });
}
else
{
    // Use Azure Active Directory authentication.
    // The identity of this app should be assigned 'App Configuration Data Reader' or 'App Configuration Data Owner' role in App Configuration.
    // For more information, please visit https://aka.ms/vs/azure-app-configuration/concept-enable-rbac
    config.AddAzureAppConfiguration(options =>
    {
        options.Connect(new Uri("https://busappac.azconfig.io"), new DefaultAzureCredential());
    });
}

var azAppConfig = config.Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddOptions<TFWMOptions>()
            .Bind(azAppConfig.GetSection("BusConfig"));
        services
            .AddTFWMFeedReader()
            .AddTFWMService();
    })
    .Build();

//var reader = host.Services.GetRequiredService<ITFWMFeedReader>();
//var message = await reader.GetFeedMessageAsync(default);

var service = host.Services.GetRequiredService<ITFWMService>();
var route24 = await service.GetTripsForRouteAsync("65060", default);

Console.WriteLine("Press Any Key to Exit");
Console.ReadLine();

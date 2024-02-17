using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StrategyDesignPattern;

// Setup the configuration data for the test
Environment.SetEnvironmentVariable("OsStrategy", " \"MyValue\" from Environment Variable");
var data = new Dictionary<string, string> { { "OsStrategy", " \"MyValue\" from AppSettings.json" } };

var configurationBuilder = new ConfigurationBuilder();
// Add the configuration data to the in-memory collection
configurationBuilder.AddInMemoryCollection(data!);

// Setup the DI container
var services = new ServiceCollection();

services.AddSingleton<IConfiguration>(configurationBuilder.Build());

services.AddSingleton<IConfigurationStrategyContext, ConfigurationStrategyContext>();

var serviceProvider = services.BuildServiceProvider();

// Get the IConfigurationStrategyContext from the DI container
var configurationStrategyContext = serviceProvider.GetService<IConfigurationStrategyContext>();


var result = configurationStrategyContext?.GetConfigurationData("OsStrategy");
Console.WriteLine($"The configuration data is {result}");
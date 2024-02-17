
using Microsoft.Extensions.DependencyInjection;

using StrategyDesignPattern;

//FileStream fileStream = new FileStream("test.txt", FileMode.OpenOrCreate, FileAccess.Write);

var services = new ServiceCollection();
services.AddSingleton<IConfigurationMock, ConfigurationMock>();

services.AddSingleton<IConfigurationStrategyContext, ConfigurationStrategyContext>();

var serviceProvider = services.BuildServiceProvider();

var configurationStrategyContext = serviceProvider.GetService<IConfigurationStrategyContext>();

var result = configurationStrategyContext.GetConfigurationData("MyKey");


public interface IConfigurationMock
{
    public string Value { get; set; }
    IConfigurationMock GetSection(string key);
}
public class ConfigurationMock : IConfigurationMock
{
    public string Value { get; set; }
    public  IConfigurationMock GetSection(string key) => this;
}
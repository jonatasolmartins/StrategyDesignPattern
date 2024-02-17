using Microsoft.Extensions.Configuration;

namespace StrategyDesignPattern;

public interface IConfigurationStrategy
{
    string GetConfigurationData(string key);
}

public class JsonFileConfigurationStrategy : IConfigurationStrategy
{
    private readonly IConfigurationMock _configuration;

    public JsonFileConfigurationStrategy(IConfigurationMock configuration)
    {
        _configuration = configuration;
    }

    public string GetConfigurationData(string key)
    {
        return _configuration.GetSection(key).Value;
    }
}

public class EnvironmentConfigurationStrategy : IConfigurationStrategy
{
    public string GetConfigurationData(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }
}

public interface IConfigurationStrategyContext
{
    string GetConfigurationData(string key);
}

public class ConfigurationStrategyContext : IConfigurationStrategyContext
{
    private IConfigurationStrategy _strategy;

    public ConfigurationStrategyContext(IConfigurationMock configuration)
    {
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            _strategy = new JsonFileConfigurationStrategy(configuration);
        }
        else
        {
            _strategy = new EnvironmentConfigurationStrategy();
        }
    }

    public string GetConfigurationData(string key)
    {
        return _strategy.GetConfigurationData(key);
    }
}
using Microsoft.Extensions.Configuration;

namespace StrategyDesignPattern;

public interface IConfigurationStrategy
{
    string? GetConfigurationData(string key);
}

public class JsonFileConfigurationStrategy(IConfiguration configuration) : IConfigurationStrategy
{
    public string? GetConfigurationData(string key)
    {
        return configuration.GetSection(key).Value;
    }
}

public class EnvironmentConfigurationStrategy : IConfigurationStrategy
{
    public string? GetConfigurationData(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }
}

public interface IConfigurationStrategyContext
{
    string? GetConfigurationData(string key);
}

public class ConfigurationStrategyContext : IConfigurationStrategyContext
{
    private readonly IConfigurationStrategy _strategy;

    public ConfigurationStrategyContext(IConfiguration configuration)
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

    public string? GetConfigurationData(string key)
    {
        return _strategy.GetConfigurationData(key);
    }
}
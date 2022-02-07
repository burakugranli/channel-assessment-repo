namespace ChannelEngineLibrary.Configuration
{
    public interface IApiClientConfiguration
    {
        string ApiUrl { get; }

        string ApiKey { get; }
    }
}

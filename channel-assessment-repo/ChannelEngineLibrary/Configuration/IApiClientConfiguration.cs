using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineLibrary.Configuration
{
    public interface IApiClientConfiguration
    {
        string ApiUrl { get; }

        string ApiKey { get; }
    }
}

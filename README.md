# statful-dotnet-core-client
Statful client for .NET Core applications. This client is intended to gather metrics and send them to Statful.

## Table of Contents

* [Supported Versions of .NET Core](#supported-versions-of-.net-core)
* [Installation](#installation)
* [Quick Start](#quick-start)
* [Examples](#examples)
* [Reference](#reference)
* [Authors](#authors)
* [License](#license)

## Supported Versions of .NET Core

| Statful client Version | Tested .NET Core versions  |
|:---|:---|
| 1.x.x | `2.1.500`|

## Installation

Install the StatfulDotnetCoreClient nuget package and you are ready to use it. 

## Quick start

You can configure the client programmatically or by using a configuration file.

### Programmatically
Create an instance of `ClientConfiguration` to set all desired parameters like the following sample:
```c#
IClientConfiguration configuration = new ClientConfiguration.Builder()
                                                            .withHost("host")
                                                            .withPort(443)
                                                            .withToken("token")
                                                                .
                                                                .
                                                                .
                                                            Build();
```
### Configuration File
The configuration file is a json document that follows the following structure:

```c#
{
    "StatfulClient":{
        "Settings":{
            "host": "api.statful.com",
            "port": 443,
            "secure": true,
            "timeout": 1000,
            "token": "token",
            "app": "foo",
            "dryrun": false,
            "tags": ["tag1","value1"],
            "sampleRate": 100,
            "ns": "application",
            "flushInterval": 10000,
            "maxBufferSize": 5000,
            "path": "tel/v2.0/metrics",
            "transport": "http"
        }
    }
}

```

## Examples

### Initialize client

Then create an instance of `IStatfulClient` by using `StatfulClientFactory`:

```c# 
IStatfulClient client = StatfulClientFactory.CreateStatfulClient("pathToFile", "filename"); // Creates configured client from a file

OR

IStatfulClient client = StatfulClientFactory.CreateStatfulClient(configuration); // Creates configured client programmatically
```
Finally, you can send a metric just like this:

```c#
client.Time("name", 10, "tag1=sample", null);
```

### Defaults Configuration Per Method

`IStatfulClient` has 4 methods to send metrics:

    Inc -> appends a prefix `counter` to the metric name and, if none are specified, sets `sum` and `count` aggregations as default;
    
    Time -> appends a prefix `timer` to the metric name and, if none are specified, sets `avg`, `p90` and `count` aggregations as default;
    
    Gauge -> appends a prefix `gauge` to the metric name and, if none are specified, sets `last` aggregation as default;
    
    Put -> sends the metric as is;
    
## Reference

Reference if you want to take full advantage from Statful.    

### Global Configuration

The custom options that can be set on config param are detailed below.

| Option | Description | Type | Default | Required |
|:---|:---|:---|:---|:---|
| host | Defines the host name to where the metrics should be sent. |  `string` | `api.statful.com` | **NO** |
| port | Defines the port. |  `string` | `443` | **NO** |
| secure | Enable or disable HTTPS. | `boolean` | `true`| **NO** |
| timeout | Defines the timeout for the transport layers in **miliseconds**. | `number` | 2000 | **NO** |
| token | Defines the token to be used. | `string` | **undefined** | **YES** |
| app | Defines the application global name. If specified sets a global tag `app=setValue`. | `string` | **undefined** | **NO** |
| dryrun | Debug log metrics when flushing the buffer. | `boolean` | `false` | **NO**|
| tags | Define global list of tags to set, these are merged with custom tags set on method calls with priority to custom tags. | `string` | **undefined** | **NO** |
| sampleRate | Defines the rate sampling. Should be a number between **[1, 100]**. | `number` | `100` | **NO**|
| ns | Defines the global namespace. | `string` | `application` | **NO** | 
| flushInterval | Defines an interval to periodically flush the buffer based on time. | `number` | `10000` | **NO**|
| maxBufferSize | Defines how many metrics at max are kept in the buffer between forced flushes. | `number` | `5000` | **NO** |
| path | Defines the api path to where the metrics should be sent. | `string` | `tel/v2.0/metrics` | **NO** |
| transport | Defines the transport layer to be used to send metrics. **Valid Transports:** `udp`, `http` | `string` | `http` | **NO**
| logger | Defines logger library. | `string` | `SilentLogger` | **NO** |

## Authors

[Mindera - Software Craft](https://github.com/Mindera)

## License

Statful .NET Core Client is available under the MIT license. See the [LICENSE](https://raw.githubusercontent.com/statful/statful-dotnet-core-client/master/LICENSE) file for more information.
